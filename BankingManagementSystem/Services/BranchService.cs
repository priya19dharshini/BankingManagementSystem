using BankingManagementSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using BankingManagementSystem.Models;
using BankingManagementSystem.Helpers;
using Microsoft.Data.SqlClient;

namespace BankingManagementSystem.Services
{
    public class BranchService
    {
        private readonly BranchRepository _repository;
        public BranchService(BranchRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> AddAsync(Branch branch)
        {
            if (string.IsNullOrWhiteSpace(branch.BranchName))
                throw new ArgumentException(
                    "Branch name is required.");

            if (string.IsNullOrWhiteSpace(branch.BranchCode))
                throw new ArgumentException(
                    "Branch code is required.");

            if (string.IsNullOrWhiteSpace(branch.City))
                throw new ArgumentException("City is required.");

            return await _repository.AddAsync(branch);
        }

        public Task<List<Branch>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }
    }
}
