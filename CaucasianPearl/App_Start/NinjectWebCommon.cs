using System.Collections.Generic;
using System.Web.Mvc;
using CaucasianPearl.Core.DAL;
using CaucasianPearl.Core.DAL.Interface;
using CaucasianPearl.Core.DAL.Repository;
using CaucasianPearl.Core.EntityServices;
using CaucasianPearl.Core.EntityServices.Interface;
using CaucasianPearl.Core.Services;
using CaucasianPearl.Core.Services.FlickrNetService;
using CaucasianPearl.Core.Services.LoggingService;
using CaucasianPearl.Models.EDM;
using Ninject.Parameters;

[assembly: WebActivator.PreApplicationStartMethod(typeof(CaucasianPearl.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(CaucasianPearl.App_Start.NinjectWebCommon), "Stop")]

namespace CaucasianPearl.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            DependencyResolver.SetResolver(new NinjectDependancyResolver(kernel: kernel));
            RegisterServices(kernel);

            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            #region Repository

            kernel.Bind<IRepository<Event>>().To<Repository<Event>>();
            kernel.Bind<IRepository<EventMedia>>().To<Repository<EventMedia>>();
            kernel.Bind<IRepository<Sponsor>>().To<Repository<Sponsor>>();
            kernel.Bind<IRepository<Feedback>>().To<Repository<Feedback>>();
            kernel.Bind<IRepository<Request>>().To<Repository<Request>>();
            kernel.Bind<IRepository<SiteSetting>>().To<Repository<SiteSetting>>();
            kernel.Bind<IRepository<Profile>>().To<Repository<Profile>>();
            kernel.Bind<IRepository<ContentBlock>>().To<Repository<ContentBlock>>();
            
            #endregion

            #region EntityServices

            kernel.Bind<IEventService<Event>>().To<EventEntityService>();
            kernel.Bind<IOrderedService<EventMedia>>().To<EventMediaEntityService>();
            kernel.Bind<IBaseService<Sponsor>>().To<SponsorEntityService>();
            kernel.Bind<IFeedbackService<Feedback>>().To<FeedbackEntityService>();
            kernel.Bind<IBaseService<SiteSetting>>().To<SiteSettingsEntityService>();
            kernel.Bind<IOrderedService<ContentBlock>>().To<ContentBlockEntityService>();
            kernel.Bind<ISponsorService<Sponsor>>().To<SponsorEntityService>();
            kernel.Bind<IProfileService<Profile>>().To<ProfileEntityService>();
            kernel.Bind<IBaseService<Request>>().To<RequestEntityService>();
            
            #endregion

            #region Services

            kernel.Bind<ILogService>().To<LogService>();
            kernel.Bind<IFlickrService>().To<FlickrService>();

            #endregion
        }

        public class NinjectDependancyResolver : IDependencyResolver
        {
            private IKernel _kernel;

            public NinjectDependancyResolver(IKernel kernel)
            {
                _kernel = kernel;
            }

            public object GetService(Type serviceType)
            {
                return _kernel.TryGet(serviceType, new IParameter[0]);
            }

            public IEnumerable<object> GetServices(Type serviceType)
            {
                return _kernel.GetAll(serviceType, new IParameter[0]);
            }
        }
    }
}