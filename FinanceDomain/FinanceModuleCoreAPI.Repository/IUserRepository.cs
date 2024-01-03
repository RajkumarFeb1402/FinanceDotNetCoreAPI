using FinanceModuleCoreAPI.Entities;
using FinanceModuleCoreAPI.Entities.Entities;

namespace FinanceModuleCoreAPI.Repository
{
    public interface IUserRepository
    {
        Users CreateUser(Users user);
        Users GetUserByUsernameAndPassword(string userName, string password);
        Users GetUser(string userName);
        decimal CheckBalance(string userName);
        TransferResult TransferFunds(string userName, decimal amount);
        void Logout();
    }

}
