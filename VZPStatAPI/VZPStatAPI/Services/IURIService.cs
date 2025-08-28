using VZPStatAPI.Filter;

namespace VZPStatAPI.Services
{
    public interface IURIService
    {
        public Uri GetPageUri(PaginationFilter filter, string route);
    }
}
