using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBusinessLogic.Core.Helpers
{
    public static class ControllersHelper
    {
        public static bool IsUmbracoController(Type controllerType)
        {
            return controllerType.Namespace != null
               && controllerType.Namespace.StartsWith("umbraco", StringComparison.InvariantCultureIgnoreCase)
               && !controllerType.Namespace.StartsWith("umbraco.extensions", StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
