using TMPro;
using UnityEngine;
using UniRx;
using System;

namespace Assets.Scripts.Clock
{
  public class ClockView : MonoBehaviour
  {
    [SerializeField] private TMP_Text timeText;
    private ClockModel model;

    public void Initialize(ClockModel model)
    {
      this.model = model;
      this.model.CurrentTime.Subscribe(OnObserveData).AddTo(this); 
    }

    private void OnObserveData(DateTime time) =>    
      timeText.text = time.ToString("HH:mm:ss");

    private void OnDestroy() => model?.Dispose();
  }
}