using BankingManagementSystem.Helpers;
using BankingManagementSystem.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BankingManagementSystem.Repositories
{
    public class CustomerRepository
    {
        public async Task<int> AddAsync(Customer customer)
        {
            const string sql = """
            INSERT INTO Customers
                (FullName, Email, Phone, DateOfBirth, BranchId)
            VALUES
                (@Name, @Email, @Phone, @DOB, @BranchId);
            """;

            await using var connection =
                DatabaseHelper.CreateConnection();

            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@Name", SqlDbType.NVarChar, 100)
                .Value = customer.FullName;

            command.Parameters.Add("@Email", SqlDbType.NVarChar, 255)
                .Value = customer.Email;

            command.Parameters.Add("@Phone", SqlDbType.NVarChar, 15)
                .Value = customer.Phone;

            command.Parameters.Add("@DOB", SqlDbType.Date)
                .Value = customer.DateOfBirth.Date;

            command.Parameters.Add("@BranchId", SqlDbType.Int)
                .Value = customer.BranchId;

            await connection.OpenAsync();

            object? result = await command.ExecuteNonQueryAsync();

            return Convert.ToInt32(result);
        }

        public async Task<List<Customer>> GetAllAsync()
        {
            const string sql = """
            SELECT CustomerId, FullName, Email, Phone,
                   DateOfBirth, BranchId, CreatedAt, IsActive
            FROM Customers
            ORDER BY CustomerId;
            """;

            var customers = new List<Customer>();

            await using var connection =
                DatabaseHelper.CreateConnection();

            await using var command = new SqlCommand(sql, connection);

            await connection.OpenAsync();

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                customers.Add(new Customer
                {
                    CustomerId = reader.GetInt32(0),
                    FullName = reader.GetString(1),
                    Email = reader.GetString(2),
                    Phone = reader.GetString(3),
                    DateOfBirth = reader.GetDateTime(4),
                    BranchId = reader.GetInt32(5),
                    CreatedAt = reader.GetDateTime(6),
                    IsActive = reader.GetBoolean(7)
                });
            }

            return customers;
        }

        public async Task<Customer?> GetByIdAsync(int id)
        {
            const string sql = """
            SELECT CustomerId, FullName, Email, Phone,
                   DateOfBirth, BranchId, CreatedAt, IsActive
            FROM Customers
            WHERE CustomerId = @Id;
            """;

            await using var connection =
                DatabaseHelper.CreateConnection();

            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            await connection.OpenAsync();

            await using var reader = await command.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
                return null;

            return new Customer
            {
                CustomerId = reader.GetInt32(0),
                FullName = reader.GetString(1),
                Email = reader.GetString(2),
                Phone = reader.GetString(3),
                DateOfBirth = reader.GetDateTime(4),
                BranchId = reader.GetInt32(5),
                CreatedAt = reader.GetDateTime(6),
                IsActive = reader.GetBoolean(7)
            };
        }
    }
}
