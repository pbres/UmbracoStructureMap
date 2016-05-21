using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyBusinessLogic.Core.Services
{
    public class SampleService : ISampleService
    {
        public string[] SampleData
        {
            get { return new[] {"sample 1", "sample 2"}; }
            set { }
        }
    }
}
