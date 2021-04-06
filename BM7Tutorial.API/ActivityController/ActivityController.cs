using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BM7Tutorial.DAL;
using BM7Tutorial.DAL.Model;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.EventGrid.Models;
using Microsoft.Azure.EventHubs;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Nexus.Base.EventHubExtensions;
using static BM7Tutorial.DAL.Repositories;

namespace BM7Tutorial.API.ActivityController
{
    public class ActivityController
    {
        /*[FunctionName("ActivityController")]
        public async Task Run(
            [EventHubTrigger("class.activity", Connection = "evh-bl-tutorial-dhanar")] EventData[] events,
            [CosmosDB(ConnectionStringSetting = "cosmos-bl-tutorial-serverless")] DocumentClient documentClient,
            ILogger log)
        {
            var exceptions = new List<Exception>();

            var eventList = events.GetData<EventGridEvent>();
            var repsActivity = new ActivityRepository(documentClient);

            foreach (var eventData in eventList)
            {
                try
                {
                    var data = JsonConvert.DeserializeObject<Class>(eventData.Data.ToString());
                    Activity activity;

                    switch (eventData.Subject)
                    {
                        case "Create/":
                            activity = new Activity
                            {
                                ClassId = data.Id,
                                ActivityName = $"Menambahkan class dengan ClassCode : {data.ClassCode}",
                                Description = data.Description
                            };

                            await repsActivity.CreateAsync(activity);

                            log.LogInformation($"Sukses menambahkan \"{activity.ActivityName}\"");
                            break;
                        case "Update/":
                            activity = (await repsActivity.GetAsync(p => p.ClassId == data.Id)).Items.FirstOrDefault();
                            if (activity == null)
                            {
                                exceptions.Add(new Exception("Class not found"));
                                break;
                            }

                            activity.ActivityName = $"Menambahkan class dengan ClassCode : {data.ClassCode}";
                            activity.Description = data.Description;

                            await repsActivity.UpdateAsync(activity.Id, activity);

                            log.LogInformation($"Sukses update \"{activity.ActivityName}\"");
                            break;
                        default:
                            exceptions.Add(new Exception("Subject must be \"Create/\" or \"Update/\""));
                            break;
                    }
                }
                catch (Exception e)
                {
                    exceptions.Add(e);
                }
            }

            // Once processing of the batch is complete, if any messages in the batch failed processing throw an exception so that there is a record of the failure.

            if (exceptions.Count > 1)
                throw new AggregateException(exceptions);

            if (exceptions.Count == 1)
                throw exceptions.Single();
        }*/
    }
}
