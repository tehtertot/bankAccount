using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bank.Models
{
    public class Account : BaseEntity
    {
        [Key]
        public int AccountId {get;set;}
        [Required]
        [Display(Name="Account Name")]
        [RegularExpression(@"^[A-Za-z0-9\s']+$", ErrorMessage="No special characters.")]
        public string AccountName {get;set;}
        [Range(0,1000000)]
        [RegularExpression(@"^[0-9.]*$", ErrorMessage="Numbers and decimal point only.")]
        [Display(Name="Starting Balance")]
        public decimal Balance {get;set;}
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt {get;set;}
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt {get;set;}
        public int UserId {get;set;}
        public List<Transaction> Transactions {get;set;}

        public Account() {
            Transactions = new List<Transaction>();
        }
    }
}