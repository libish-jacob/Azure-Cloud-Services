using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;

namespace AzureFunctionApp
{
    public static class TimerFunction
    {
        [FunctionName("Function1")]
        public static void Run([TimerTrigger("* * * * * *")]TimerInfo myTimer, TraceWriter log)
        {/*It requires us to create a local.settings.json file. How to create it automatically is yet to figure out. Figure out if there is a configuration tool for this.
            */
            log.Info($"C# Timer trigger function executed at: {DateTime.Now}");
        }
    }
}
