using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Data.SqlClient;

namespace BankingManagementSystem.Helpers
{
    public static class DatabaseHelper
    {
        private const string ConnectionString =
            @"Server=.\SQLEXPRESS;Database=BankingManagement;" +
            @"Trusted_Connection=True;TrustServerCertificate=True;";

        public static SqlConnection CreateConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}
