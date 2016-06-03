using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using FakeItEasy;
using MyBusinessLogic.Controllers.Surface;
using MyBusinessLogic.Core.Services;
using Umbraco.Core;
using Umbraco.Core.Configuration.UmbracoSettings;
using Umbraco.Core.Logging;
using Umbraco.Core.Models;
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
    public class SurfaceSampleTest
    {
        private readonly ISampleService _sampleService;
        private readonly IPublishedContent _publishedContent;
        private readonly UmbracoContext _ctx;
        private readonly RouteData _routeData;

        public SurfaceSampleTest()
        {
            _sampleService = A.Fake<ISampleService>();
            _publishedContent = A.Fake<IPublishedContent>();

            A.CallTo(() => _publishedContent.Id).Returns(1234);
            A.CallTo(() => _publishedContent.Name).Returns("Test");

            var fakeViewEngine = A.Fake<IViewEngine>();
            A.CallTo(() => fakeViewEngine.FindView(null, null, null, false))
                .WithAnyArguments()
                .Returns(new ViewEngineResult(A.Fake<IView>(), fakeViewEngine));

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(fakeViewEngine);

            var appCtx = ApplicationContext.EnsureContext(
                new DatabaseContext(A.Fake<IDatabaseFactory>(), A.Fake<ILogger>(), new SqlSyntaxProviders(new[] { A.Fake<ISqlSyntaxProvider>() })),
                new ServiceContext(),
                CacheHelper.CreateDisabledCacheHelper(),
                new ProfilingLogger(A.Fake<ILogger>(), A.Fake<IProfiler>()), true);

            _ctx = UmbracoContext.EnsureContext(
                A.Fake<HttpContextBase>(),
                appCtx,
                A.Fake<WebSecurity>(),
                A.Fake<IUmbracoSettingsSection>(),
                Enumerable.Empty<IUrlProvider>(), true);

            var webRoutingSection = A.Fake<IWebRoutingSection>();
            A.CallTo(() => webRoutingSection.UrlProviderMode).Returns(UrlProviderMode.AutoLegacy.ToString());
            _ctx.PublishedContentRequest = new PublishedContentRequest(new Uri("http://test.com"), _ctx.RoutingContext,
                webRoutingSection,
                s => new string[] { })
            {
                PublishedContent = _publishedContent
            };

            _routeData = new RouteData();
            _routeData.Values.Add("action", "Home");
            _routeData.Values.Add("controller", "Home");
            _routeData.DataTokens.Add(Umbraco.Core.Constants.Web.PublishedDocumentRequestDataToken, _ctx.PublishedContentRequest);
        }

        [Fact]
        public void sampleSurface_controller_sampleSurface_action_has_sampleData_in_viewData()
        {
            var surface_controller = new SampleSurfaceController(_sampleService);
            surface_controller.ControllerContext = new System.Web.Mvc.ControllerContext(_ctx.HttpContext, _routeData, surface_controller);

            A.CallTo(() => _sampleService.SampleData).Returns(new[] { "test data 1", "test data 2" });

            surface_controller.SampleSurface();

            var result = surface_controller.ViewData["sampleData"];
            Assert.Equal(new string[] { "test data 1", "test data 2" }, result);


        }
    }
}
