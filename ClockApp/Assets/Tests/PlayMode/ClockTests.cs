using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;
using TMPro;
using Assets.Scripts.CountDownTimer;
using UnityEngine.UI;
using Assets.Scripts.StopWatch;
using System.Threading.Tasks;

namespace Assets.Tests.PlayMode
{
  public class ClockTests
  {
    private const string clockTextPath = "ClockTimerText";

    [UnitySetUp]
    public IEnumerator Setup()
    {
      yield return PlaymodeTestsHelper.LoadTestSceneCoroutine();
    }

    [UnityTest]
    public IEnumerator ClockUpdateTest()
    {
      var clockText = PlaymodeTestsHelper.GetComponentFromChild<TextMeshProUGUI>(clockTextPath);

      // Arrange
      var initialText = clockText.text;

      // Act
      yield return new WaitForSeconds(1.1f);

      // Assert
      Assert.AreNotEqual(initialText, clockText.text,
          "Clock text should update every second");
    }
  }
}