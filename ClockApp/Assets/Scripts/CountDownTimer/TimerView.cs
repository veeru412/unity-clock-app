using System;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts.Enum;
using VContainer;

namespace Assets.Scripts.CountDownTimer
{
  public class TimerView : MonoBehaviour
  {
    [SerializeField] private TMP_Text displayText;
    [SerializeField] private Button startButton, pauseButton, resetButton;
    [SerializeField] private float duration = 60;

    private TimerModel model;

    /// <summary>
    /// this function is only E2E. just make test runs quicker we are overiding the duration.
    /// so that test can execute quickly.
    /// </summary>
    /// <param name="duration"></param>
    public void SetDuration(float duration) => model.SetDuration(duration);

    [Inject]
    public void Construct(TimerModel model)
    {
      this.model = model;
      Initialize(); 
    }
    private void Initialize()
    {
      model.SetDuration(duration);
      model.RemainingSeconds
          .Subscribe(OnObserveTimerChange)
          .AddTo(this);

      InitializeButtons();
    }

    private void InitializeButtons()
    {
      startButton.OnClickAsObservable().Subscribe(_ => model.Start()).AddTo(this);
      pauseButton.OnClickAsObservable().Subscribe(_ => model.Pause()).AddTo(this);
      resetButton.OnClickAsObservable().Subscribe(_ => model.ResetTimer()).AddTo(this);

      model.State.Subscribe(OnObserveTimerState).AddTo(this);
    }

    private void OnObserveTimerChange(float seconds)
    {
      displayText.text = FormatTime(seconds);
      displayText.color = seconds <= 5 ? Color.red : Color.green;
    }

    private void OnObserveTimerState(TimerState state)
    {
      startButton.interactable = state != TimerState.Running;
      pauseButton.interactable = state == TimerState.Running;
    }

    private string FormatTime(float seconds) =>
        TimeSpan.FromSeconds(seconds).ToString(@"mm\:ss");
  }
}