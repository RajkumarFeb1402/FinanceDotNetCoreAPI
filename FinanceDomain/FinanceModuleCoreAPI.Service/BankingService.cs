using FinanceModuleCoreAPI.Entities;
using FinanceModuleCoreAPI.Entities.Entities;
using FinanceModuleCoreAPI.Repository;

namespace FinanceModuleCoreAPI.Service
{
    public class BankingService : IBankingService
    {
        private readonly IUserRepository _userRepository;

        public BankingService(IUserRepository userRepository)
        {
            this._userRepository = userRepository;
        }

        public Users CreateUser(Users users)
        {
            return _userRepository.CreateUser(users);
        }

        public Users Login(string username, string password)
        {
            return _userRepository.GetUserByUsernameAndPassword(username, password);
        }

        public Users GetUser(string username)
        {
            return _userRepository.GetUser(username);
        }
        public decimal CheckBalance(string userName)
        {
            return _userRepository.CheckBalance(userName);
        }

        public TransferResult TransferFunds(string userId, decimal amount)
        {
            return _userRepository.TransferFunds(userId, amount);
        }

        public void Logout()
        {
            _userRepository.Logout();
        }
    }

}
