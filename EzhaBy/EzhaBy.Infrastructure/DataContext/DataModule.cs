using Autofac;

using Module = Autofac.Module;

namespace EzhaBy.Infrastructure
{
    public class DataModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<DataContext>()
                .As<IDataContext>()
                .InstancePerLifetimeScope();
        }
    }
}