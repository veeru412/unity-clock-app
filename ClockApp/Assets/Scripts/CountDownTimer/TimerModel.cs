using UniRx;
using System;
using UnityEngine;
using Assets.Scripts.Interface;
using Assets.Scripts.Enum;

namespace Assets.Scripts.CountDownTimer
{ 
  public class TimerModel : IDisposable
  {
    public ReactiveProperty<TimerState> State { get; } = new(TimerState.Stopped);
    public ReactiveProperty<float> RemainingSeconds { get; } = new(0f);
    public ReactiveCommand TimerFinished { get; } = new(); 

    private readonly IAudioService audioService;
    private CompositeDisposable disposables = new();
    private const string finishTimerClip = "timer_finish";
    private float initialDuration = 0f;

    public TimerModel(IAudioService audioService)
    {
      this.audioService = audioService;

      Observable.EveryUpdate()
          .Where(_ => State.Value == TimerState.Running)
          .Subscribe(UpdateUI)
          .AddTo(disposables);
    }

    public void ResetTimer()
    {
      State.Value = TimerState.Stopped;
      RemainingSeconds.Value = initialDuration;
    }

    public void SetDuration(float seconds)
    {
      RemainingSeconds.Value = seconds;
      initialDuration = seconds;
    }

    public void Start() => State.Value = TimerState.Running;

    public void Pause() => State.Value = TimerState.Paused;

    public void Dispose() => disposables?.Dispose();

    private void UpdateUI(long obj)
    {
      RemainingSeconds.Value -= Time.deltaTime;
      if (RemainingSeconds.Value <= 0f)
      { 
        FinishTimer();
      }
    }

    private void FinishTimer()
    {
      RemainingSeconds.Value = 0f;
      State.Value = TimerState.Stopped;
      TimerFinished.Execute();
      audioService.PlaySound(finishTimerClip);
    }
  }
}