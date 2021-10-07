namespace AspNetCoreAngular_EF5.Extensions
{
    public interface IQueryObject
    {
        string SortBy { get; set; }
        bool IsAscending { get; set; }
    }
}