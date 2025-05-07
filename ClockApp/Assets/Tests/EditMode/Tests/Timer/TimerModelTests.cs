using NUnit.Framework;
using UnityEngine.TestTools;
using Assets.Scripts.CountDownTimer;
using Assets.Scripts.Common;
using Assets.Scripts.Interface;
using Moq;
using UniRx;
using System;
using Assets.Scripts.Clock;
using Assets.Scripts.Enum;

namespace Assets.Tests.EditMode.Timer
{
  public class TimerModelTests 
  {
    private TimerModel timerModel;
    private Mock<IAudioService> audioService;
    private Subject<long> timerSubject;

    [SetUp]
    public void Setup()
    {
      audioService = new Mock<IAudioService>();
      timerSubject = new Subject<long>();
      timerModel = new TimerModel(audioService.Object, timerSubject);
    }

    [Test]
    public void CurrentTimeUpdateTest()
    {
      // Arrange
      var expectedTime = new DateTime(2025, 1, 1, 9, 30, 0);
      var timerSubject = new Subject<long>();
      Func<DateTime> timeProvider = () => expectedTime;

      var clockModel = new ClockModel(timerSubject, timeProvider);

      DateTime? observedTime = null;
      clockModel.CurrentTime.Subscribe(t => observedTime = t);

      // Act
      timerSubject.OnNext(1);

      // Assert
      Assert.AreEqual(expectedTime, observedTime);
    }

    [Test]
    public void ResetTimerTest()
    {
      timerModel.ResetTimer();
      Assert.IsTrue(timerModel.State.Value == TimerState.Stopped);
    }

    [TestCase(10)]
    [TestCase(5)]
    public void SetDurationTest(int expectedValue)
    {
      timerModel.SetDuration(expectedValue);
      Assert.AreEqual(expectedValue, timerModel.RemainingSeconds.Value);
    }

    [Test]
    public void StartTest()
    {
      timerModel.Start();
      Assert.IsTrue(timerModel.State.Value == TimerState.Running);
    }

    [Test]
    public void PauseTest()
    {
      timerModel.Pause();
      Assert.IsTrue(timerModel.State.Value == TimerState.Paused);
    }

  }
}