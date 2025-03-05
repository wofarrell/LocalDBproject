using System.Dynamic;

namespace LOCALDATABASEPROJECT.Models;

internal class StudySession
{
    public int StudySession_id {get; set;}
    public string? StudySession_date { get; set; }
    public int  Score { get; set; }

    //parameterless constructor for dapper. When retrieving data, Dapper instantiates objects using reflection.Without a parameterless constructor, it doesn’t know how to create LogItem from query results.
    public StudySession() { }

    public StudySession(string studysession_date, int score)
    {
        StudySession_date = studysession_date;
        Score = score;
        

    }

    
}