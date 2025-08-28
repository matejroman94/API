using System.ComponentModel;
using System.Net;

namespace VZPStatAPI.Wrappers
{
    public class APIResponse 
    {
        [DefaultValue(200)]
        public HttpStatusCode StatusCode { get; set; }
        [DefaultValue(false)]
        public bool IsSuccess { get; set; }
        [DefaultValue("")]
        public string ErrorMessage { get; set; } = string.Empty;
        [DefaultValue("")]
        public string InnerErrorMessage { get; set; } = string.Empty;
        [DefaultValue("")]
        public object? Result { get; set; } 
    }
}
