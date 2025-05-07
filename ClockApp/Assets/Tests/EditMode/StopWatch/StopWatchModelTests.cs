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
    private Subject<long> timerSubject;
    private TimeSpan expectedTime = new TimeSpan(10);

    [SetUp]
    public void Setup()
    {
      timerSubject = new Subject<long>();
      model = new StopWatchModel(timerSubject, () => expectedTime);
    }

    [Test]
    public void TimerUpdateTest()
    {
      TimeSpan observedTime = default;
      model.ElapsedTime.Subscribe(t => observedTime = t);

      // Act
      timerSubject.OnNext(0);

      // Assert
      Assert.AreEqual(expectedTime, observedTime);
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
