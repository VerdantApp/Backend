using Autofac;
using Verdant.API.Core.Interfaces;
using Verdant.API.Core.Services;

namespace Verdant.API.Core;

public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<ToDoItemSearchService>()
        .As<IToDoItemSearchService>().InstancePerLifetimeScope();
  }
}
