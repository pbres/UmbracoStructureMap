using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using MyBusinessLogic.Core.Services;
using Umbraco.Web.WebApi;

namespace MyBusinessLogic.Controllers.API
{
    public class SampleApiController : UmbracoApiController
    {
        private readonly ISampleService _sampleService;

        public SampleApiController(ISampleService sampleService)
        {
            _sampleService = sampleService;
        }

        public HttpResponseMessage GetSampleData()
        {
            return Request.CreateResponse(HttpStatusCode.OK, _sampleService.SampleData);
        }
    }
}
