using FinanceModuleCoreAPI.Entities;
using FinanceModuleCoreAPI.Entities.Entities;

namespace FinanceModuleCoreAPI.Service
{
    public interface IBankingService
    {
        Users CreateUser(Users user);
        Users Login(string username, string password);
        decimal CheckBalance(string userName);
        Users GetUser(string userName);
        TransferResult TransferFunds(string userName, decimal amount);
        void Logout();
    }
}
