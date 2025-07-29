using System.ComponentModel.DataAnnotations;

namespace Bank_transactions__system_1.Models 
{ 
public class BankBranch
{
    public int Id { get; set; }

    [Required]
    [Display(Name ="Branch Name")]
    public string BranchName { get; set; }

    [Range(0, double.MaxValue)]
    [Required]
        [Display(Name ="Credit Limit")]
    public decimal CreditLimit { get; set; }
 

    [Range(0, double.MaxValue)]
    [Display(Name ="Debit Limit")]
    public decimal DebitLimit { get; set; }

    public ICollection<BankTransaction> Transactions { get; set; }
}
}