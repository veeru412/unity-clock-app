using UniRx;
using System;

namespace Assets.Scripts.Clock
{
  public class ClockModel : IDisposable
  {
    public ReactiveProperty<DateTime> CurrentTime { get; } = new ReactiveProperty<DateTime>();
    private CompositeDisposable _disposables = new CompositeDisposable();

    public ClockModel()
    {
      Observable.Interval(TimeSpan.FromSeconds(1))
          .Subscribe(_ => CurrentTime.Value = DateTime.Now)
          .AddTo(_disposables);
    }

    public void Dispose() => _disposables.Dispose();
  }
}

