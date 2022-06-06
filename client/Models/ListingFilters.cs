using System.ComponentModel.DataAnnotations;

namespace client.Models;

public class ListingFilters
{

    [Range(0, 1000, ErrorMessage = "Minimum price must be between 0 and 1000.")]
    public int? MinPrice { get; set; } = null;

    [Range(1, 1000, ErrorMessage = "Maximum price must be between 1 and 1000.")]
    public int? MaxPrice { get; set; } = null;

    [StringLength(100, ErrorMessage = "Neighbourhood is too long.")]
    public string? Neighbourhood { get; set; } = null;

    [Range(0, 1000, ErrorMessage = "Minimum number of reviews must be between 1 and 1000.")]
    public int? MinNrOfReviews { get; set; } = null;

    [Range(0, 1000, ErrorMessage = "Maximum number of reviews must be between 1 and 1000.")]
    public int? MaxNrOfReviews { get; set; } = null;
}
