using System;
using NUnit.Framework;
using UnityEngine;
using TMPro;
using Assets.Scripts.Clock;

namespace Assets.Tests.EditMode.Clock
{
  public class ClockViewTests
  {
    private GameObject clockGO;
    private ClockView clockView;
    private ClockModel clockModel;
    private TextMeshProUGUI displayText;

    [SetUp]
    public void SetUp()
    {
      clockGO = new GameObject("ClockView");
      clockView = clockGO.AddComponent<ClockView>();

      displayText = new GameObject("DisplayText").AddComponent<TextMeshProUGUI>();
      clockView.GetType()
               .GetField("timeText", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
               .SetValue(clockView, displayText);

      clockModel = new ClockModel();

      clockView.Initialize(clockModel);
    }



    [Test]
    public void UpdateUITest()
    {
      // Act
      var testTime = new DateTime(2025, 1, 1, 13, 45, 30);
      clockModel.CurrentTime.Value = testTime;

      // Assert
      var expectedText = testTime.ToString("HH:mm:ss");
      Assert.AreEqual(expectedText, displayText.text);
    }

    [TearDown]
    public void TearDown()
    {
      UnityEngine.Object.DestroyImmediate(clockGO);
      UnityEngine.Object.DestroyImmediate(displayText.gameObject);
    }
  }

}
