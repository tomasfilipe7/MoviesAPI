using System.Net.Http.Headers;
using System.Net.Http.Json;
using DeptAssignment.Models;
// using Newtonsoft.Json;
using System.Text.Json;
namespace DeptAssignment.Data;




public static class APIDataHandler
{
    static HttpClient? client;
    static string API_KEY = ""; 
    static string baseAdress = "https://api.themoviedb.org";
    static string get_popular_movies = "/3/movie/popular";
    static string get_movie_details = "/3/movie";
    static string search_movies = "/3/search/movie";
    static string get_videos = "/3/movie/{movie_id}/videos";

    public static void InitializeAPI(string _APIKey)
    {
        API_KEY = _APIKey;
        client = new();
        client.BaseAddress = new Uri(baseAdress);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public static void InitializeAPI()
    {
        client = new();
        client.BaseAddress = new Uri(baseAdress);
        client.DefaultRequestHeaders.Accept.Clear();
        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    public static async Task<MoviesResponse> GetMovieDetails(int id)
    {
        if(client == null)
        {
            InitializeAPI();
        }
        string endpoint = get_movie_details + "/" + id.ToString() +"?api_key=" + API_KEY;
        HttpResponseMessage response = await client.GetAsync(endpoint);
        if(response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<MovieDto>();
            if(result != null)
            {
                Movie movieResult = ConvertDetailsToViewModel(result).Result;
                return new MoviesResponse(movieResult, response);
            }
            else
            {
                return new MoviesResponse(response);
            }
        }
        else
        {
            return new MoviesResponse(response);
        }
    }

    public static async Task<MoviesResponse> GetPopularMovies(string language = "en-US", int page = 1)
    {
        if(client == null)
        {
            InitializeAPI();
        }
        string endpoint = get_popular_movies + "?api_key=" + API_KEY + "&language=" + language + "&page=" + page.ToString();
        HttpResponseMessage response = await client.GetAsync(endpoint);
        // response.EnsureSuccessStatusCode();
        if(response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<SearchMoviesDto>();
            
            if(result != null && result.Results != null)
            {
                List<Movie> movies_result = new List<Movie>();
                foreach(MovieDto movieDto in result.Results)
                {
                    movies_result.Add(ConvertSearchToViewModel(movieDto).Result);
                }
                return new MoviesResponse(movies_result, response);
            }
            else
            {
                return new MoviesResponse(response);
            }
            
        }
        else
        {
            return new MoviesResponse(response);
        }
    }
    
    public static async Task<MoviesResponse> SearchMovie(string query, string language = "en-US", int page = 1, bool include_adult = false, int year = -1, int primary_release_year = -1)
    {
        if(client == null)
        {
            InitializeAPI();
        }
        string endpoint = search_movies + "?api_key=" + API_KEY + "&query=" + query + "&language=" + language + "&page=" + page.ToString() +
        "&include_adult=" + include_adult.ToString();
        if(year != -1)
        {
            endpoint += endpoint + "&year=" + year.ToString();
        }
        if(primary_release_year != -1)
        {
            endpoint += endpoint + "&primary_release_year=" + primary_release_year.ToString();
        }
        HttpResponseMessage response = await client.GetAsync(endpoint);
        // response.EnsureSuccessStatusCode();
        if(response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<SearchMoviesDto>();
            
            if(result != null && result.Results != null)
            {
                List<Movie> movies_result = new List<Movie>();
                foreach(MovieDto movieDto in result.Results)
                {
                    movies_result.Add(ConvertSearchToViewModel(movieDto).Result);
                }
                return new MoviesResponse(movies_result, response);
            }
            else
            {
                return new MoviesResponse(response);
            }
        }
        else
        {
            return new MoviesResponse(response);
        }
    }

    public static async Task<Movie> ConvertSearchToViewModel(MovieDto movieDto)
    {
        Movie movie = new Movie();
        movie.Adult = movieDto.Adult;
        movie.Backdrop_path = movieDto.Backdrop_path;
        movie.Id = movieDto.Id;
        movie.Original_language = movieDto.Original_language;
        movie.Original_title = movieDto.Original_title;
        movie.Overview = movieDto.Overview;
        movie.Popularity = movieDto.Popularity;
        movie.Poster_path = movieDto.Poster_path;
        movie.Release_date = movieDto.Release_date;
        movie.Title = movieDto.Title;
        movie.Video = movieDto.Video;
        movie.Vote_average = movieDto.Vote_average;
        movie.Vote_count = movieDto.Vote_count;
        Genre[] genres = new Genre[movieDto.Genre_ids.Length];
        for(int i = 0; i < genres.Length; i++)
        {
            Genre _genre = new Genre();
            _genre.Id = movieDto.Genre_ids[i];
            _genre.Name = "";
            genres[i] = _genre;
        }
        movie.Genres = genres;
        VideoDto videoDto = await GetTrailer(movie.Id);
        if(videoDto != null)
        {
            movie.Trailer_site = videoDto.Site;
            movie.Trailer =  videoDto.Key;
        }
        return movie;
    }

    public async static Task<Movie> ConvertDetailsToViewModel(MovieDto movieDto)
    {
        Movie movie = new Movie();
        movie.Adult = movieDto.Adult;
        movie.Backdrop_path = movieDto.Backdrop_path;

        movie.Id = movieDto.Id;
        movie.Original_language = movieDto.Original_language;
        movie.Original_title = movieDto.Original_title;
        movie.Overview = movieDto.Overview;
        movie.Popularity = movieDto.Popularity;
        movie.Poster_path = movieDto.Poster_path;
        movie.Release_date = movieDto.Release_date;
        movie.Title = movieDto.Title;
        movie.Video = movieDto.Video;
        movie.Vote_average = movieDto.Vote_average;
        movie.Vote_count = movieDto.Vote_count;
        movie.Runtime = movieDto.Runtime;
        movie.Homepage = movieDto.Homepage;
        movie.Status = movieDto.Status;
        movie.Imdb_id = movieDto.Imdb_id;
        
        Genre[] genres = new Genre[movieDto.Genres.Length];
        for(int i = 0; i < genres.Length; i++)
        {
            Genre _genre = new Genre();
            _genre.Id = movieDto.Genres[i].Id;
            _genre.Name = movieDto.Genres[i].Name;
            genres[i] = _genre;
        }
        movie.Genres = genres;
        VideoDto videoDto = await GetTrailer(movie.Id);
        if(videoDto != null)
        {
            movie.Trailer_site = videoDto.Site;
            movie.Trailer =  videoDto.Key;
        }
        
        return movie;
    }

    public static async Task<VideoDto> GetTrailer(int movie_id)
    {
        string endpoint = get_videos.Replace("{movie_id}", movie_id.ToString()) + "?api_key=" + API_KEY;
        HttpResponseMessage response = await client.GetAsync(endpoint);
        // response.EnsureSuccessStatusCode();
        if(response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<SearchVideosDto>();
            if(result != null && result.Results != null)
            {
                foreach(VideoDto videoDto in result.Results)
                {
                    if(videoDto.Type == "Trailer")
                    {
                        return videoDto;
                    }
                }
            }
            
        }
        return null;
    }

}