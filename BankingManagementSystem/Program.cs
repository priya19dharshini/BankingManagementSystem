using BankingManagementSystem.Repositories;
using BankingManagementSystem.Services;
using BankingManagementSystem.Models;
using System.Diagnostics;

class Program
{
    static async Task Main(string[] args)
    {
        var branchRepository = new BranchRepository();
        var customerRepository = new CustomerRepository();

        var branchService = new BranchService(branchRepository);
        var customerService = new CustomerService(customerRepository);

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

                case "3":
                    await RegisterCustomer(customerService);
                    break;

                case "4":
                    await ViewCustomers(customerService);
                    break;

                case "5":
                    await SearchCustomer(customerService);
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


    static async Task RegisterCustomer(CustomerService service)
    {
        var customer = new Customer();

        Console.Write("Enter full name: ");
        customer.FullName = Console.ReadLine() ?? "";

        Console.Write("Enter email: ");
        customer.Email = Console.ReadLine() ?? "";

        Console.Write("Enter phone number: ");
        customer.Phone = Console.ReadLine() ?? "";

        Console.Write("Enter date of birth (yyyy-MM-dd): ");
        if (!DateTime.TryParse(Console.ReadLine(), out DateTime dob))
        {
            Console.WriteLine("Invalid date.");
            return;
        }

        customer.DateOfBirth = dob;

        Console.Write("Enter branch ID: ");
        if (!int.TryParse(Console.ReadLine(), out int branchId))
        {
            Console.WriteLine("Invalid branch ID.");
            return;
        }

        customer.BranchId = branchId;

        int id = await service.RegisterAsync(customer);

        Console.WriteLine($"Customer registered successfully. ID: {id}");
    }

    static async Task ViewCustomers(CustomerService service)
    {
        var customers = await service.GetAllAsync();

        Console.WriteLine("\n--- Customer List ---");

        foreach (var customer in customers)
        {
            Console.WriteLine(
                $"ID: {customer.CustomerId} | " +
                $"Name: {customer.FullName} | " +
                $"Email: {customer.Email} | " +
                $"Phone: {customer.Phone} | " +
                $"Branch ID: {customer.BranchId} | " +
                $"Active: {customer.IsActive}");
        }

        if (customers.Count == 0)
            Console.WriteLine("No customers found.");
    }

    static async Task SearchCustomer(CustomerService service)
    {
        Console.Write("Enter customer ID: ");

        if (!int.TryParse(Console.ReadLine(), out int id))
        {
            Console.WriteLine("Invalid customer ID.");
            return;
        }

        var customer = await service.GetByIdAsync(id);

        Console.WriteLine($"\nCustomer ID: {customer.CustomerId}");
        Console.WriteLine($"Name: {customer.FullName}");
        Console.WriteLine($"Email: {customer.Email}");
        Console.WriteLine($"Phone: {customer.Phone}");
        Console.WriteLine($"Date of Birth: {customer.DateOfBirth:yyyy-MM-dd}");
        Console.WriteLine($"Branch ID: {customer.BranchId}");
        Console.WriteLine($"Active: {customer.IsActive}");
    }


}

