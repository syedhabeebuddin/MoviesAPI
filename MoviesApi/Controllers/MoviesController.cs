using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesApi.Services.Contracts;
using MoviesApi.ViewModels.Request;
using System.Threading.Tasks;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoviesController : ControllerBase
    {
        private readonly ILogger<MoviesController> _logger;
        private readonly IMovieManager _movieManager;

        public MoviesController(ILogger<MoviesController> logger, IMovieManager movieManager)
        {
            _logger = logger;
            _movieManager = movieManager;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _movieManager.GetByIdAsync(id);
            if (response == null)
            {
                return BadRequest("Couldn't retrieve Movie");
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> Get()
        {
            var response = await _movieManager.GetAllAsync();
            if (response == null)
            {
                return BadRequest("Couldn't retrieve Movies");
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody]AddMovieRequest request)
        {
            var response = await _movieManager.InsertAsync(request);
            if (response == null)
            {
                return BadRequest("Failed To Add Genere");
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("{id}/Update")]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateMovieRequest updateRequest)
        {
            var response = await _movieManager.UpdateAsync(id, updateRequest);
            if (!response.IsSuccess)
                return BadRequest("Exception Occured While Updating Movie");

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}/Delete")]
        public async Task<IActionResult> Delete(string id)
        {
            var response = await _movieManager.DeleteAsync(id);
            if (!response.IsSuccess)
                return BadRequest(response.Error);

            return Ok(response);
        }
    }
}
