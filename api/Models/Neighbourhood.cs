namespace api.Models

{
    public partial class Neighbourhood
    {
        public string? NeighbourhoodGroup { get; set; }
        public string Name { get; set; } = null!;
    }

    public partial class NeighbourhoodDTO
    {
        public string Name { get; set; } = null!;
    }
}
