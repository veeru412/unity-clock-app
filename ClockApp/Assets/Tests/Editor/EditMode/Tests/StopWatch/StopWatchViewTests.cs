using NUnit.Framework;
using UniRx;
using Assets.Scripts.StopWatch;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Assets.Scripts.Enum;
using Moq;
using System;

namespace Assets.Tests.EditMode.StopWatch
{
  public class StopWatchViewTests 
  {
    private StopWatchView view;
    private StopWatchModel model;
    private TextMeshProUGUI displayText;
    private TextMeshProUGUI lappedTimeText;
    private Toggle onOffToggle;
    private TextMeshProUGUI onOffToggleLabel;
    private Button resetButton;
    private Button recordButton;

    private Subject<TimeSpan> timerSubject;

    [SetUp]
    public void SetUp() {
      view = new GameObject("StopWatchView").AddComponent<StopWatchView>();
      displayText = new GameObject("DisplayText").AddComponent<TextMeshProUGUI>();
      lappedTimeText = new GameObject("LappedText").AddComponent<TextMeshProUGUI>();
      onOffToggle = new GameObject("StartStopToggle").AddComponent<Toggle>();
      resetButton = new GameObject("ResetButton").AddComponent<Button>();
      recordButton = new GameObject("RecordButton").AddComponent<Button>();
      onOffToggleLabel = new GameObject("Label").AddComponent<TextMeshProUGUI>();

      SetPrivateField(view, "displayText", displayText);
      SetPrivateField(view, "lappedTimeText", lappedTimeText);
      SetPrivateField(view, "onOffToggle", onOffToggle);
      SetPrivateField(view, "resetButton", resetButton);
      SetPrivateField(view, "recordButton", recordButton);
      SetPrivateField(view, "onOffToggleLabel", onOffToggleLabel);

      timerSubject = new Subject<TimeSpan>();
      model = new StopWatchModel(timerSubject);

      view.Construct(model);
    }

    [Test]
    public void ElapsedTimeUpdateTest()
    {
      //arrange
      var observeTime = TimeSpan.FromSeconds(10);
      var expectedTime = observeTime.ToString(@"mm\:ss\.ff");
      //act
      timerSubject.OnNext(observeTime); 
      //assert
      Assert.AreEqual(expectedTime, displayText.text);
    }

    [Test]
    public void LappedTimeUpdateTest()
    {
      model.ElapsedTime.Value = TimeSpan.FromSeconds(10);
      model.Record(); 
      Assert.AreEqual("00:10.00", lappedTimeText.text);
    }

    [Test]
    public void StartTimerTest()
    {
      onOffToggle.isOn = true;
      onOffToggle.onValueChanged.Invoke(true);

      Assert.AreEqual(TimerState.Running, model.State.Value);
      Assert.AreEqual("STOP", onOffToggleLabel.text);
    }

    [Test]
    public void StopTimerTest()
    {
      onOffToggle.isOn = false;
      onOffToggle.onValueChanged.Invoke(false);

      Assert.AreEqual(TimerState.Stopped, model.State.Value);
      Assert.AreEqual("START", onOffToggleLabel.text);
    }

    [Test]
    public void ResetButtonClickTest()
    {
      model.ElapsedTime.Value = TimeSpan.FromSeconds(42);
      model.State.Value = TimerState.Running;

      resetButton.onClick.Invoke();

      Assert.AreEqual(TimeSpan.Zero, model.ElapsedTime.Value);
      Assert.AreEqual(TimerState.Stopped, model.State.Value);
    }

    [Test]
    public void RecordButtonClickToStoreLappedTimeTest()
    {
      model.ElapsedTime.Value = TimeSpan.FromSeconds(25);
      recordButton.onClick.Invoke();
      Assert.AreEqual(TimeSpan.FromSeconds(25), model.LappedTime.Value);
    }

    [Test]
    public void RecordButtonShouldNotInteractWhenTimerRunningTest()
    {
      model.State.Value = TimerState.Stopped;
      Assert.IsTrue(recordButton.interactable);

      model.State.Value = TimerState.Running;
      Assert.IsFalse(recordButton.interactable);
    }


    [TearDown] 
    public void TearDown()
    {
      GameObject.DestroyImmediate(view.gameObject);
      GameObject.DestroyImmediate(displayText.gameObject);
      GameObject.DestroyImmediate(lappedTimeText.gameObject);
      GameObject.DestroyImmediate(recordButton.gameObject);
      GameObject.DestroyImmediate(resetButton.gameObject);
      GameObject.DestroyImmediate(onOffToggle.gameObject);
      GameObject.DestroyImmediate(onOffToggleLabel.gameObject);
    }

    private void SetPrivateField(object target, string fieldName, object value)
    {
      var field = target.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
      field?.SetValue(target, value);
    }
  }
}