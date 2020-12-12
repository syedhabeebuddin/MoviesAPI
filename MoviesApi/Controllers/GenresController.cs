using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MoviesApi.Services.Contracts;
using MoviesApi.ViewModels.Request;

namespace MoviesApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenresController : ControllerBase
    {
        private readonly ILogger<GenresController> _logger;
        private readonly IGenreManager _genreManager;
        

        public GenresController(ILogger<GenresController> logger, IGenreManager genreManager)
        {           
            _logger = logger;
            _genreManager = genreManager;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var response = await _genreManager.GetByIdAsync(id);
            if (response == null)
            {
                return BadRequest("");
            }
            return Ok(response);
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> Get()
        {
            var response = await _genreManager.GetAllAsync();
            if (response == null)
            {
                return BadRequest("");
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("Add")]
        public async Task<IActionResult> Add([FromBody]AddGenreRequest request)
        {
            var response = await _genreManager.InsertAsync(request);
            if (response == null)
            {
                return BadRequest("Failed To Add Genere");
            }
            return Ok(response);
        }

        [HttpPost]
        [Route("{id}/Update")]
        public async Task<IActionResult> Update(string id,[FromBody]UpdateGenreRequest updateRequest)
        {
            var response = await _genreManager.UpdateAsync(id,updateRequest);
            if (!response.IsSuccess)
                return BadRequest("Exception Occured While Updating Genre");

            return Ok(response);
        }

        [HttpDelete]
        [Route("{id}/Delete")]
        public async Task<IActionResult> DeleteAccount(string id)
        {
            var response = await _genreManager.DeleteAsync(id);
            if (!response.IsSuccess)
                return BadRequest(response.Error);

            return Ok(response);
        }
    }
}
