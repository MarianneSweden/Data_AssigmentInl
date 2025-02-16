namespace Business.Models;

public class ProjectModel
{
    public int Id { get; set; }
    public string ProjectNumber { get; set; } = string.Empty;
    public string ProjectName { get; set; } = string.Empty;
    public string? Description { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int CustomerId { get; set; }
    public int StatusId { get; set; }
    public int UserId { get; set; }
    public int ProductId { get; set; }
    public decimal PricePerHour { get; set; }
    public decimal? TotalPrice { get; set; }

    public decimal? EstimatedHours { get; set; }


}
