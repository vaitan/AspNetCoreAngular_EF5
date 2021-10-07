using AspNetCoreAngular_EF5.Extensions;

namespace AspNetCoreAngular_EF5.Models
{
    public class VehicleQuery : IQueryObject
    {
        public int? MakeId { get; set; }
        public int? ModelId { get; set; }
        public string SortBy { get; set; }
        public bool IsAscending { get; set; }
    }
}