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

        private static readonly string UserEndpoint = "user";

        private static string GetURL(string enpoint) => $"{BaseURL}{enpoint}";

        private static Uri GetURI(string endpoint) => new Uri(GetURL(endpoint));

        private readonly List<UserModel> cleanUpList = new List<UserModel>();

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
                var restRequest = new RestRequest(GetURI($"{UserEndpoint}/{data.Username}"));
                var restResponse = await restClient.DeleteAsync(restRequest);
            }
        }

        [TestMethod]
        public async Task GetMethodExecuteAsync()
        {
            #region CreateUser
            //Create User
            var newUser = new UserModel()
            {
                Username = $"csvDemoUser_ExecuteAsync{DateTime.Now.ToString("ddHHmmss")}",
                FirstName = "Thancred",
                LastName = "Waters",
                Email = "csvDemo2User0099@admin.com",
                Password = "aBcD9936",
                Phone = "36521542",
                UserStatus = 1
            };

            // Send Post Request
            var temp = GetURI(UserEndpoint);
            var postRestRequest = new RestRequest(GetURI(UserEndpoint)).AddJsonBody(newUser);
            var postRestResponse = await restClient.ExecutePostAsync(postRestRequest);
            #endregion

            #region GetUser
            var restRequest = new RestRequest(GetURI($"{UserEndpoint}/{newUser.Username}"), Method.Get);
            var restResponse = await restClient.ExecuteAsync<UserModel>(restRequest);
            #endregion

            #region Assertions
            Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode, "Status code is not equal to 200");
            Assert.AreEqual(newUser.FirstName, restResponse.Data.FirstName, "First Name did not match.");
            Assert.AreEqual(newUser.LastName, restResponse.Data.LastName, "Last Name did not match.");
            Assert.AreEqual(newUser.Email, restResponse.Data.Email, "Email did not match.");
            #endregion

            #region CleanUp
            cleanUpList.Add(newUser);
            #endregion
        }

        [TestMethod]
        public async Task GetMethodExecuteGetAsync()
        {
            #region CreateUser
            //Create User
            var newUser = new UserModel()
            {
                Username = $"csvDemoUser_ExecuteGetAsync{DateTime.Now.ToString("ddHHmmss")}",
                FirstName = "Thancred",
                LastName = "Waters",
                Email = "csvDemo2User0099@admin.com",
                Password = "aBcD9936",
                Phone = "36521542",
                UserStatus = 1
            };

            // Send Post Request
            var postRestRequest = new RestRequest(GetURI(UserEndpoint)).AddJsonBody(newUser);
            var postRestResponse = await restClient.ExecutePostAsync(postRestRequest);
            #endregion

            #region GetUser
            var restRequest = new RestRequest(GetURI($"{UserEndpoint}/{newUser.Username}"));
            var restResponse = await restClient.ExecuteGetAsync<UserModel>(restRequest);
            #endregion

            #region Assertions
            Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode, "Status code is not equal to 200");
            Assert.AreEqual(newUser.Password, restResponse.Data.Password, "Password did not match.");
            Assert.AreEqual(newUser.Phone, restResponse.Data.Phone, "Phone did not match.");
            Assert.AreEqual(newUser.UserStatus, restResponse.Data.UserStatus, "User status did not match.");
            #endregion

            #region CleanUp
            cleanUpList.Add(newUser);
            #endregion
        }

        [TestMethod]
        public async Task GetMethodGetAsync()
        {
            #region CreateUser
            //Create User
            var newUser = new UserModel()
            {
                Username = $"csvDemoUser_GetAsync{DateTime.Now.ToString("ddHHmmss")}",
                FirstName = "Thancred",
                LastName = "Waters",
                Email = "csvDemo2User0099@admin.com",
                Password = "aBcD9936",
                Phone = "36521542",
                UserStatus = 1
            };

            // Send Post Request
            var postRestRequest = new RestRequest(GetURI(UserEndpoint)).AddJsonBody(newUser);
            var postRestResponse = await restClient.ExecutePostAsync(postRestRequest);
            #endregion

            #region GetUser
            var restRequest = new RestRequest(GetURI($"{UserEndpoint}/{newUser.Username}"));
            var restResponse = await restClient.GetAsync<UserModel>(restRequest);
            #endregion

            #region Assertions
            //Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode, "Status code is not equal to 200");
            Assert.AreEqual(newUser.FirstName, restResponse.FirstName, "First Name did not match.");
            Assert.AreEqual(newUser.LastName, restResponse.LastName, "Last Name did not match.");
            Assert.AreEqual(newUser.Email, restResponse.Email, "Email did not match.");
            #endregion

            #region CleanUp
            cleanUpList.Add(newUser);
            #endregion
        }

        [TestMethod]
        public async Task PostMethod()
        {
            #region CreateUser
            //Create User
            var newUser = new UserModel()
            {
                Username = $"csvDemoUser_Post{DateTime.Now.ToString("ddHHmmss")}",
                FirstName = "Thancred",
                LastName = "Waters",
                Email = "csvDemo2User0099@admin.com",
                Password = "aBcD9936",
                Phone = "36521542",
                UserStatus = 1
            };

            // Send Post Request
            var temp = GetURI(UserEndpoint);
            var postRestRequest = new RestRequest(GetURI(UserEndpoint)).AddJsonBody(newUser);
            var postRestResponse = await restClient.ExecutePostAsync(postRestRequest);

            //Verify POST request status code
            Assert.AreEqual(HttpStatusCode.OK, postRestResponse.StatusCode, "Status code is not equal to 200");
            #endregion

            #region GetUser
            var restRequest = new RestRequest(GetURI($"{UserEndpoint}/{newUser.Username}"), Method.Get);
            var restResponse = await restClient.ExecuteAsync<UserModel>(restRequest);
            #endregion

            #region Assertions
            Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode, "Status code is not equal to 200");
            Assert.AreEqual(newUser.FirstName, restResponse.Data.FirstName, "First Name did not match.");
            Assert.AreEqual(newUser.LastName, restResponse.Data.LastName, "Last Name did not match.");
            Assert.AreEqual(newUser.Email, restResponse.Data.Email, "Email did not match.");
            #endregion

            #region CleanUp
            cleanUpList.Add(newUser);
            #endregion
        }

        [TestMethod]
        public async Task PutMethod()
        {
            #region CreateUser
            //Create User
            var newUser = new UserModel()
            {
                Username = $"csvDemoUser_Put{DateTime.Now.ToString("ddHHmmss")}",
                FirstName = "Thancred",
                LastName = "Waters",
                Email = "csvDemo2User0099@admin.com",
                Password = "aBcD9936",
                Phone = "36521542",
                UserStatus = 1
            };

            // Send Post Request
            var temp = GetURI(UserEndpoint);
            var postRestRequest = new RestRequest(GetURI(UserEndpoint)).AddJsonBody(newUser);
            var postRestResponse = await restClient.ExecutePostAsync(postRestRequest);
            #endregion

            #region GetUser
            var restRequest = new RestRequest(GetURI($"{UserEndpoint}/{newUser.Username}"), Method.Get);
            var restResponse = await restClient.ExecuteAsync<UserModel>(restRequest);
            #endregion

            #region Assertions
            Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode, "Status code is not equal to 200");
            Assert.AreEqual(newUser.FirstName, restResponse.Data.FirstName, "First Name did not match.");
            Assert.AreEqual(newUser.LastName, restResponse.Data.LastName, "Last Name did not match.");
            Assert.AreEqual(newUser.Email, restResponse.Data.Email, "Email did not match.");
            #endregion

            #region UpdateUser
            //Update user data
            newUser.Id = restResponse.Data.Id;
            newUser.FirstName = "Malena";
            newUser.LastName = "Scordia";
            newUser.Email = "csvDemo2User0099@admin.com";

            //Send Put request
            var restPutRequest = new RestRequest(GetURI($"{UserEndpoint}/{newUser.Username}")).AddJsonBody(newUser);
            var restPutResponse = await restClient.ExecutePutAsync<UserModel>(restPutRequest);

            //Verify PUT request status code
            Assert.AreEqual(HttpStatusCode.OK, restPutResponse.StatusCode, "Status code is not equal to 200");
            #endregion

            #region GetUpdatedUser
            var restRequest2 = new RestRequest(GetURI($"{UserEndpoint}/{newUser.Username}"), Method.Get);
            var restResponse2 = await restClient.ExecuteAsync<UserModel>(restRequest2);
            #endregion

            #region Assertions
            Assert.AreEqual(HttpStatusCode.OK, restResponse2.StatusCode, "Status code is not equal to 200");
            Assert.AreEqual(newUser.FirstName, restResponse2.Data.FirstName, "First Name did not match.");
            Assert.AreEqual(newUser.LastName, restResponse2.Data.LastName, "Last Name did not match.");
            Assert.AreEqual(newUser.Email, restResponse2.Data.Email, "Email did not match.");
            #endregion

            #region CleanUp
            cleanUpList.Add(newUser);
            #endregion
        }

        [TestMethod]
        public async Task DeleteMethod()
        {
            #region CreateUser
            //Create User
            var newUser = new UserModel()
            {
                Username = $"csvDemoUser_Delete{DateTime.Now.ToString("ddHHmmss")}",
                FirstName = "Thancred",
                LastName = "Waters",
                Email = "csvDemo2User0099@admin.com",
                Password = "aBcD9936",
                Phone = "36521542",
                UserStatus = 1
            };

            // Send Post Request
            var postRestRequest = new RestRequest(GetURI(UserEndpoint)).AddJsonBody(newUser);
            var postRestResponse = await restClient.ExecutePostAsync(postRestRequest);
            #endregion

            #region DeleteUser
            var deleteRestRequest = new RestRequest(GetURI($"{UserEndpoint}/{newUser.Username}"));
            var deleteRestResponse = await restClient.DeleteAsync(deleteRestRequest);

            Assert.AreEqual(HttpStatusCode.OK, deleteRestResponse.StatusCode, "Status code is not equal to 200");
            #endregion

            #region Verify if user exist
            var restRequest = new RestRequest(GetURI($"{UserEndpoint}/{newUser.Username}"), Method.Get);
            var restResponse = await restClient.ExecuteAsync<UserModel>(restRequest);

            Assert.AreEqual(HttpStatusCode.NotFound, restResponse.StatusCode, "Status code is not equal to 404. User still exist");
            #endregion

        }

        [TestMethod]
        public async Task AuthenticationBearerToken()
        {
            #region get token data
            // Initialize Request
            var authenticationURL = "https://restful-booker.herokuapp.com/auth";
            var authenticationData = "{\"username\":\"admin\",\"password\":\"password123\"}";

            // Send Post Request
            var restRequest = new RestRequest(authenticationURL).AddStringBody(authenticationData, DataFormat.Json);
            var restResponse = await restClient.ExecutePostAsync<TokenModel>(restRequest);

            // Validate Reponse Status Code
            Assert.AreEqual(HttpStatusCode.OK, restResponse.StatusCode, "Status Code is not OK");

            // Get Token Data
            var token = restResponse.Data.Token;
            #endregion

            #region Create Booking
            var newBooking = new BookingModel()
            {
                Firstname = "Jim",
                Lastname = "Brown",
                Totalprice = 111,
                Depositpaid = true,
                Bookingdates = new BookingDatesModel()
                {
                    Checkin = DateTime.Parse("2022-01-01"),
                    Checkout = DateTime.Parse("2022-01-05")
                },
                Additionalneeds = "Breakfast"
            };

            // Send Post Request
            var postRequestURL = "https://restful-booker.herokuapp.com/booking";
            var postRestRequest = new RestRequest(postRequestURL).AddJsonBody(newBooking)
                .AddHeader("Accept", "application/json");
            var postRestResponse = await restClient.ExecutePostAsync<BookingResponseModel>(postRestRequest);

            // Validate Reponse Status Code
            Assert.AreEqual(HttpStatusCode.OK, postRestResponse.StatusCode, "Status Code is not OK");

            //Get Booking Id
            var bookingId = postRestResponse.Data.BookingId;
            #endregion

            #region Delete Booking
            var deleteRequestURL = $"https://restful-booker.herokuapp.com/booking/{bookingId}";
            var deleteRestRequest = new RestRequest(deleteRequestURL)
                .AddHeader("Cookie", $"token={token}");
            //.AddHeader("Authorization", Basic YWRtaW46cGFzc3dvcmQxMjM=);
            var deleteRestResponse = await restClient.DeleteAsync(deleteRestRequest);

            Assert.AreEqual(HttpStatusCode.Created, deleteRestResponse.StatusCode, "Status code is not equal to 201");
            #endregion

        }
    }
}
