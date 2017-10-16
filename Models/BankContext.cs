using Microsoft.EntityFrameworkCore;

namespace bank.Models
{
    public class BankContext : DbContext
    {
        public BankContext(DbContextOptions<BankContext> options) : base(options) { }

        public DbSet<User> Users {get;set;}
        public DbSet<Account> Accounts {get;set;}
        public DbSet<Transaction> Transactions {get;set;}
    }
}