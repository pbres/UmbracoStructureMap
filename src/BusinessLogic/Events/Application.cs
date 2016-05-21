using System.Web.Http;
using System.Web.Http.Dispatcher;
using MyBusinessLogic.DependencyResolution;
using Umbraco.Core;

namespace MyBusinessLogic.Events
{
    public class Application : ApplicationEventHandler
    {
        protected override void ApplicationStarting(UmbracoApplicationBase umbracoApplication, ApplicationContext applicationContext)
        {
            GlobalConfiguration.Configuration.Services.
               Replace(typeof(IHttpControllerActivator), new UmbracoWebApiHttpControllerActivator());
        }
    }
}
