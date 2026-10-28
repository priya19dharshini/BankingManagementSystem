using BankingManagementSystem.Helpers;
using BankingManagementSystem.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BankingManagementSystem.Repositories
{
    public class AccountRepository
    {
        public async Task<(int AccountId, string AccountNumber)> AddAsync(Account account)
        {
            string accountNumber = Guid.NewGuid()
                .ToString("N")[..12]
                .ToUpper();

            const string sql = """
        INSERT INTO Accounts
            (AccountNumber, CustomerId, BranchId, AccountType)
        OUTPUT INSERTED.AccountId, INSERTED.AccountNumber
        VALUES
            (@AccountNumber, @CustomerId, @BranchId, @AccountType);
        """;

            await using var connection = DatabaseHelper.CreateConnection();
            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@AccountNumber", SqlDbType.NVarChar, 20)
                .Value = accountNumber;

            command.Parameters.Add("@CustomerId", SqlDbType.Int)
                .Value = account.CustomerId;

            command.Parameters.Add("@BranchId", SqlDbType.Int)
                .Value = account.BranchId;

            command.Parameters.Add("@AccountType", SqlDbType.NVarChar, 20)
                .Value = account.AccountType;

            await connection.OpenAsync();

            await using var reader = await command.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
                throw new InvalidOperationException("Account could not be created.");

            return (reader.GetInt32(0), reader.GetString(1));
        }



        public async Task<Account?> GetByIdAsync(int id)
        {
            const string sql = """
        SELECT AccountId, AccountNumber, CustomerId,
               BranchId, AccountType, Balance, IsActive, CreatedAt
        FROM dbo.Accounts
        WHERE AccountId = @Id;
        """;

            await using var connection =
                DatabaseHelper.CreateConnection();

            await using var command = new SqlCommand(sql, connection);

            command.Parameters.Add("@Id", SqlDbType.Int).Value = id;

            await connection.OpenAsync();

            await using var reader = await command.ExecuteReaderAsync();

            if (!await reader.ReadAsync())
                return null;

            return new Account
            {
                AccountId = reader.GetInt32(0),
                AccountNumber = reader.GetString(1),
                CustomerId = reader.GetInt32(2),
                BranchId = reader.GetInt32(3),
                AccountType = reader.GetString(4),
                Balance = reader.GetDecimal(5),
                Status = reader.GetBoolean(6) ? "Active" : "Inactive",
                OpenedAt = reader.GetDateTime(7)
            };
        }


    }
}
