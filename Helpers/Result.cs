using System.Net;

namespace Movie_Reservation_System.Helpers
{
    public class Result<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public List<string>? Errors { get; set; }
        public T? Data { get; set; }
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;

        public static Result<T> Ok(T data, string? message = null)
            => new Result<T> { Success = true, Data = data, Message = message, StatusCode = HttpStatusCode.OK };

        public static Result<T> Fail(string message, HttpStatusCode status = HttpStatusCode.BadRequest, List<string>? errors = null)
            => new Result<T> { Success = false, Message = message, Errors = errors, StatusCode = status };

        public static Result<T> Fail(List<string> errors, HttpStatusCode status = HttpStatusCode.BadRequest)
            => new Result<T> { Success = false, Errors = errors, StatusCode = status };
    }
} 