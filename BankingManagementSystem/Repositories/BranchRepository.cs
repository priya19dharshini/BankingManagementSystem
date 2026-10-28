using BankingManagementSystem.Helpers;
using BankingManagementSystem.Models;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BankingManagementSystem.Repositories
{
    public class BranchRepository
    {
        public async Task<int> AddAsync(Branch branch)
        {
            const string sql = """
                INSERT INTO BRANCHES (BranchName,BranchCode ,City)
                VALUES (@Name ,@Code ,@City);
                """;

            await using var connection = DatabaseHelper.CreateConnection();
              
            await using var command = new SqlCommand(sql, connection);
            command.Parameters.Add("@Name", SqlDbType.NVarChar, 100).Value = branch.BranchName;
            command.Parameters.Add("@Code", SqlDbType.NVarChar, 11)
            .Value = branch.BranchCode;

            command.Parameters.Add("@City", SqlDbType.NVarChar, 100)
                .Value = branch.City;

            await connection.OpenAsync();
            return Convert.ToInt32(await command.ExecuteNonQueryAsync());
        }

        public async Task<List<Branch>> GetAllAsync()
        {
            const string sql = """
            SELECT BranchId, BranchName, BranchCode, City, IsActive
            FROM Branches
            ORDER BY BranchId;
            """;

            var branches = new List<Branch>();

            await using var connection =
                DatabaseHelper.CreateConnection();

            await using var command = new SqlCommand(sql, connection);

            await connection.OpenAsync();

            await using var reader = await command.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                branches.Add(new Branch
                {
                    BranchId = reader.GetInt32(0),
                    BranchName = reader.GetString(1),
                    BranchCode = reader.GetString(2),
                    City = reader.GetString(3),
                    isActive = reader.GetBoolean(4)
                });
            }

            return branches;
        }
    }
}
