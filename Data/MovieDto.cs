namespace DeptAssignment.Data;
public class MovieDto
{
    // [Required]
    public string? Poster_path { get; set; }
    public object? Belongs_to_collection { get; set; }
    public int Budget { get; set; }
    public int Revenue { get; set; }
    public int Runtime { get; set; }
    public GenreDto[]? Genres { get; set; }
    public Object[]? Production_companies { get; set; }
    public Object[]? Production_countries { get; set; }
    public Object[]? Spoken_languages { get; set; }
    public string? Homepage { get; set; }
    public string? Status { get; set; }
    public string? Tagline { get; set; }
    public bool Adult { get; set; } 
    public string? Overview { get; set; }
    public string? Release_date { get; set; }
    public int[]? Genre_ids  { get; set; }
    public int Id { get; set; }
    public string? Imdb_id { get; set; }
    public string? Original_title { get; set; }
    public string? Original_language { get; set; }
    public string? Title { get; set; }
    public string? Backdrop_path { get; set; }
    public float Popularity { get; set; }
    public int Vote_count { get; set; }
    public bool Video { get; set; } 
    public float Vote_average { get; set; }
}