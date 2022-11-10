using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;
using System.Collections.Generic;
using System.Net;
using Test.Model;

namespace Test
{
    [TestFixture]
    public class ApiCrudTest
    {
        private RestClient client;
        private IRestResponse response;
        private RestRequest request;

        [SetUp]
        public void Setup()
        {
            client = new RestClient("https://restful-booker.herokuapp.com/");
            //client.Authenticator = new HttpBasicAuthenticator("admin", "password123");

        }
        

        [Test]
        public void GetBookingIdsTest()
        {
            request = new RestRequest("booking", Method.GET);

            response = client.Execute(request);
            
            var bookings = new JsonDeserializer().Deserialize<List<Booking>>(response);

            //client.Authenticator = new HttpBasicAuthenticator("admin", "password123");

            request = new RestRequest("booking/" + bookings[1].bookingId, Method.GET);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }


        [Test]
        public void CreateBookingTest()
        {
            //arrange
            request = new RestRequest("booking", Method.POST);
            request.RequestFormat = DataFormat.Json;
            request.AddHeader("Content-Type", "application/json");

            var testObj = new
            {
                firstname = "TEST",
                lastname = "test",
                totalprice = 170,
                depositpaid = true,
                bookingdates = new {
                    checkin = "2018-01-01",
                    checkout = "2018-01-05"
                },
                additionalneeds = "Dinner"
            };
            request.AddHeader("Accept", "application/json");
            request.AddJsonBody(testObj);

            //act
            response = client.Execute<PostsModel>(request);

            //assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }


        [Test]
        public void UpdateBookingTest()
        {
            //arrange
            request = new RestRequest("booking", Method.GET);

            response = client.Execute(request);
            var bookings = new JsonDeserializer().Deserialize<List<Booking>>(response);

            client.Authenticator = new HttpBasicAuthenticator("admin", "password123");

            request = new RestRequest("booking/" + bookings[1].bookingId, Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            var testObj = new
            {
                firstname = "Nastiia",
                lastname = "Kochurenkova",
                totalprice = 170,
                depositpaid = true,
                bookingdates = new
                {
                    checkin = "2018-01-01",
                    checkout = "2018-01-05"
                },
                additionalneeds = "Dinner"
            };

            //request.AddHeader("Accept", "application/json");
            request.AddJsonBody(testObj);

            //act
            response = client.Execute<PostsModel>(request);

            //assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }


        [Test]
        public void DeleteBookingTest()
        {
            //arrange
            client = new RestClient("https://restful-booker.herokuapp.com/");
            request = new RestRequest("booking", Method.GET);
            response = client.Execute(request);
            var bookings = new JsonDeserializer().Deserialize<List<Booking>>(response);

            client.Authenticator = new HttpBasicAuthenticator("admin", "password123");

            request = new RestRequest("booking/" + bookings[1].bookingId, Method.DELETE);

            //act
            response = client.Execute(request);

            //assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

    }
}












































//[Test]
//public void AuthTest()
//{
//    //arrange
//    string username = "admin";
//    string password = "password123";

//    RestRequest request = new RestRequest("auth", Method.POST);

//    //act
//    IRestResponse response = client.Execute(request);

//    //assert
//    Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
//}