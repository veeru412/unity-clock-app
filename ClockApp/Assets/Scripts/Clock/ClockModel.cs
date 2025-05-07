using UniRx;
using System;
using VContainer;

namespace Assets.Scripts.Clock
{
  public class ClockModel : IDisposable
  {
    public ReactiveProperty<DateTime> CurrentTime { get; } = new ReactiveProperty<DateTime>();

    private CompositeDisposable disposables = new CompositeDisposable();

    [Inject]
    public ClockModel()
    {
      Observable.Interval(TimeSpan.FromSeconds(1))
          .Subscribe(UpdateUI)
          .AddTo(disposables);
    }

    // Only for UT
    public ClockModel(IObservable<long> timerObservable, Func<DateTime> timeProvider)
    {
      timerObservable.Subscribe(_ => CurrentTime.Value = timeProvider());
    }

    public void Dispose() => disposables.Dispose();

    private void UpdateUI(long obj) => CurrentTime.Value = DateTime.Now;
  }
}

