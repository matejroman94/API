using System.Net;

namespace VZPStatAPI.Wrappers
{
    public interface IAPIResponse
    {
        HttpStatusCode StatusCode { get; set; }
        bool IsSuccess { get; set; }
        string ErrorMessage { get; set; }
        string InnerErrorMessage { get; set; }
        object? Result { get; set; }
    }
}
