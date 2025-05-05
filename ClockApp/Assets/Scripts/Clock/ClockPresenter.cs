using System.Collections;
using UnityEngine;
using VContainer.Unity;

namespace Assets.Scripts.Clock
{
  public class ClockPresenter : IStartable
  {
    private readonly ClockView view;
    private readonly ClockModel model;

    public ClockPresenter(ClockView view, ClockModel model)
    {
      this.view = view;
      this.model = model;
    }

    public void Start() => view.Initialize(model);
  }
}