using System;
using Microsoft.Data.Sqlite;

namespace SQLPrac
{


    class Program
    {
        // Put your SQL queries here:
        private static string task01 = null;
        private static string task02 = null;
        private static string task03 = null;
        private static string task04 = null;
        private static string task05 = null;
        private static string task06 = null;

        /// <summary>
        /// Executes the SQL query corresponding to the task.
        /// </summary>
        /// <param name="connection">Connection to a SQLite database</param>
        /// <param name="task">A console key that specifies the number of the ефыл to be executed</param>
        public static void ExecuteTask(SqliteConnection connection, ConsoleKey task)
        {
            switch (task)
            {
                case ConsoleKey.D1:
                    SQLUtilities.ExecuteAndPrint(connection, task01);
                    break;
                case ConsoleKey.D2:
                    SQLUtilities.ExecuteAndPrint(connection, task02);
                    break;
                case ConsoleKey.D3:
                    SQLUtilities.ExecuteAndPrint(connection, task03);
                    break;
                case ConsoleKey.D4:
                    SQLUtilities.ExecuteAndPrint(connection, task04);
                    break;
                case ConsoleKey.D5:
                    SQLUtilities.ExecuteAndPrint(connection, task05);
                    break;
                case ConsoleKey.D6:
                    SQLUtilities.ExecuteAndPrint(connection, task06);
                    break;
                default:
                    Console.WriteLine("Unknown task.");
                    break;
            }
            Console.WriteLine($"{Environment.NewLine}Press any key to continue...");
            Console.ReadKey();
            Console.Clear();
        }

        static void Main()
        {
            using (var connection = new SqliteConnection("Data Source=olympics.db"))
            {
                connection.Open();
                do
                {
                    Console.WriteLine($"Press ESC to exit.{Environment.NewLine}");
                    Console.Write("Select task to execute [1; 6]: ");

                    ConsoleKey task = Console.ReadKey().Key;
                    if (task == ConsoleKey.Escape)
                        break;
                    Console.Clear();
                    ExecuteTask(connection, task);
                } while (true);
            }
        }
    }
}