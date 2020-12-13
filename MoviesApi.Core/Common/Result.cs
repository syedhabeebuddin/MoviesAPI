namespace MoviesApi.Core.Common
{
    public class Result
    {
        public Result()
        {
        }

        public Result(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }

        public Result(bool isSuccess, string error)
        {
            IsSuccess = isSuccess;
            Error = error;
        }

        public Result(bool isSuccess, string error, int statusCode)
        {
            IsSuccess = isSuccess;
            Error = error;
            StatusCode = statusCode;
        }

        public int StatusCode { get; set; }

        public bool IsSuccess { get; set; }        
        public string Error { get; set; }
    }
}
