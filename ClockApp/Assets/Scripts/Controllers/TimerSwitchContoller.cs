using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Controllers
{
  public class TimerSwitchContoller : MonoBehaviour
  {
    [SerializeField] GameObject timerUI = null!;
    [SerializeField] GameObject stopWatchUI = null!;
    [SerializeField] Toggle timerToggle = null!;
    [SerializeField] Toggle stopSwitchToggle = null!;

    private void Awake()
    {
      stopSwitchToggle.onValueChanged.AddListener(ControlStopWatchUI);
      timerToggle.onValueChanged.AddListener (ControlTimerUI);
    }

    private void ControlTimerUI(bool isOn) => timerUI.SetActive(isOn);

    private void ControlStopWatchUI(bool isOn) => stopWatchUI.SetActive(isOn);
  }
}