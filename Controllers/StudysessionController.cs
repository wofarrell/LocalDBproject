
using LOCALDATABASEPROJECT.Models;
using Dapper;
using Microsoft.Data.SqlClient;

namespace LOCALDATABASEPROJECT.Controllers;

internal class StudySessionController
{

    public void StudySessionCreate(int score)
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

    public void StudySessionDelete(int studysession_id)
    {
        string connectionString = "Data Source=(localdb)\\.\\SharedLocalDB;AttachDbFilename=C:\\Users\\Sandwich\\flashcards.mdf;Integrated Security=True";
        using (var connection = new SqlConnection(connectionString))
        {
            connection.Open();
            string insertQuery = @"DELETE from dbo.studysessions where studysession_id = @Studysessionid;";

            connection.Execute(insertQuery, new { Studysessionid = studysession_id});
            connection.Close();
        }
    }

}