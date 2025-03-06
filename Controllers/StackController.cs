
using LOCALDATABASEPROJECT.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace LOCALDATABASEPROJECT.Controllers;

internal class StackController
{
    public void StackCreate(string stack_name)
    {
        DateTime sessionDateFull = DateTime.Now;
        string sessionDateShort = sessionDateFull.ToString();

        //stack_id auto generated
        // test entry string stack_name = "first stack";

        var newStack = new Stack(stack_name);

        string connectionString = @"Data Source=(localdb)\.\SharedLocalDB;AttachDbFilename=C:\Users\Sandwich\flashcards.mdf;Integrated Security=True";
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string insertQuery = @"INSERT INTO dbo.stacks (stack_name)
                    VALUES (@Stack_name);";

            connection.Execute(insertQuery, newStack);
            connection.Close();
        }
    }

    public void StackDelete(int stack_id)
    {

        string connectionString = @"Data Source=(localdb)\.\SharedLocalDB;AttachDbFilename=C:\Users\Sandwich\flashcards.mdf;Integrated Security=True";
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string deleteQuery = @"DELETE from dbo.stacks where stack_id = @Stack_id;";

            connection.Execute(deleteQuery, new { Stack_id = stack_id });
            connection.Close();
        }
    }

    public void StackView()
    {
        string connectionString = @"Data Source=(localdb)\.\SharedLocalDB;AttachDbFilename=C:\Users\Sandwich\flashcards.mdf;Integrated Security=True";
        using (var connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new("SELECT * from dbo.stacks;", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("{0}\t{1}",
                    reader.GetInt32(0),
                    reader.GetString(1));
                }
            }
            else
            {
                Console.WriteLine("No rows found.");
            }
            reader.Close();
            connection.Close();
        }
    }




}
