using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ActionConstraints;

namespace Bank_transactions__system_1.Models
{ 
public class BankTransaction
{
    public int Id { get; set; }

    [Required]
        [Display(Name ="Select Branch")]
    public int BankBranchId { get; set; }

    public BankBranch Branch { get; set; }

    [Required]
    [Range(0.01, double.MaxValue)]
    public decimal Amount { get; set; }

        [Required]

        //public string TransactionType { get; set; } // "Credit" or "Debit"

        [Display(Name ="Transaction Type")]
        public int TransactionTypeId { get; set; }

        [ForeignKey("TransactionTypeId")]
        public TransactionType TransactionType { get; set; }

        public DateTime TransactionDate { get; set; }
}

}