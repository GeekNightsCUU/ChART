using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Moq;
using ChART.DataAccess.Abstract;
using ChART.DataAccess.Concrete;
using ChART.Domain.Entities;
using System.Configuration;

namespace ChARTServices.Infrastructure
{
    public class NinjectControllerFactory : DefaultControllerFactory
    {
        private IKernel ninjectKernel;

        public NinjectControllerFactory()
        {
            ninjectKernel = new StandardKernel();
            AddBindings();
        }

        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            return controllerType == null
                ? null
                : (IController)ninjectKernel.Get(controllerType);        
        }

        private void AddBindings()
        {
            ninjectKernel.Bind<IStationRepository>().To<MongoStationRepository>().WithConstructorArgument("connectionString", ConfigurationManager.AppSettings.Get("MONGOLAB_URI"));
        }
    }
}