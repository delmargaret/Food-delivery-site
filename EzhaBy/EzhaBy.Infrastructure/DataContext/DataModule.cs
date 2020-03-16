using Autofac;

using Module = Autofac.Module;

namespace EzhaBy.Infrastructure
{
    public class DataModule : Module
    {
        private string connectionString;
        public DataModule(string connectionString)
        {
            this.connectionString = connectionString;
        }
        protected override void Load(ContainerBuilder builder)
        {
            builder
                .RegisterType<DataContext>()
                .As<IDataContext>()
                .WithParameter("connectionString", connectionString)
                .InstancePerLifetimeScope();
        }
    }
}