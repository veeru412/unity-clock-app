using Assets.Scripts.Common;
using Assets.Scripts.Interface;
using VContainer;
using VContainer.Unity;

namespace Assets.Scripts.CountDownTimer
{
  public class TimerInstaller : LifetimeScope
  {
    protected override void Configure(IContainerBuilder builder)
    {
      builder.Register<AudioService>(Lifetime.Singleton).As<IAudioService>();
      builder.Register<TimerModel>(Lifetime.Singleton);
      builder.RegisterComponentInHierarchy<TimerView>();
    }
  }
}