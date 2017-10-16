using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bank.Models
{
    public class Transaction : BaseEntity
    {
        [Key]
        public int TransactionId {get;set;}
        [DataType(DataType.Currency)]
        [Display(Name="Deposit/Withdraw Amount")]
        public decimal Amount {get;set;}
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt {get;set;}
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt {get;set;}
        public int AccountId {get;set;}
    }
}