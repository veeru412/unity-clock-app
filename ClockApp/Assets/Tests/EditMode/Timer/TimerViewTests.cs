using NUnit.Framework;
using UnityEngine;
using TMPro;
using Assets.Scripts.CountDownTimer;
using Moq;
using Assets.Scripts.Interface;
using UnityEngine.UI;
using UniRx;
using System;

namespace Assets.Tests.EditMode.Timer
{
  public class TimerViewTests
  {
    private TimerView timerView;
    private TimerModel timerModel;
    private TextMeshProUGUI displayText;
    private Mock<IAudioService> audioService;
    private Subject<long> timerSubject;
    private Button startButton;
    private Button pauseButton;
    private Button resetButton;

    /* [SetUp]
     public void SetUp()
     {
       timerView = new GameObject("TimerView").AddComponent<TimerView>();
       displayText = new GameObject("DisplayText").AddComponent<TextMeshProUGUI>();
       startButton = new GameObject("StartButton").AddComponent<Button>();
       pauseButton = new GameObject("PauseButton").AddComponent<Button>();
       resetButton = new GameObject("ResetButton").AddComponent<Button>();

       SetPrivateField(timerView, "displayText", displayText);
       SetPrivateField(timerView, "startButton", startButton);
       SetPrivateField(timerView, "pauseButton", pauseButton);
       SetPrivateField(timerView, "resetButton", resetButton);

       audioService = new Mock<IAudioService>();
       timerSubject = new Subject<long>();

       timerModel = new TimerModelExtention(audioService.Object, timerSubject);

       timerView.Construct(timerModel);
     }

     private void SetPrivateField(object target, string fieldName, object value)
     {
       var field = target.GetType().GetField(fieldName, System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
       field?.SetValue(target, value);
     }

     [Test]
     public void UpdateUITest()
     {
       // Arrange
       timerModel.SetDuration(10f);
       timerModel.Start();

       // Act
       timerSubject.OnNext(0);

       // Assert
       Assert.AreEqual("00:09", displayText.text);
     }

     [Test]
     public void PlaySFXWhenTimerZeroTest()
     {
       // Arrange
       timerModel.SetDuration(1f);
       timerModel.Start();

       // Act
       timerSubject.OnNext(0);

       // Assert
       Assert.AreEqual("00:00", displayText.text);
       audioService.Verify(a => a.PlaySound("timer_finish"), Times.Once);
     }
   }

   public class TimerModelExtention : TimerModel
   {
     public TimerModelExtention(IAudioService audioService, IObservable<long> updateObservable) : base(audioService, updateObservable)
     {
     }
   }
    */
  }
  }