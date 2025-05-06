using UnityEngine;
using UniRx; 
using TMPro;
using UnityEngine.UI;
using VContainer;
using System;
using Assets.Scripts.Enum;

namespace Assets.Scripts.StopWatch
{
  public class StopWatchView : MonoBehaviour
  {
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private TextMeshProUGUI lappedTimeText;
    [SerializeField] private Toggle onOffToggle;
    [SerializeField] private TextMeshProUGUI onOffToggleLabel;
    [SerializeField] private Button resetButton;
    [SerializeField] private Button recordButton;

    private StopWatchModel model;
    private CompositeDisposable disposables = new();

    [Inject]
    public void Construct(StopWatchModel model)
    {
      this.model = model;
      Initialize();
    }

    private void Initialize()
    {
      model.ElapsedTime
          .Subscribe(ObserveUI)
          .AddTo(disposables);

      model.LappedTime
        .Subscribe(OnLappedTimeChange) 
        .AddTo(disposables);

      onOffToggle.OnValueChangedAsObservable() 
          .Subscribe(OnStopWatchStartOrStop)
          .AddTo(disposables);

      resetButton.OnClickAsObservable()
          .Subscribe(ResetButtonClick)
          .AddTo(disposables);

      recordButton.OnClickAsObservable()
        .Subscribe(OnRecordButtonClick)
        .AddTo(disposables);

      model.State
        .Subscribe(OnTimerStateChange)
        .AddTo(disposables);
    }

    private void OnRecordButtonClick(Unit obj) => model.Record();
    
    private void ObserveUI(TimeSpan time) => displayText.text = time.ToString(@"mm\:ss\.ff");

    private void OnStopWatchStartOrStop(bool isOn)
    {
      if (isOn)
      {
        model.Start();
      }
      else
      { 
        model.Stop();
      }
      onOffToggleLabel.text = isOn ? "STOP" : "START";
    }

    private void OnTimerStateChange(TimerState state) => recordButton.interactable = state == TimerState.Stopped;

    private void OnLappedTimeChange(TimeSpan time) => lappedTimeText.text = time.ToString(@"mm\:ss\.ff");

    private void ResetButtonClick(Unit obj) => model.Reset();
    
    private void OnDestroy()
    {
      disposables.Dispose(); 
      model.Dispose();
    }
  }
}