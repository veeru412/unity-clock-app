using Assets.Scripts.Common;
using Assets.Scripts.CountDownTimer;
using Assets.Scripts.Interface;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.StopWatch
{
  public class StopWatchInstaller : LifetimeScope
  {
    protected override void Configure(IContainerBuilder builder)
    {
      builder.Register<StopWatchModel>(Lifetime.Singleton);
      builder.RegisterComponentInHierarchy<StopWatchView>();
    }
  }
}