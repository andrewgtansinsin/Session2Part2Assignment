using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

[assembly: Parallelize(Workers =10, Scope = ExecutionScope.MethodLevel)]
namespace DemoLearning2Part2
{
    [TestClass]
    public class Session2Part2Demo
    {
        private static RestClient restClient;

        private static readonly string BaseURL = "https://petstore.swagger.io/v2/";

        private static readonly string UserEndpoint = "pet";

        private static string GetURL(string enpoint) => $"{BaseURL}{enpoint}";

        private static Uri GetURI(string endpoint) => new Uri(GetURL(endpoint));

        private readonly List<PetModel> cleanUpList = new List<PetModel>();

        private static readonly string V = "https://images.app.goo.gl/KvPjfUZwUKfeJ34M8";

        [TestInitialize]
        public void TestInitialize()
        {
            restClient = new RestClient();
        }

        [TestCleanup]
        public async Task TestCleanup()
        {
            foreach (var data in cleanUpList)
            {
                var restRequest = new RestRequest(GetURI($"{UserEndpoint}/{data.Id}"));
                var restResponse = await restClient.DeleteAsync(restRequest);
            }
        }

        [TestMethod]
        public async Task PostMethod()
        {
            #region CreatePet
            //Create Pet
            var newPet = new PetModel()
            {
                Id = 88,
                Category = new Category
                {
                    Id = 8008,
                    Name = "Persian Cat"
                },
                Name = "Garfield",
                PhotoUrls = new string[] { V },
                Tags = new Tags[] { new Tags {
                    Id = 2023,
                    Name = "Andrew Tansinsin"
                },new Tags {
                    Id = 2024,
                    Name = "Andrew Tansinsin"
                }
                },
                Status = "available"
            };
        

            // Send Post Request
            var temp = GetURI(UserEndpoint);
            var postRestRequest = new RestRequest(GetURI(UserEndpoint)).AddJsonBody(newPet);
            var postRestResponse = await restClient.ExecutePostAsync(postRestRequest);

            //Verify POST request status code
            Assert.AreEqual(HttpStatusCode.OK, postRestResponse.StatusCode, "Status code is not equal to 200");
            #endregion

            #region GetPet
            var restRequest = new RestRequest(GetURI($"{UserEndpoint}/{newPet.Id}"), Method.Get);
            var restResponse = await restClient.ExecuteAsync<PetModel>(restRequest);
            #endregion

            #region Assertions
            Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode, "Status code is not equal to 200");
            Assert.AreEqual(newPet.Name, restResponse.Data.Name, "Pet Name did not match.");
            Assert.AreEqual(newPet.Category.Id, restResponse.Data.Category.Id, "Category Id did not match.");
            Assert.AreEqual(newPet.Category.Name, restResponse.Data.Category.Name, "Category Name did not match.");
            Assert.AreEqual(newPet.PhotoUrls, restResponse.Data.PhotoUrls, "Photo URLS did not match.");


            #endregion

            #region CleanUp
            cleanUpList.Add(newPet);
            #endregion
        }
    }
}
