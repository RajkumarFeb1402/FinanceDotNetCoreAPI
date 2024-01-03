namespace FinanceModuleCoreAPI.Entities.Entities
{
    public class TransferResult
    {
        public bool IsSuccess { get; }
        public string Message { get; }

        public TransferResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }
    }
}
