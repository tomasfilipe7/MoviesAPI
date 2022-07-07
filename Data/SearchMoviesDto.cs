namespace DeptAssignment.Data;
public class SearchMoviesDto
{
    public int Page { get; set; }
    // [JsonProperty(PropertyName = "results")]
    public MovieDto[]? Results { get; set; }

    public int Total_results { get; set; }
    public int Total_pages { get; set; }
}