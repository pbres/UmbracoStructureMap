using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using MyBusinessLogic.Core.Services;
using Umbraco.Web.Mvc;

namespace MyBusinessLogic.Controllers.Surface
{
    public class  SampleSurfaceController : SurfaceController
    {
        private readonly ISampleService _sampleService;

        public SampleSurfaceController(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        public ActionResult SampleSurface()
        {
            ViewData["sampleData"] = _sampleService.SampleData;
            return PartialView("SamplePartialView");
        }
    }
}
