namespace DeptAssignment.Data;
public class SearchVideosDto
{
    public int Id { get; set; }
    public VideoDto[]? Results { get; set; }
}