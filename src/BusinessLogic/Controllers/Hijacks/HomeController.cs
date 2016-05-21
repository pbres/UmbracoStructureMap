using System.Web.Mvc;
using MyBusinessLogic.Core.Services;
using Umbraco.Web;
using Umbraco.Web.Models;
using Umbraco.Web.Mvc;

namespace MyBusinessLogic.Controllers.Hijacks
{
    public class HomeController : RenderMvcController
    {
        private readonly ISampleService _sampleService;

        public HomeController(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        public override ActionResult Index(RenderModel model)
        {
            ViewData["sampleData"] = _sampleService.SampleData;
            //Do some stuff here, then return the base method
            return base.Index(model);
        }

        public ActionResult SampleAction()
        {
            ViewData["sampleData"] = _sampleService.SampleData;
            return View("SampleView");
        }
    }
}
