using System;
using UniRx;
using UnityEngine;
using Assets.Scripts.Enum;
using VContainer;

namespace Assets.Scripts.StopWatch
{
  public class StopWatchModel : IDisposable
  {
    public ReactiveProperty<TimerState> State { get; } = new(TimerState.Stopped);
    public ReactiveProperty<TimeSpan> ElapsedTime { get; } = new();
    public ReactiveProperty<TimeSpan> LappedTime { get; } = new();

    private CompositeDisposable disposables = new();

    [Inject]
    public StopWatchModel() {
      Observable.EveryUpdate()
        .Where(_ => State.Value == TimerState.Running)
        .Subscribe(UpdateUI)
        .AddTo(disposables);
    }

    // Only for UT
    public StopWatchModel(IObservable<TimeSpan> timerObservable) => 
      timerObservable.Subscribe(time => ElapsedTime.Value = time);

    public void Record() =>
      LappedTime.Value = ElapsedTime.Value;
    
    public void Reset()
    {
      ElapsedTime.Value = TimeSpan.Zero;
      State.Value = TimerState.Stopped;
    }

    public void Start()
    {
      ElapsedTime.Value = TimeSpan.Zero;
      State.Value = TimerState.Running;
    }

    public void Stop() => 
      State.Value = TimerState.Stopped;
    
    public void Dispose() => disposables.Dispose();

    private void UpdateUI(long obj) => ElapsedTime.Value += TimeSpan.FromSeconds(Time.deltaTime);

  }
}