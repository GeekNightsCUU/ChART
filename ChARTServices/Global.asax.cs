﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Routing;
using ChARTServices.Infrastructure;

namespace ChARTServices
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);

            var ninjectControllerFactory = new NinjectControllerFactory();
            ControllerBuilder.Current.SetControllerFactory(ninjectControllerFactory);
            GlobalConfiguration.Configuration.DependencyResolver = new NinjectDependencyResolver(ninjectControllerFactory.NinjectKernel);            
        }

       
    }
}