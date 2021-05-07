using System;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace SqlInjectionTest
{
    class Program
    {
        static async Task Main(string[] args)
        {
            

            Console.WriteLine("Insera um nome ou um SqlInjection:");

            var unsafeString = Console.ReadLine();


            await using var conn = new SqlConnection();
            await using var command = conn.CreateCommand();
            command.CommandText = "SELECT * FROM usu_user WHERE user_first_name = '" + unsafeString + "'";
            await using var reader = await command.ExecuteReaderAsync();
            if (!reader.HasRows)
                return;

            var arr = new object[reader.FieldCount];

            while(await reader.ReadAsync())
            {
                reader.GetValues(arr);
                Console.WriteLine(string.Join(", ", arr));
            }
        }

        public static async void Test()
        {
            await Task.Delay(1000);
            await Task.Delay(2000);
            Console.WriteLine("x");
        }

        public static void Bar(SqlConnection connection, string parametro)
        {
            SqlCommand command;
            string sensitiveQuery = string.Format("INSERT INTO Users (name) VALUES (\"{0}\")", parametro);
            command = new SqlCommand(sensitiveQuery);

            command.CommandText = sensitiveQuery;

            SqlDataAdapter adapter;
            adapter = new SqlDataAdapter(sensitiveQuery, connection);
        }
    }
}
