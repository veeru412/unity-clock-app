using Assets.Scripts.StopWatch;
using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using TMPro;
using NUnit.Framework;

namespace Assets.Tests.PlayMode
{
  public class StopWatchTests
  {
    private const string timerToggleObjectPath = "StopWatchUI/StopwatchButtons/OnOffToggle";
    private const string displayTextPath = "StopWatchUI/TimerText";

    [UnityTest]
    public IEnumerator StopWatchTest()
    {
      // Arrange
      var toggle = PlaymodeTestsHelper.GetComponentFromChild<Toggle>(timerToggleObjectPath);
      var displayText = PlaymodeTestsHelper.GetComponentFromChild<TextMeshProUGUI>(displayTextPath);
      var initialText = displayText.text;

      // Act
      toggle.isOn = true;
      yield return new WaitForSeconds(1f);

      // Assert
      Assert.AreNotEqual(initialText, displayText.text);
    }
  }
}