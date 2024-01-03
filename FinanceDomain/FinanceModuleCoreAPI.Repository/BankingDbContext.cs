
using FinanceModuleCoreAPI.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinanceModuleCoreAPI.Repository
{
    public class BankingDbContext : DbContext
    {
        public BankingDbContext(DbContextOptions<BankingDbContext> options)
               : base(options)
        {
        }
        public DbSet<Users> Users { get; set; }
    }
}
