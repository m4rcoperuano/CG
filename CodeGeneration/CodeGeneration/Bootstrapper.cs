using System.Web.Mvc;
using Microsoft.Practices.Unity;
using Unity.Mvc4;
using CodeGeneration.Domain.RepositoryInterfaces;
using CodeGeneration.Infrastructure.RepositoryImplementations;
using CodeGeneration.Interfaces;
using CodeGeneration.Services;
using CodeGeneration.Membership;

namespace CodeGeneration
{
    public static class Bootstrapper
    {
        public static IUnityContainer UnityContainer;
        public static IUnityContainer Initialise()
        {
            var container = BuildUnityContainer();
            Bootstrapper.UnityContainer = container;

            DependencyResolver.SetResolver(new UnityDependencyResolver(container));
            return container;
        }

        private static IUnityContainer BuildUnityContainer()
        {
            var container = new UnityContainer();
            RegisterTypes(container);

            return container;
        }

        public static void RegisterTypes(IUnityContainer container)
        {
            container.RegisterType<IUserRepository, UserRepository>();
            container.RegisterType<ICodeGenerationRepository, CodeGenerationRepository>();
            container.RegisterType<ISystemClock, SystemClockService>();
            container.RegisterType<IMembership, CGMembership>();
        }
    }
}