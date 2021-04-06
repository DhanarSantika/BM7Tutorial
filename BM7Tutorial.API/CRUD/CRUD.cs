using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using BM7Tutorial.API.CRUD.DTO;
using BM7Tutorial.BLL.CRUD;
using BM7Tutorial.DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Nexus.Base.CosmosDBRepository;
using static BM7Tutorial.DAL.Repositories;

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

        [FunctionName("CRUD_GetClassByIdNexus")]
        public static async Task<IActionResult> GetAllByIdNexus(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "Nexus/Class/{classId}")] HttpRequest req,
            [CosmosDB(ConnectionStringSetting = "cosmos-bl-tutorial-serverless")] DocumentClient documentClient,
            string classId,
            ILogger log)
        {
            log.LogInformation("Triggering CRUD_GetClassByIdNexus by HTTP Trigger");

            try
            {
                var repsClass = new ClassRepository(documentClient);
                var pk = new Dictionary<string, string> { { "ClassCode", "test-class-1" } };
                var data = await repsClass.GetByIdAsync(classId, partitionKeys: pk);

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

                var repsClass = new ClassRepository(documentClient);
                var classObj = new Class
                {
                    ClassCode = data.ClassCode,
                    Description = data.Description
                };

                var options = new EventGridOptions { PublishEvent = false };

                await repsClass.CreateAsync(classObj, options);

                return new OkObjectResult(classObj);
            }
            catch (Exception e)
            {
                log.LogError($"Error : {e.Message}");

                return new BadRequestObjectResult($"Error : {e.Message}");
            }
        }

        /// <summary>
        /// Implementasi Get Class By Id yang melalui tahap unit testing
        /// </summary>
        /// <param name="req"></param>
        /// <param name="documentClient"></param>
        /// <param name="classId"></param>
        /// <param name="log"></param>
        /// <returns></returns>
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Class))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(string))]
        [FunctionName("CRUD_GetClassByIdUT")]
        public static async Task<IActionResult> GetClassByIdUT(
            [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "UT/Class/{classId}")] HttpRequest req,
            [CosmosDB(ConnectionStringSetting = "cosmos-bl-tutorial-serverless")] DocumentClient documentClient,
            string classId,
            ILogger log)
        {
            try
            {
                var crudService = new CRUDService(new ClassRepository(documentClient));

                Dictionary<string, string> pk = null; // new Dictionary<string, string> { { "ClassCode", "test-class-code" } };
                var classById = await crudService.GetClassById(classId, pk);

                return new OkObjectResult(classById);
            } catch (Exception e)
            {
                return new BadRequestObjectResult(e.Message);
            }
        }
    }
}

