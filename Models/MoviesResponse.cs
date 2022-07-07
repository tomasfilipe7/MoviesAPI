using System.ComponentModel.DataAnnotations;

namespace DeptAssignment.Models;

public class MoviesResponse
{
    public HttpResponseMessage Response { get; set; }
    public Movie? Movie { get; set; }
    public List<Movie>? Movies { get; set; }
    public MoviesResponse(Movie Movie, HttpResponseMessage Response)
    {
        this.Movie = Movie;
        this.Response = Response;
    }

    public MoviesResponse(List<Movie> Movies, HttpResponseMessage Response)
    {
        this.Movies = Movies;
        this.Response = Response;
    }

    public MoviesResponse(HttpResponseMessage Response)
    {
        this.Response = Response;
    }
}