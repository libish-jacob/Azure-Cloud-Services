using System.Net;
using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;

namespace AzureFunctionApp
{
    public static class AnotherFunction
    {
        [FunctionName("AnotherFunction")]
        public static async Task<object> Run([HttpTrigger(WebHookType = "genericJson")]HttpRequestMessage req, TraceWriter log)
        {
            log.Info($"Webhook was triggered!");

            string jsonContent = await req.Content.ReadAsStringAsync();
            dynamic data = JsonConvert.DeserializeObject(jsonContent);

            if (data == null || data.first == null || data.last == null)
            {
                return req.CreateResponse(HttpStatusCode.BadRequest, new
                {
                    error = "Please pass first/last properties in the input object"
                });
            }

            return req.CreateResponse(HttpStatusCode.OK, new
            {
                greeting = $"Hello {data.first} {data.last}!"
            });
        }
    }
}
