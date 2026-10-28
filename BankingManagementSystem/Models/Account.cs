using System;
using System.Collections.Generic;
using System.Text;

namespace BankingManagementSystem.Models
{
    public class Account
    {
        
        public int AccountId { get; set; }
        public string AccountNumber { get; set; } = "";
        public int CustomerId { get; set; }
        public int BranchId { get; set; }
        public string AccountType { get; set; } = "Savings";
        public decimal Balance { get; set; }
        public string Status { get; set; } = "Active";
        public DateTime OpenedAt { get; set; }
    
}
}
