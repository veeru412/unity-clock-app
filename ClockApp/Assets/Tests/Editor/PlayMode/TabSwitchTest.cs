using System.Collections;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;
using NUnit.Framework;

namespace Assets.Tests.PlayMode
{
  public class TabSwitchTest
  {
    private const string timerTogglePath = "TimerToggle";
    private const string stopwatchTogglepath = "StopWatch";
    private const string timerUIPath = "TimerUI";
    private const string stopwatchUIPath = "StopWatchUI";

    [UnityTest]
    public IEnumerator TimerSwitchTest()
    {
      //arrange
      var timerToggle = GameObject.Find(timerTogglePath).GetComponent<Toggle>();
      var stopwatchToggle = GameObject.Find(stopwatchTogglepath).GetComponent<Toggle>();
      var timerUIObject = PlaymodeTestsHelper.GetComponentFromChild<Transform>(timerUIPath).gameObject;
      var stopWatchUI = PlaymodeTestsHelper.GetComponentFromChild<Transform>(stopwatchUIPath).gameObject;

      //act
      timerToggle.isOn= true;
      yield return null;

      //assert
      Assert.IsTrue(timerUIObject.activeInHierarchy);
      Assert.IsFalse(stopWatchUI.activeInHierarchy);

      //act
      stopwatchToggle.isOn = true;

      //assert
      Assert.IsFalse(timerUIObject.activeInHierarchy);
      Assert.IsTrue(stopWatchUI.activeInHierarchy);
    }
  }
}