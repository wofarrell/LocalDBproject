
using LOCALDATABASEPROJECT.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using System.Security.Cryptography;
using System.Linq;

namespace LOCALDATABASEPROJECT.Controllers;

internal class StudySessionController
{

    public void StudySession(int stack_id)
    {

        int[] flashcardId = new int[20];
        string?[] flashcardQuestion = new string[20];
        string?[] flashcardAnswer = new string[20];

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
            int[] usedFlashcardIds = new int[flashcardId.Length];

            Random rnd = new Random();
            int studySessionScore = 0;
            //randomly show them to user
            //only show flashcard one time, go through 10 at a time as a session
            //I need a random number, and it has to be unique each time
            bool randomFlashcardPickBool = false;

            while (flashcardCounter <= 5)
            {
                int randomFlashcardId;

                //making sure our random is suitable
                do
                {
                    randomFlashcardId = rnd.Next(10);
                    bool contains = usedFlashcardIds.Contains(randomFlashcardId);
                    if (!contains)
                    {
                        foreach (int n in flashcardId)
                        {
                            if (n == randomFlashcardId)
                            {
                                usedFlashcardIds[flashcardCounter] = randomFlashcardId;
                                randomFlashcardPickBool = true;
                            }

                        }
                    }
                } while (randomFlashcardPickBool == false);
                randomFlashcardPickBool = true;


                //now show the flashcard based on the array placcement which is the random number generated above
                Console.WriteLine("Question:");
                Console.WriteLine(flashcardQuestion[randomFlashcardId]);
                Console.WriteLine("Please enter your answer");
                bool answerBool = false;
                do
                {
                    string? flashcardUserAnswer = Console.ReadLine();
                    if (flashcardUserAnswer != null)
                    {
                        if (flashcardUserAnswer == flashcardAnswer[randomFlashcardId])
                        {
                            Console.WriteLine("Correct!");
                            studySessionScore++;
                            answerBool = true;
                        }
                        else
                        {
                            Console.WriteLine("Incorrect");
                            answerBool = true;
                        }

                    }
                } while (answerBool == false);
                Console.WriteLine("Press any key to continue");
                Console.ReadLine();
                flashcardCounter++;
            }

            StudySessionLogCreate(studySessionScore,stack_id);
        }

    }

    public void StudySessionLogCreate(int score, int stack_id)
    {
        DateTime studysessionDateFull = DateTime.Now;
        string studysession_date = studysessionDateFull.ToString();

        //test entry int score = 1;

        var newStudySession = new StudySession(studysession_date, score, stack_id);

        string connectionString = "Data Source=(localdb)\\.\\SharedLocalDB;AttachDbFilename=C:\\Users\\Sandwich\\flashcards.mdf;Integrated Security=True";
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string insertQuery = @"INSERT INTO dbo.studysessions (studysession_date, score, stack_id) 
                    VALUES (@StudySession_date,@Score,@Stack_id);";

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

    public void StudySessionView()
    {
        string connectionString = @"Data Source=(localdb)\.\SharedLocalDB;AttachDbFilename=C:\Users\Sandwich\flashcards.mdf;Integrated Security=True";
        using (var connection = new SqlConnection(connectionString))
        {
            SqlCommand command = new("SELECT * from dbo.studysessions;", connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    Console.WriteLine("{0}\t{1}\t{2}\t{3}\t",
                    reader.GetInt32(0),
                    reader.GetInt32(1),
                    reader.GetString(2),
                    reader.GetInt32(3)
                    );
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