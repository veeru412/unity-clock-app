using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.Clock
{
  public class ClockInstaller : LifetimeScope
  {
    protected override void Configure(IContainerBuilder builder)
    {
      UnityEngine.Debug.Log("DI running..");
      builder.Register<ClockModel>(Lifetime.Singleton);
      builder.RegisterComponentInHierarchy<ClockView>();

      builder.RegisterEntryPoint<ClockPresenter>(); 
    }
  }
}