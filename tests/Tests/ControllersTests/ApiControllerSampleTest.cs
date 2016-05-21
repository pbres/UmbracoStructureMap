using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using FakeItEasy;
using MyBusinessLogic.Controllers.API;
using MyBusinessLogic.Core.Services;
using Umbraco.Core;
using Umbraco.Core.Configuration.UmbracoSettings;
using Umbraco.Core.Logging;
using Umbraco.Core.Persistence;
using Umbraco.Core.Persistence.SqlSyntax;
using Umbraco.Core.Profiling;
using Umbraco.Core.Services;
using Umbraco.Web;
using Umbraco.Web.Routing;
using Umbraco.Web.Security;
using Xunit;

namespace Tests.ControllersTests
{
    public class ApiControllerSampleTest
    {
        private readonly ISampleService _sampleService;

        public ApiControllerSampleTest()
        {
            _sampleService = A.Fake<ISampleService>();

            var appCtx = ApplicationContext.EnsureContext(
                new DatabaseContext(A.Fake<IDatabaseFactory>(), A.Fake<ILogger>(), new SqlSyntaxProviders(new[] { A.Fake<ISqlSyntaxProvider>() })),
                new ServiceContext(),
                CacheHelper.CreateDisabledCacheHelper(),
                new ProfilingLogger(A.Fake<ILogger>(), A.Fake<IProfiler>()), true);

            var ctx = UmbracoContext.EnsureContext(
                A.Fake<HttpContextBase>(),
                appCtx,
                A.Fake<WebSecurity>(),
                A.Fake<IUmbracoSettingsSection>(),
                Enumerable.Empty<IUrlProvider>(), true);
        }

        [Fact]
        public void sample_api_controller_returns_sample_data()
        {
            var api_controller = new SampleApiController(_sampleService);
            api_controller.Request = new HttpRequestMessage();
            api_controller.Configuration = new HttpConfiguration();

            A.CallTo(() => _sampleService.SampleData).Returns(new string[] { "test data 1", "test data 2" });

            var result = api_controller.GetSampleData();

            string[] httpContent;
            Assert.Equal(result.TryGetContentValue<string[]>(out httpContent), true);
            Assert.Equal(httpContent, new[] { "test data 1", "test data 2" });
        }

        [Fact]
        public void sample_api_controller_returns_httpStatus_ok()
        {
            var api_controller = new SampleApiController(_sampleService);
            api_controller.Request = new HttpRequestMessage();
            api_controller.Configuration = new HttpConfiguration();

            A.CallTo(() => _sampleService.SampleData).Returns(new string[] { "test data 1", "test data 2" });

            var result = api_controller.GetSampleData();

            Assert.Equal(result.StatusCode, HttpStatusCode.OK);
        }
    }
}
