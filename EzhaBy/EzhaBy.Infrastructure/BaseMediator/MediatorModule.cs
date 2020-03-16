using System;
using System.Linq;
using System.Reflection;
using Autofac;
using FluentValidation;
using MediatR;

using Module = Autofac.Module;

namespace NetCoreCqs.Infrastructure.BaseMediator
{
    public class MediatorModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = GetAllProjectAssemblies();
            builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(IRequest<>)).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(IRequestHandler<>))
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(IRequestHandler<,>))
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(IPipelineBehavior<,>))
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(AsyncRequestHandler<>))
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(INotificationHandler<>))
                .AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(IValidator<>)).AsImplementedInterfaces();
            builder.RegisterAssemblyTypes(assemblies).AsClosedTypesOf(typeof(AbstractValidator<>))
                .AsImplementedInterfaces();

            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(componentContext =>
            {
                var context = componentContext.Resolve<IComponentContext>();
                return type => context.ResolveOptional(type);
            });
        }

        private Assembly[] GetAllProjectAssemblies()
        {
            var entryAssembly = Assembly.GetEntryAssembly();
            var assemblies = entryAssembly
                .GetReferencedAssemblies()
                .Where(it => it.FullName.StartsWith(nameof(EzhaBy), StringComparison.CurrentCultureIgnoreCase))
                .Select(Assembly.Load)
                .ToList();
            assemblies.Add(entryAssembly);
            return assemblies.ToArray();
        }
    }
}