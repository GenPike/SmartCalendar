using Ninject;
using Ninject.Modules;
using Ninject.Syntax;
using SmartCalendar.Models.Abstracts;
using SmartCalendar.Models.EFRepositories;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Web;
using System.Web.Http.Dependencies;
using System.Web.Mvc;

namespace SmartCalendar.Infrastructure
{
    public class NinjectRegistrations : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository>().To<EventRepository>();
        }
    }

    public class NinjectDependencyResolver : NinjectDependencyScope, 
        System.Web.Http.Dependencies.IDependencyResolver, System.Web.Mvc.IDependencyResolver
    {
        private readonly IKernel kernel;

        public NinjectDependencyResolver(IKernel kernel)
            : base(kernel)
        {
            this.kernel = kernel;
        }

        public IDependencyScope BeginScope()
        {
            return new NinjectDependencyScope(this.kernel.BeginBlock());
        }
    }

    public class NinjectDependencyScope : IDependencyScope
    {
        private IResolutionRoot resolver;

        internal NinjectDependencyScope(IResolutionRoot resolver)
        {
            Contract.Assert(resolver != null);

            this.resolver = resolver;
        }

        public void Dispose()
        {
            var disposable = this.resolver as IDisposable;
            if (disposable != null)
            {
                disposable.Dispose();
            }

            this.resolver = null;
        }

        public object GetService(Type serviceType)
        {
            if (this.resolver == null)
            {
                throw new ObjectDisposedException("this", "This scope has already been disposed");
            }

            return this.resolver.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            if (this.resolver == null)
            {
                throw new ObjectDisposedException("this", "This scope has already been disposed");
            }

            return this.resolver.GetAll(serviceType);
        }
    }
}