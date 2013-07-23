using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Ninject;
using Moq;
using ChART.DataAccess.Abstract;
using ChART.Domain.Entities;

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
            //Bindings go here
            Mock<IStationRepository> mock = new Mock<IStationRepository>();
            mock.Setup(m => m.Stations).Returns(new List<Station> { 
                new Station{ Name = "Central Norte"},
                new Station{ Name = "Central Sur"}
            }.AsQueryable());

            ninjectKernel.Bind<IStationRepository>().ToConstant(mock.Object);
        }
    }
}