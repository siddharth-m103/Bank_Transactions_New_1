using System.ComponentModel.DataAnnotations;
namespace Bank_transactions__system_1.Models
{
    public class TransactionFilterViewModel
    {
        public int? BranchId { get; set; }

        [DataType(DataType.Date)]
        public DateTime? FromDate { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ToDate { get; set; }

        public string? TransactionType { get; set; }

        public List<BankTransaction>? Transactions { get; set; }

        public List<BankBranch>? Branches { get; set; }

        public decimal TotalAmount { get; set; }
    }
}
