using System;
using System.Data.SqlClient;

namespace Encoder.Client
{
    class Program
    {
        private static readonly string queryString = "INSERT INTO Message (Id, EncryptedValue) VALUES (@Id, @EncryptedValue)";
        private static readonly string connectionString = "Server=(localdb)\\mssqllocaldb;Database=DoubleEncodingSystem;Trusted_Connection=True;MultipleActiveResultSets=true;";

        static void Main(string[] args)
        {
            while(true)
            {
                Console.WriteLine("Hello! Please, enter the message you want to proceed.");
                var message = Console.ReadLine();
                if (string.IsNullOrEmpty(message))
                    break;
                var id = Guid.NewGuid();

                using SqlConnection connection = new SqlConnection(connectionString);
                SqlCommand command = new SqlCommand(queryString, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@EncryptedValue", message);
                connection.Open();
                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    Console.WriteLine("Unexpected database error occurred!");
                    break;
                }
            }
        }
    }
}
