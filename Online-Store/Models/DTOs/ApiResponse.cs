using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Online_Store.Models.DTOs
{
    public class ApiResponse
    {
        public bool Success { get; protected set; }
        public string Message { get; protected set; }

        protected ApiResponse()
        {
            Success = true;
        }

        public ApiResponse(string message) : this()
        {
            Message = message;
        }
    }

    public class ApiResponse<T> : ApiResponse
    {
        public T Data { get; set; }

        public ApiResponse(T data, string message = null) : base(message)
        {
            Data = data;
        }
    }

    public class ApiResponseError : ApiResponse
    {
        public Dictionary<string, string[]> Errors { get; set; }

        public ApiResponseError(ModelStateDictionary modelState) : base("Ошибки валидации")
        {
            Success = false;
            Errors = modelState
                .Where(e => e.Value.Errors.Count > 0)
                .ToDictionary(
                    kvp => kvp.Key,
                    kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray());
        }
    }
}
