using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Dispatcher;
using BusinessLogic.App_Start;
using MyBusinessLogic.Core.Helpers;

namespace MyBusinessLogic.DependencyResolution
{
    public class UmbracoWebApiHttpControllerActivator : IHttpControllerActivator
    {
        private readonly DefaultHttpControllerActivator _defaultHttpControllerActivator;
        public UmbracoWebApiHttpControllerActivator()
        {
            this._defaultHttpControllerActivator = new DefaultHttpControllerActivator();
        }
        public IHttpController Create(HttpRequestMessage request, HttpControllerDescriptor controllerDescriptor, Type controllerType)
        {
            IHttpController instance =
               ControllersHelper.IsUmbracoController(controllerType)
                  ? this._defaultHttpControllerActivator.Create(request, controllerDescriptor, controllerType)
                  : StructuremapMvc.StructureMapDependencyScope.GetInstance(controllerType) as IHttpController;
            return instance;
        }
    }
}
