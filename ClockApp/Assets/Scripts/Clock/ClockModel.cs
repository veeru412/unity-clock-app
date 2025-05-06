using UniRx;
using System;

namespace Assets.Scripts.Clock
{
  public class ClockModel : IDisposable
  {
    public ReactiveProperty<DateTime> CurrentTime { get; } = new ReactiveProperty<DateTime>();

    private CompositeDisposable disposables = new CompositeDisposable();

    public ClockModel()
    {
      Observable.Interval(TimeSpan.FromSeconds(1))
          .Subscribe(UpdateUI)
          .AddTo(disposables);
    }

    public void Dispose() => disposables.Dispose();

    private void UpdateUI(long obj) => CurrentTime.Value = DateTime.Now;
  }
}

