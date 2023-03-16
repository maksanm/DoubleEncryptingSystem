using Encoder.Client.Interfaces;
using System;
using System.Data.SqlClient;

namespace Encoder.Client.Services
{
    internal class MessageDatabaseFacade : IMessageDatabaseFacade
    {
        private static readonly string insertMessageQueryString = "INSERT INTO Message (Id, EncryptedValue) VALUES (@Id, @EncryptedValue)";
        private static readonly string connectionString = "Server=(localdb)\\mssqllocaldb;Database=DoubleEncryptionSystem;Trusted_Connection=True;MultipleActiveResultSets=true;";

        public bool InsertMessage(string id, string encryptedValue)
        {
            using SqlConnection connection = new SqlConnection(connectionString);
            SqlCommand command = new SqlCommand(insertMessageQueryString, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.Parameters.AddWithValue("@EncryptedValue", encryptedValue);
            connection.Open();

            try
            {
                var rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected != 1)
                {
                    Console.WriteLine("Incorrect number of database table rows affected!");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("SQL query execution unexpected error! Error message: " + ex.Message);
                return false;
            }
            return true;
        }
    }
}
