using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace MoviesApi.IntegrationTest.Api
{
    public class MovieApiTest
    {
        private readonly HttpClient _client;

        public MovieApiTest()
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

        [Theory]
        [InlineData("DELETE", "5fd4ba7cef6928616c200a6c")]
        [InlineData("DELETE", "5fd4bab4ef6928616c200a6d")]
        public async Task MovieDeleteTestAsync(string method, string id)
        {
            // Arrange
            var request = new HttpRequestMessage(new HttpMethod(method), $"/api/Movies/{id}/Delete");

            // Act
            var response = await _client.SendAsync(request);

            // Assert
            response.EnsureSuccessStatusCode();
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
