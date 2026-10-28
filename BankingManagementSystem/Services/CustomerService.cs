using BankingManagementSystem.Models;
using BankingManagementSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace BankingManagementSystem.Services
{
    public class CustomerService
    {
        private readonly CustomerRepository _repository;

        public CustomerService(CustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> RegisterAsync(Customer customer)
        {
            if (string.IsNullOrWhiteSpace(customer.FullName))
                throw new ArgumentException("Name is required.");

            if (!MailAddress.TryCreate(customer.Email, out _))
                throw new ArgumentException("Invalid email address.");

            if (string.IsNullOrWhiteSpace(customer.Phone) ||
                customer.Phone.Length < 10 ||
                customer.Phone.Length > 15 ||
                !customer.Phone.All(char.IsDigit))
            {
                throw new ArgumentException(
                    "Phone must contain 10 to 15 digits.");
            }

            if (customer.DateOfBirth.Date >
                DateTime.Today.AddYears(-18))
            {
                throw new ArgumentException(
                    "Customer must be at least 18 years old.");
            }

            if (customer.BranchId <= 0)
                throw new ArgumentException("Invalid branch ID.");

            return await _repository.AddAsync(customer);
        }

        public Task<List<Customer>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public async Task<Customer> GetByIdAsync(int id)
        {
            if (id <= 0)
                throw new ArgumentException("Invalid customer ID.");

            return await _repository.GetByIdAsync(id)
                ?? throw new InvalidOperationException(
                    "Customer not found.");
        }
    }
}
