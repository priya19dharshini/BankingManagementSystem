
using BankingManagementSystem.Models;
using BankingManagementSystem.Repositories;
using System;

namespace BankingManagementSystem.Services
{
    public class AccountService
    {
        private readonly AccountRepository _repository;

        public AccountService(AccountRepository repository)
        {
            _repository = repository;
        }

        public async Task<(int AccountId, string AccountNumber)> OpenAccountAsync(Account account)
        {
            if (account.CustomerId <= 0)
                throw new ArgumentException("Invalid customer ID.");

            if (account.BranchId <= 0)
                throw new ArgumentException("Invalid branch ID.");

            if (account.AccountType != "Savings" &&
                account.AccountType != "Current")
                throw new ArgumentException("Invalid account type.");

            return await _repository.AddAsync(account);
        }

        public async Task<Account> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid account ID.");

            return await _repository.GetByIdAsync(id)
                ?? throw new InvalidOperationException(
                    "Account not found.");
        }
    }
}
