using System;
using System.Collections.Generic;
using System.Text;

namespace BankingManagementSystem.Models
{
    public class Branch
    {
        public int BranchId { get; set; }
        public string? BranchName { get; set; } = "";
        public string? BranchCode { get; set; } = "";
        public string? City { get; set; } = "";
        public bool isActive { get; set; }

    }
}
