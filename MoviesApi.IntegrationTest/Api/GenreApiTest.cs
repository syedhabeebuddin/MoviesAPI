using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace MoviesApi.IntegrationTest.Api
{
    public class GenreApiTest
    {
        private readonly HttpClient _client;

        public GenreApiTest()
        {
            var config = new ConfigurationBuilder()
                             .SetBasePath(Directory.GetCurrentDirectory())
                             .AddJsonFile("appsettings.json", optional: true)            
                             .Build();

            var server = new TestServer(new WebHostBuilder()
                .UseEnvironment("Development")
                .UseConfiguration(config)                
                .UseStartup<Startup>()); ;
            _client = server.CreateClient();
        }

        //[Theory]
        //[InlineData("GET")]
        //public async Task GenreGetAllTestAsync(string method)
        //{
        //    // Arrange
        //    var request = new HttpRequestMessage(new HttpMethod(method), "/api/Genres/");

        //    // Act
        //    var response = await _client.SendAsync(request);

        //    // Assert
        //    response.EnsureSuccessStatusCode();
        //    Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        //}

        [Theory]
        [InlineData("GET", "5fd48668e2227ef25e47a59b")]
        public async Task GenreGetByIdTestAsync(string method,string id)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), $"/api/Genres/{id}");

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
