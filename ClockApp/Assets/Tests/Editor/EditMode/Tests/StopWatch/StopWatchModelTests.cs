using System;
using NUnit.Framework;
using Assets.Scripts.StopWatch;
using UniRx;
using Assets.Scripts.Enum;

namespace Assets.Tests.EditMode.StopWatch
{
  public class StopWatchModelTests
  {
    private StopWatchModel model;
    private Subject<TimeSpan> timerSubject;

    [SetUp]
    public void Setup()
    {
      timerSubject = new Subject<TimeSpan>();
      model = new StopWatchModel(timerSubject);
    }

    [Test]
    public void TimerUpdateTest()
    {
      TimeSpan observedTime = TimeSpan.FromSeconds(10);

      // Act
      timerSubject.OnNext(observedTime);

      // Assert
      Assert.AreEqual(observedTime, model.ElapsedTime.Value);
    }

    [Test]
    public void RecordTest()
    {
      model.Record();
      Assert.AreEqual(model.ElapsedTime.Value, model.LappedTime.Value);
    }

    [Test]
    public void ResetTest()
    {
      model.Reset();
      Assert.AreEqual(model.State.Value, TimerState.Stopped);
    }

    [Test]
    public void StartTest()
    {
      model.Start();
      Assert.AreEqual(model.State.Value, TimerState.Running);
    }

    [Test]
    public void StopTest()
    {
      model.Stop();
      Assert.AreEqual(model.State.Value, TimerState.Stopped);
    }
  }
}
