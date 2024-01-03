using FinanceModuleCoreAPI.Entities;
using FinanceModuleCoreAPI.Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceModuleCoreAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly DbContext _dbContext;

        public UserRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Users CreateUser(Users user)
        {
            _dbContext.Set<Users>().Add(user);
            _dbContext.SaveChanges();
            return user;
        }

        public Users GetUserByUsernameAndPassword(string username, string password)
        {

            var newUser = _dbContext.Set<Users>().SingleOrDefault(u => u.Username!.ToUpper().Trim() == username.ToUpper().Trim() && u.Password == password)!;
            if (newUser == null)
                throw new Exception("User not found");

            return newUser;
        }

        public Users GetUser(string username)
        {
            return _dbContext.Set<Users>().SingleOrDefault(u => u.Username!.ToString().ToUpper() == username.ToUpper().Trim())!;
        }

        public decimal CheckBalance(string userName)
        {
            return _dbContext.Set<Users>().Where(u => u.Username!.ToUpper().Trim() == userName.ToUpper().Trim()).Select(u => u.Balance).SingleOrDefault();
        }

        public TransferResult TransferFunds(string userName, decimal amount)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    var user = _dbContext.Set<Users>().SingleOrDefault(u => u.Username!.ToUpper().Trim() == userName.ToUpper().Trim());
                    if (user == null)
                        return new TransferResult(false, "User not found");

                    if (user.Balance < amount)
                        return new TransferResult(false, "There is insufficient funds in your account.");

                    if (user.Balance == 100)
                    {
                        user.Balance -= amount;

                        ApplyMaintenanceCharge(user);

                        _dbContext.SaveChanges();

                        transaction.Commit();

                        return new TransferResult(true, "There is no more balance in your account. You will be levied a Minimum maintenance charge.");
                    }

                    _dbContext.SaveChanges();
                    transaction.Commit();

                    return new TransferResult(true, "Transfer successful");
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    // Log the exception or handle it accordingly
                    return new TransferResult(false, "An error occurred during the transfer.");
                }
            }
        }


        private void ApplyMaintenanceCharge(Users user)
        {
            // Your logic to apply a minimum maintenance charge
            // For example, deduct a fixed amount from the user's balance
            user.Balance -= 10; // Assuming a fixed charge of 10 units
        }

        public void Logout()
        {
            // Implement logout logic if needed and It's handling by JWT 
        }
    }

}
