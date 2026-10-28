using BankingManagementSystem.Repositories;
using BankingManagementSystem.Services;
using BankingManagementSystem.Models;
using System.Diagnostics;

class Program
{
    static async Task Main(string[] args)
    {
        var branchRepository = new BranchRepository();

        var branchService = new BranchService(branchRepository);

        while (true)
        {
            Console.WriteLine("\n================================");
            Console.WriteLine("   BANKING MANAGEMENT SYSTEM");
            Console.WriteLine("================================");
            Console.WriteLine("1. Add Branch");
            Console.WriteLine("2. View Branches");
            Console.WriteLine("3. Register Customer");
            Console.WriteLine("4. View Customers");
            Console.WriteLine("5. Search Customer by ID");
            Console.WriteLine("6. Open Account");
            Console.WriteLine("7. View Account by ID");
            Console.WriteLine("0. Exit");
            Console.Write("Enter your choice: ");

            string? choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    await AddBranch(branchService);
                    break;

                case "2":
                    await ViewBranches(branchService);
                    break;
            }

        }
    }

    static async Task AddBranch(BranchService service)
    {
        var branch = new Branch();

        Console.Write("Enter branch name: ");
        branch.BranchName = Console.ReadLine() ?? "";

        Console.Write("Enter branch code: ");
        branch.BranchCode = Console.ReadLine() ?? "";

        Console.Write("Enter city: ");
        branch.City = Console.ReadLine() ?? "";

        int id = await service.AddAsync(branch);

        Console.WriteLine($"Branch added successfully. ID: {id}");
    }

    static async Task ViewBranches(BranchService service)
    {
        var branches = await service.GetAllAsync();

        Console.WriteLine("\n--- Branch List ---");

        foreach (var branch in branches)
        {
            Console.WriteLine(
                $"ID: {branch.BranchId} | " +
                $"Name: {branch.BranchName} | " +
                $"Code: {branch.BranchCode} | " +
                $"City: {branch.City} | " +
                $"Active: {branch.isActive}");
        }

        if (branches.Count == 0)
            Console.WriteLine("No branches found.");
    }

}

