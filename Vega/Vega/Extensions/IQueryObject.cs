namespace Vega.Extensions
{
    public interface IQueryObject
    {
        string SortBy { get; set; }
        bool IsAsc { get; set; }
        int Page { get; set; }
        byte PageSize { get; set; }
    }
}
