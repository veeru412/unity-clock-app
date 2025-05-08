using Assets.Scripts.CountDownTimer;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using NUnit.Framework;

namespace Assets.Tests.PlayMode
{
  public class TimerTests 
  {
    private const string timerStartButtonPath = "TimerUI/TimerButtons/StartButton";
    private const string timerTextPath = "TimerUI/CountDownTimerText";

    [UnityTest]
    public IEnumerator TimerCompleteTest()
    {
      // Arrange
      var timerView = GameObject.FindObjectOfType<TimerView>();
     
      var startButton = PlaymodeTestsHelper.GetComponentFromChild<Button>(timerStartButtonPath);
      var timerText = PlaymodeTestsHelper.GetComponentFromChild<TextMeshProUGUI>(timerTextPath);

      // Act
      timerView.SetDuration(1);
      startButton.onClick.Invoke(); // simulating button click
      yield return new WaitForSeconds(1.2f);

      // Assert
      Assert.AreEqual("00:00", timerText.text);
    }
  }
}