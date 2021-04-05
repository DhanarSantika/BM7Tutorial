using System;
using System.IO;
using System.Threading.Tasks;
using BM7Tutorial.API.CRUD.DTO;
using BM7Tutorial.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BM7Tutorial.API.CRUD
{
    public static class CRUD
    {
        [FunctionName("CRUD_GetAllClass")]
        public static async Task<IActionResult> GetAll(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Class")] HttpRequest req,
            [CosmosDB(ConnectionStringSetting = "cosmos-bl-tutorial-serverless")] DocumentClient documentClient,
            ILogger log)
        {
            log.LogInformation("Triggering CRUD_GetAllClass by HTTP Trigger");

            try
            {
                var query = new SqlQuerySpec("SELECT * FROM c");
                var pk = new PartitionKey("Class/");
                var options = new FeedOptions() { PartitionKey = pk };
                var data = documentClient.CreateDocumentQuery(UriFactory.CreateDocumentCollectionUri("Course", "Class"), query, options);

                return new OkObjectResult(data);
            } catch (Exception e)
            {
                log.LogError($"Error : {e.Message}");

                return new BadRequestObjectResult($"Error : {e.Message}");
            }
        }

        [FunctionName("CRUD_GetClassById")]
        public static async Task<IActionResult> GetAllById(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Class/{classId}")] HttpRequest req,
            [CosmosDB(
                databaseName: "Course",
                collectionName: "Class",
                ConnectionStringSetting = "cosmos-bl-tutorial-serverless",
                Id = "{classId}",
                PartitionKey = "Class/")] Class data,
            ILogger log)
        {
            log.LogInformation("Triggering CRUD_GetClassById by HTTP Trigger");

            try
            {
                return new OkObjectResult(data);
            }
            catch (Exception e)
            {
                log.LogError($"Error : {e.Message}");

                return new BadRequestObjectResult($"Error : {e.Message}");
            }
        }

        [FunctionName("CRUD_CreateClass")]
        public static async Task<IActionResult> CreateClass(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "Class")] HttpRequest req,
            [CosmosDB(ConnectionStringSetting = "cosmos-bl-tutorial-serverless")] DocumentClient documentClient,
            ILogger log)
        {
            log.LogInformation("Triggering CRUD_GetClassById by HTTP Trigger");

            try
            {
                string requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<ClassDTO>(requestBody);
                var pk = new PartitionKey("/partitionKey");
                var options = new RequestOptions() { PartitionKey = pk };

                var classObj = new Class
                {
                    Id = Guid.NewGuid().ToString(),
                    PartitionKey = "Class/",
                    ClassCode = data.ClassCode,
                    Description = data.Description
                };

                await documentClient.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri("Course", "Class"), classObj);

                return new OkObjectResult(classObj);
            }
            catch (Exception e)
            {
                log.LogError($"Error : {e.Message}");

                return new BadRequestObjectResult($"Error : {e.Message}");
            }
        }
    }
}

