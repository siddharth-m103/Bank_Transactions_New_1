using Bank_transactions__system_1.Models;
using Microsoft.EntityFrameworkCore;

namespace Bank_transactions__system_1.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options) 

        {
            
        }
        public DbSet<BankBranch>  BankBranches { get; set; }
        public DbSet<BankTransaction> BankTransactions { get; set; }
        public DbSet<TransactionType> TransactionTypes { get; set; }



    }
}
