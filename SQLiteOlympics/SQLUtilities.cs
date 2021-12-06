using System;
using System.IO;
using System.Text;
using Microsoft.Data.Sqlite;

namespace SQLPrac
{
    class SQLUtilities {
        public static string Truncate(string value, int maxChars)
        {
            return value.Length <= maxChars ? value : value.Substring(0, maxChars - 3) + "...";
        }

        /// <summary>
        /// Prints the result of the query to the console.
        /// </summary>
        /// <param name="reader">Result of the query</param>
        public static void PrintTable(SqliteDataReader reader)
        {
            StringBuilder sb = new StringBuilder();
            // Print header
            string sep = " " + new string('-', 28 * reader.FieldCount + 1) + Environment.NewLine;
            sb.Append(sep);
            sb.Append(" | ");
            for (int i = 0; i < reader.FieldCount; ++i)
            {
                sb.AppendFormat("{0, -25}", Truncate(reader.GetName(i), 25));
                sb.Append(" | ");
            }
            sb.Append(Environment.NewLine + sep);

            // Print data
            int count = 0;
            while (reader.Read())
            {
                sb.Append(" | ");
                for (int i = 0; i < reader.FieldCount; ++i)
                {
                    sb.AppendFormat("{0, -25}", Truncate(reader.GetString(i), 25));
                    sb.Append(" | ");
                }
                sb.AppendLine();
                count++;
            }
            sb.Append(sep);
            if (count > 0)
            {
                Console.WriteLine(sb.ToString());
            }
            Console.WriteLine("Total results: {0}", count);
        }

        /// <summary>
        /// Populates the database using the commands written in the file.
        /// </summary>
        /// <param name="path">Path to the file with commands</param>
        public void FillDB(string path = "Olympics DB script.txt")
        {
            using (var connection = new SqliteConnection("Data Source=olimpics.db"))
            {
                connection.Open();

                using (StreamReader sr = File.OpenText(path))
                {
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        StringBuilder sb = new StringBuilder(line);
                        while (!(line = sr.ReadLine()).EndsWith(";"))
                        {
                            sb.AppendLine(line);
                        }
                        sb.AppendLine(line);

                        SqliteCommand command = new SqliteCommand();
                        command.Connection = connection;
                        command.CommandText = sb.ToString();
                        command.ExecuteNonQuery();

                        Console.WriteLine($"{Environment.NewLine}Executed:{Environment.NewLine}{command.CommandText}");
                    }
                }
            }
        }

        /// <summary>
        /// Executes the command from commandText against the database and prints the result.
        /// </summary>
        /// <param name="connection">Connection to a SQLite database</param>
        /// <param name="commandText">Thext of the command to execute</param>
        public static void ExecuteAndPrint(SqliteConnection connection, string commandText)
        {
            var command = connection.CreateCommand();
            command.CommandText = commandText;
            using (var reader = command.ExecuteReader())
            {
                PrintTable(reader);
            }
        }
    }
}