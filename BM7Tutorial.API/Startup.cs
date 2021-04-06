using AzureFunctions.Extensions.Swashbuckle;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Hosting;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

[assembly: WebJobsStartup(typeof(BM7Tutorial.API.Startup))]
namespace BM7Tutorial.API
{
    public class Startup : IWebJobsStartup
    {
        public void Configure(IWebJobsBuilder builder)
        {
            builder.AddSwashBuckle(Assembly.GetExecutingAssembly(), c => { c.ConfigureSwaggerGen = a => a.SchemaGeneratorOptions.SchemaIdSelector = (type) => type.FullName; });
        }
    }
}
