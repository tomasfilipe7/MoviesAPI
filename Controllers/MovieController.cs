using Microsoft.AspNetCore.Mvc;
using DeptAssignment.Models;
using DeptAssignment.Data;
namespace DeptAssignment.Controllers;

[ApiController]
// [Route("api/[controller]")]
[Route("api/[controller]")]
public class MovieController : ControllerBase
{

    private readonly ILogger<MovieController> _logger;

    public MovieController(ILogger<MovieController> logger)
    {
        _logger = logger;
    }


    [HttpGet]
    [Route("[action]")]
    [ProducesResponseType(typeof(IEnumerable<Movie>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ResponseCache(Duration = 15, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Popular()
    {
        MoviesResponse moviesResponse = await APIDataHandler.GetPopularMovies();
        if(moviesResponse.Response.StatusCode.ToString() == "Unauthorized")
        {
            _logger.LogInformation(DateTime.Now.ToString() +  " Unauthorize access tried");
            return Unauthorized();
        }
        else if(moviesResponse.Response.IsSuccessStatusCode)
        {
            _logger.LogInformation(DateTime.Now.ToString() + " Popular accessed");
            return Ok(moviesResponse.Movies);
        }
        else
        {
            _logger.LogInformation(DateTime.Now.ToString() + " Popular accessed ");
            return NotFound();
        }
    }

    [HttpGet]
    [Route("[action]/{query}")]
    [ProducesResponseType(typeof(IEnumerable<Movie>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ResponseCache(Duration = 15, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Search(string query)
    {
        string _query = query.Replace(",", "").Replace(";", "").Replace("(","").Replace(")","");
        MoviesResponse moviesResponse = await APIDataHandler.SearchMovie(query:_query);
        if(moviesResponse.Response.StatusCode.ToString() == "Unauthorized")
        {
            _logger.LogInformation(DateTime.Now.ToString() + " Unauthorize access tried");
            return Unauthorized();
        }
        else if(moviesResponse.Response.IsSuccessStatusCode)
        {
            _logger.LogInformation(DateTime.Now.ToString() + " Search accessed");
            return Ok(moviesResponse.Movies);
        }
        else
        {
            _logger.LogInformation(DateTime.Now.ToString() + " Search accessed ");
            return NotFound();
        }
    }

    [HttpGet]
    [Route("[action]/{id}")]
    [ProducesResponseType(typeof(Movie), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
    public async Task<IActionResult> Details(int id)
    {
        MoviesResponse moviesResponse = await APIDataHandler.GetMovieDetails(id);
        if(moviesResponse.Response.StatusCode.ToString() == "Unauthorized")
        {
            _logger.LogInformation(DateTime.Now.ToString() + " Unauthorize access tried ");
            return Unauthorized();
        }
        else if(moviesResponse.Response.IsSuccessStatusCode)
        {
            _logger.LogInformation(DateTime.Now.ToString() + " Details accessed ");
            return Ok(moviesResponse.Movie);
        }
        else
        {
            _logger.LogInformation(DateTime.Now.ToString() + " Details accessed ");
            return NotFound();
        }    
    }
}
