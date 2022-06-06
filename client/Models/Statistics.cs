namespace client.Models;

public class NeighbourhoodListingCount
{
    public string? Neighbourhood { get; set; }
    public int Count { get; set; }
}

public class NeighbourhoodAveragePrice
{
    public string? Neighbourhood { get; set; }
    public decimal? AveragePrice { get; set; }
}

public class NeighbourhoodAverageAvailability
{
    public string? Neighbourhood { get; set; }
    public double? AverageAvailability { get; set; }
}

public class NeighbourhoodAverageBeds
{
    public string? Neighbourhood { get; set; }
    public double? AverageBeds { get; set; }
}
