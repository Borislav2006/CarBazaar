namespace CarBazaar.Helper
{
    public class QueryObject
    {
        public string? Brand { get; set; } = null;
        public string? Model { get; set; } = null;
        public string? EngineType { get; set; } = null;
        public string? FuelType { get; set; } = null;
        public string? GearBox { get; set; } = null;
        public string? Color { get; set; } = null;
        public string? SortBy { get; set; } = null;
        public bool IsDescending { get; set; } = false;
        public int  PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 6;
    }
}
