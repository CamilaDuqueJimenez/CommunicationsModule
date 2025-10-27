namespace DomoNow.Communications.Application.Dtos
{
    public class  ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T? Data { get; set; }
        public IReadOnlyList<string>? Errors { get; set; }
        public string? Message { get; set; }

        private ApiResponse(T data)
        {
            IsSuccess = true;
            Data = data;
        }

        private ApiResponse(IReadOnlyList<string> errors)
        {
            IsSuccess = false;
            Errors = errors;
        }

        public static ApiResponse<T> Success(T data) => new(data);
        public static ApiResponse<T> Failure(IReadOnlyList<string> errors) => new(errors);
    }
}
