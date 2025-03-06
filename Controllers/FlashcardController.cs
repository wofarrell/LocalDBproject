
using LOCALDATABASEPROJECT.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace LOCALDATABASEPROJECT.Controllers;

internal class FlashcardController
{
    public void FlashcardCreate(int stack_id, string flashcard_question, string flashcard_answer)
    {
        // test entry int stack_id = 1;
        //test entry string? flashcard_question = "what is the color of the sky?";
        // test entry string? flashcard_answer = "Blue";

        var newFlashcard = new Flashcard(stack_id, flashcard_question, flashcard_answer);

        string connectionString = "Data Source=(localdb)\\.\\SharedLocalDB;AttachDbFilename=C:\\Users\\Sandwich\\flashcards.mdf;Integrated Security=True";
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string insertQuery = @"INSERT INTO dbo.flashcards (stack_id, flashcard_question, flashcard_answer) 
                    VALUES (@stack_id, @Flashcard_question, @Flashcard_answer);";

            connection.Execute(insertQuery, newFlashcard);
            connection.Close();
        }
    }

    public void FlashcardDelete(int flashcard_id)
    {
        string connectionString = @"Data Source=(localdb)\.\SharedLocalDB;AttachDbFilename=C:\Users\Sandwich\flashcards.mdf;Integrated Security=True";
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string insertQuery = @"DELETE from dbo.flashcards where flashcard_id = @Flashcard_id;";

            connection.Execute(insertQuery, new { Flashcard_id = flashcard_id });
            connection.Close();
        }
    }

    public void FlashcardView(int stack_id)
    {
        string connectionString = @"Data Source=(localdb)\.\SharedLocalDB;AttachDbFilename=C:\Users\Sandwich\flashcards.mdf;Integrated Security=True";
        using (var connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new("SELECT * from dbo.flashcards;", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}",
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetString(2),
                    reader.GetString(3));
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

