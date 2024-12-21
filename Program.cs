using System.Runtime.CompilerServices;


using System.Data.SqlClient;

namespace FinanceTrackerWithAdo
{
    internal class Program
    {
        private static string connectionString = "Data Source=.;Initial Catalog=FinanceTracker;Integrated Security=True;";
        private static DAL dal;
        static void Main(string[] args)
        {
           dal = new DAL(connectionString);

            while (true)
            {
                DisplayMainMenu();
                string choice = Console.ReadLine();
                HandleUserChoice(choice);
               
            }

        }
        private static void DisplayMainMenu()
        {
            Console.Clear();
            Console.WriteLine("=== Personal Finance Tracker ===");
            Console.WriteLine("1. Add Income");
            Console.WriteLine("2. Add Expense");
            Console.WriteLine("3. View Income");
            Console.WriteLine("4. View Expenses");
            Console.WriteLine("5. Set Budget");
            Console.WriteLine("6. View Budget Status");
            Console.WriteLine("7. Generate Report");
            Console.WriteLine("8. Exit");
            Console.Write("Choose an option: ");
        }
        private static void HandleUserChoice(string choice)
        {
            switch (choice)
            {
                case "1":
                    AddIncome();
                    break;
                case "2":
                    AddExpense();
                    break;
                case "3":
                    ViewIncome();
                    break;
                case "4":
                    ViewExpenses();
                    break;
                case "5":
                    SetBudget();
                    break;
                case "6":
                    ViewBudgetStatus();
                    break;
                case "7":
                    GenerateReport();
                    break;
                case "8":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid option! Please try again.");
                    WaitForKeyPress();
                    break;
            }
        }
        private static void AddIncome()
        {
            decimal amount = GetPositiveDecimal("Enter income amount: ");
            string source = GetNonEmptyString("Enter income source: ");

            var income = new Income { Amount = amount, Source = source };
            dal.AddIncome(income);
            Console.WriteLine("Income added successfully!");
            WaitForKeyPress();
        }

        private static void AddExpense()
        {
            decimal amount = GetPositiveDecimal("Enter expense amount: ");
            string category = GetNonEmptyString("Enter expense category: ");

            var expense = new Expense { Amount = amount, Category = category };
            dal.AddExpense(expense);
            Console.WriteLine("Expense added successfully!");
            WaitForKeyPress();
        }

        private static void ViewIncome()
        {
            var incomes = dal.GetAllIncomes();
            Console.WriteLine("Income Entries:");
            foreach (var income in incomes)
            {
                Console.WriteLine(income);
            }
            WaitForKeyPress();
        }

        private static void ViewExpenses()
        {
            var expenses = dal.GetAllExpenses();
            Console.WriteLine("Expense Entries:");
            foreach (var expense in expenses)
            {
                Console.WriteLine(expense);
            }
            WaitForKeyPress();
        }

        private static void SetBudget()
        {
            decimal limit = GetNonNegativeDecimal("Enter budget limit: ");
            dal.SetBudget(limit);
            Console.WriteLine($"Budget set to {limit:C}");
            WaitForKeyPress();
        }

        private static void ViewBudgetStatus()
        {
            var totalExpenses = dal.GetTotalExpenses();
            var budgetLimit = dal.GetBudgetLimit();
            Console.WriteLine($"Total Expenses: {totalExpenses:C}");
            Console.WriteLine(totalExpenses > budgetLimit ? "You have exceeded your budget!" : "You are within your budget.");
            WaitForKeyPress();
        }

        private static void GenerateReport()
        {
            var totalIncome = dal.GetTotalIncome();
            var totalExpenses = dal.GetTotalExpenses();
            var netBalance = totalIncome - totalExpenses;

            Console.WriteLine("=== Financial Report ===");
            Console.WriteLine($"Total Income: {totalIncome:C}");
            Console.WriteLine($"Total Expenses: {totalExpenses:C}");
            Console.WriteLine($"Net Balance: {netBalance:C}");
            WaitForKeyPress();
        }

        private static decimal GetPositiveDecimal(string prompt)
        {
            decimal value;
            do
            {
                Console.Write(prompt);
            } while (!decimal.TryParse(Console.ReadLine(), out value) || value <= 0);
            return value;
        }

        private static decimal GetNonNegativeDecimal(string prompt)
        {
            decimal value;
            do
            {
                Console.Write(prompt);
            } while (!decimal.TryParse(Console.ReadLine(), out value) || value < 0);
            return value;
        }

        private static string GetNonEmptyString(string prompt)
        {
            string value;
            do
            {
                Console.Write(prompt);
                value = Console.ReadLine();
            } while (string.IsNullOrWhiteSpace(value));
            return value;
        }

        private static void WaitForKeyPress()
        {
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
        }
    }
}

