using System;
using Assets.Scripts.Clock;
using NUnit.Framework;
using UniRx;

namespace Assets.Tests.EditMode.Clock
{
  public class ClockModelTests
  {
    [Test]
    public void CurrentTimeUpdateTest()
    {
      var testTimer = new Subject<long>();
      var expectedTime = new DateTime(2025, 1, 1, 9, 30, 0);
      var model = new ClockModelExtention(testTimer, () => expectedTime);

      DateTime observedTime = default;
      model.CurrentTime.Subscribe(t => observedTime = t);

      // Act
      testTimer.OnNext(0);

      // Assert
      Assert.AreEqual(expectedTime, observedTime);
    }
  }

  public class ClockModelExtention : ClockModel
  {
    public ClockModelExtention(IObservable<long> timerObservable, Func<DateTime> timeProvider) { }
  }
}

