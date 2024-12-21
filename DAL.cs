using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceTrackerWithAdo
{
    public class DAL
    {
        private readonly string _connectionString;
        public DAL(string connectionString)
        {
            _connectionString = connectionString;
        }
        public void AddIncome(Income income)
        {
            SqlConnection sqlconnection = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand
            {
                Connection = sqlconnection,
                CommandText = "INSERT INTO Incomes (Amount, Source) VALUES (@amount, @source)"
            };
            cmd.Parameters.AddWithValue("@amount", income.Amount);
            cmd.Parameters.AddWithValue("@source", income.Source);
            sqlconnection.Open();
            cmd.ExecuteNonQuery();
            sqlconnection.Close();

        }
        public void AddExpense(Expense expense)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "INSERT INTO Expenses (Amount, Category) VALUES (@amount, @category)"
            };
            cmd.Parameters.AddWithValue("@amount", expense.Amount);
            cmd.Parameters.AddWithValue("@category", expense.Category);

            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();

        }
        public List<Income> GetAllIncomes()
        {
            var incomes = new List<Income>();
            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT Amount, Source FROM Incomes"
            };

            sqlConnection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    incomes.Add(new Income
                    {
                        Amount = reader.GetDecimal(0),
                        Source = reader.GetString(1)
                    });
                }
            }
            sqlConnection.Close();

            return incomes;
        }
        public List<Expense> GetAllExpenses()
        {
            var expenses = new List<Expense>();
            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT Amount, Category FROM Expenses"
            };

            sqlConnection.Open();
            using (var reader = cmd.ExecuteReader())
            {
                while (reader.Read())
                {
                    expenses.Add(new Expense
                    {
                        Amount = reader.GetDecimal(0),
                        Category = reader.GetString(1)
                    });
                }
            }
            sqlConnection.Close();

            return expenses;
        }
        public void SetBudget(decimal limit)
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "IF EXISTS (SELECT * FROM Budget) " +
                              "UPDATE Budget SET Limit = @limit " +
                              "ELSE INSERT INTO Budget (Limit) VALUES (@limit)"
            };
            cmd.Parameters.AddWithValue("@limit", limit);

            sqlConnection.Open();
            cmd.ExecuteNonQuery();
            sqlConnection.Close();

        }
        public decimal GetTotalExpenses()
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT ISNULL(SUM(Amount), 0) FROM Expenses"
            };

            sqlConnection.Open();
            return (decimal)cmd.ExecuteScalar();

        }
        public decimal GetTotalIncome()
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT ISNULL(SUM(Amount), 0) FROM Incomes"
            };

            sqlConnection.Open();
            return (decimal)cmd.ExecuteScalar();

        }
        public decimal GetBudgetLimit()
        {
            SqlConnection sqlConnection = new SqlConnection(_connectionString);

            SqlCommand cmd = new SqlCommand
            {
                Connection = sqlConnection,
                CommandText = "SELECT ISNULL(Limit, 0) FROM Budget"
            };

            sqlConnection.Open();
            return (decimal)cmd.ExecuteScalar();

        }


    }


}

