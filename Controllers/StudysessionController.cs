
using LOCALDATABASEPROJECT.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;

namespace LOCALDATABASEPROJECT.Controllers;

internal class StudySessionController
{

    public void StudySession(int stack_id)
    {

        int[] flashcardId = new int[20];
        string?[] flashcardQuestion = [];
        string?[] flashcardAnswer = [];

        //load flashcards into memory from stack
        {
            string connectionString = @"Data Source=(localdb)\.\SharedLocalDB;AttachDbFilename=C:\Users\Sandwich\flashcards.mdf;Integrated Security=True";
            using (var connection = new SqlConnection(connectionString))
            {
                //grab all records from 


                SqlCommand command = new("SELECT * from dbo.flashcards;", connection);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    int i = 0;

                    //puts flashcard data into 3 arrays Id, Q, A
                    while (reader.Read())
                    {

                        flashcardId[i] = reader.GetInt32(0);
                        // skip stack id - reader.GetInt32(1),
                        flashcardQuestion[i] = reader.GetString(2);
                        flashcardAnswer[i] = reader.GetString(3);
                        i++;
                    }
                }
                else
                {
                    Console.WriteLine("No rows found.");
                }
                reader.Close();
                connection.Close();
            }

            
            //set up flashcard count to stop at 10

            int flashcardCounter = 0;

            int flashcardRecordsAmount = flashcardId.Length;
            int[] usedFlashcardsById = new int[flashcardId.Length];


            while (flashcardCounter <= 10)
            {
                
            //randomly show them to user
            //only show flashcard one time, go through 10 at a time as a session

            //start with say 5 flashcards. 
            //I need a random number, and it has to be unique each time

            var randomFlashcardId = new Random();
            
            
            flashcardCounter++;



            }

        }








    }

    public void StudySessionLogCreate(int score)
    {
        DateTime studysessionDateFull = DateTime.Now;
        string studysession_date = studysessionDateFull.ToString();

        //test entry int score = 1;

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

    public void StudySessionLogDelete(int studysession_id)
    {
        string connectionString = "Data Source=(localdb)\\.\\SharedLocalDB;AttachDbFilename=C:\\Users\\Sandwich\\flashcards.mdf;Integrated Security=True";
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string insertQuery = @"DELETE from dbo.studysessions where studysession_id = @Studysessionid;";

            connection.Execute(insertQuery, new { Studysessionid = studysession_id });
            connection.Close();
        }
    }

}