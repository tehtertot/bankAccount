using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace bank.Models
{
    public class User : BaseEntity
    {
        [Key]
        public int UserId {get;set;}
        public string FirstName {get;set;}
        public string LastName {get;set;}
        public string EmailAddress {get;set;}
        public string Password {get;set;}
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime CreatedAt {get;set;}
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt {get;set;}
        public List<Account> Accounts {get;set;}

        public User() {
            Accounts = new List<Account>();
        }
        
    }
}