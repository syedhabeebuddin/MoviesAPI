using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using MoviesApi.Controllers;
using MoviesApi.Services.Contracts;
using MoviesApi.ViewModels.Response;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MoviesApi.UnitTest.Api.Controller
{
    public class GenresControllerTests
    {
        [Fact]
        public async Task Task_Get_WhenIdIsEmpty_ReturnsBadRequest()
        {
            //Arrange
            string id = string.Empty;
            var mockGenreManager = new Mock<IGenreManager>();
            var mockLogger = new Mock<ILogger<GenresController>>();
            mockGenreManager.Setup(repo => repo.GetByIdAsync(id)).ReturnsAsync((GenreResponse)null);
            var controller = new GenresController(mockLogger.Object,mockGenreManager.Object);
            
            //Act
            var result = await controller.Get(id);

            //Assert
            Assert.IsType<BadRequestObjectResult>(result);
        }

        [Fact]
        public async Task Task_Get_WhenIdIsNotEmpty_ReturnsOK()
        {
            //Arrange
            string id = Guid.NewGuid().ToString();
            var mockGenreManager = new Mock<IGenreManager>();
            var mockLogger = new Mock<ILogger<GenresController>>();
            mockGenreManager.Setup(repo => repo.GetByIdAsync(id))
                .ReturnsAsync(new GenreResponse(new Core.Models.Genre { Name="test",Description="test",Id=id }));
            var controller = new GenresController(mockLogger.Object, mockGenreManager.Object);

            //Act
            var result = await controller.Get(id);

            //Assert
            Assert.IsType<OkObjectResult>(result);
        }
    }
}
