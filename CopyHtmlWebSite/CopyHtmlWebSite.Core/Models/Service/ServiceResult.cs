namespace CopyHtmlWebSite.Core.Models.Service
{
    public class ServiceResult
    {
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        public ServiceResult Completed()
        {
            return Result(true, string.Empty);
        }

        public ServiceResult Failed(string message)
        {
            return Result(false, message);
        }

        public ServiceResult Result(bool isSuccess, string message)
        {
            this.Success = isSuccess;
            this.ErrorMessage = message;
            return this;
        }
    }

    public class ServiceResult<TData> : ServiceResult
    {
        public TData Data { get; set; }

        public ServiceResult<TData> Completed(TData data)
        {
            Data = data;
            Result(true, string.Empty);
            return this;
        }
    }

    public static class ServiceResultExtension
    {
        public static bool IsSuccess(this ServiceResult result)
        {
            return result != null && result.Success;
        }

        public static bool IsFail(this ServiceResult result)
        {
            return result == null || !result.Success;
        }

        public static bool HasData<TData>(this ServiceResult<TData> result)
        {
            return result.IsSuccess() && result.Data != null;
        }
    }
}
