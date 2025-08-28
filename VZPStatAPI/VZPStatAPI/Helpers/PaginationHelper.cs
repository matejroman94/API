using VZPStatAPI.Filter;
using VZPStatAPI.Services;
using VZPStatAPI.Wrappers;

namespace VZPStatAPI.Helpers
{
    public class PaginationHelper
    {
        public static PagedAPIResponse CreatePagedReponse<T>(IEnumerable<T> pagedData, PaginationFilter validFilter, int totalRecords, IURIService uriService, string route)
        {
            var response = new PagedAPIResponse(pagedData, validFilter.PageNumber, validFilter.PageSize);
            double totalPages = 1;
            if (validFilter.PageSize <= 0) 
            { 
                validFilter.PageSize = Common.Common.MaxNumberOfRecordsFromAPI(); 
            }
            else
            {
                totalPages = ((double)totalRecords / (double)validFilter.PageSize);
            }
            int roundedTotalPages = Convert.ToInt32(Math.Ceiling(totalPages));
            response.NextPage =
                validFilter.PageNumber >= 1 && validFilter.PageNumber < roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber + 1, validFilter.PageSize), route)
                : null;
            response.PreviousPage =
                validFilter.PageNumber - 1 >= 1 && validFilter.PageNumber <= roundedTotalPages
                ? uriService.GetPageUri(new PaginationFilter(validFilter.PageNumber - 1, validFilter.PageSize), route)
                : null;
            response.FirstPage = uriService.GetPageUri(new PaginationFilter(1, validFilter.PageSize), route);
            response.LastPage = uriService.GetPageUri(new PaginationFilter(roundedTotalPages, validFilter.PageSize), route);
            response.TotalPages = roundedTotalPages;
            response.TotalRecords = totalRecords;
            return response;
        }
    }
}
