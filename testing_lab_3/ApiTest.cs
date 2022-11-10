using NUnit.Framework;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;
using System.Net;
using Test.Model;

namespace Test
{
    public class ApiTest
    {
        private RestClient client;
        private RestRequest request;

        [SetUp]
        public void Setup()
        {
            client = new RestClient("https://api.spotify.com/v1/");
            client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(
                "BQD6YUp0-Hz5bAJ4C9rrjZDNk8C2lii11gAhC8ky7X49Ca04pz39Tw6rWFKJE0wOMpNVQhkylLXIwH_LgRi5vpWRiUDw2lIowWIP-zTRFfubRE-PGqyLjMVYqlKJ6uJEBjqBQ4Av6zolIO6c6546IN4ydW3XtlNCm_X_4zPmqA_Mur18FLj_CTVfvJXy7jnuxwzjptxqfBmRPfutrWkiHSNu2RSDpZ7Oh8tkIfg", "Bearer"
            );
        }


        [Test]
        public void BrowseCategoriesIdPlaylistsTest()
        {

            request = new RestRequest("browse/categories/0JQ5DAqbMKFA6SOHvT3gck/playlists", Method.GET);

            IRestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }


        [Test]
        public void GetTrackInPlaylistsTest()
        {
            request = new RestRequest("tracks/2FY7b99s15jUprqC0M5NCT", Method.GET);

            IRestResponse<Root> res = client.Execute<Root>(request);
            
            Assert.That(res.Data.name, Is.EqualTo("Natural"));
        }


        [Test]
        public void CreatePlaylistTest()
        {
            request = new RestRequest("users/31kvf4sntvzjs46zvtilrqodtnsm/playlists", Method.POST);

            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            var testObj = new
            {
                name = "News",
                description = "News"
            };

            request.AddJsonBody(testObj);

            IRestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));
        }

        [Test]
        public void UpdatePlaylistTest()
        {
            request = new RestRequest("playlists/6BVfTzaYPwchveihyMLv6N", Method.PUT);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Accept", "application/json");

            var testObj = new
            {
                name = "News",
                description = "News"
            };
            request.AddJsonBody(testObj);

            IRestResponse response = client.Execute(request);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }
    }
}