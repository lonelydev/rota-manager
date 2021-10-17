using System;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;

namespace StandupRota
{
    public class StandupRota
    {
        public StandupRota()
        {
        }

        [FunctionName("StandupRota")]
        public Task Run([TimerTrigger("0 */5 * * * *")]TimerInfo myTimer, ILogger log)
        {
            log.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            return Task.CompletedTask;
        }
    }
}
