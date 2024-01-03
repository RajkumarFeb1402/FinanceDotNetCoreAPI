namespace FinanceModuleCoreAPI.Entities.Entities
{
    public class TransferRequest
    {
        public string UserName { get; set; } = null!;
        public decimal Amount { get; set; }
    }
}
