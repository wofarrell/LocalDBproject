
using LOCALDATABASEPROJECT.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace localDB;
class Program

{

    static void Main(string[] args)
    {
        Console.Clear();
        StackCreate();
        FlashcardCreate();
        StudySessionCreate();

        Console.WriteLine("added rows into tables");

    }
    static void StackCreate()
    {

    
        DateTime sessionDateFull = DateTime.Now;
        string sessionDateShort = sessionDateFull.ToString();


        //need to query
        //stack_id auto generated
        string stack_name = "first stack";

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

    static void FlashcardCreate()
    {
        
        int stack_id = 1;
        string? flashcard_question = "what is the color of the sky?";
        string? flashcard_answer = "Blue";
        
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


    static void StudySessionCreate()
    {
        DateTime studysessionDateFull = DateTime.Now;
        string studysession_date = studysessionDateFull.ToString();

        int score = 1;
        
        var newStudySession = new StudySession(studysession_date, score);

        string connectionString = "Data Source=(localdb)\\.\\SharedLocalDB;AttachDbFilename=C:\\Users\\Sandwich\\flashcards.mdf;Integrated Security=True";
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string insertQuery = @"INSERT INTO dbo.studysessions (studysession_date, score) 
                    VALUES (@StudySession_date,@Score);";

            connection.Execute(insertQuery, newStudySession);
            connection.Close();
        }
    }
}

