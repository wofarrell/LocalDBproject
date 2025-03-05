using System.Dynamic;

namespace LOCALDATABASEPROJECT.Models;

internal class Flashcard
{
    public int Id {get; set;}
    public int Stack_id {get; set;}
    public string? Flashcard_question { get; set; }
    public string? Flashcard_answer { get; set; }
   

    //parameterless constructor for dapper. When retrieving data, Dapper instantiates objects using reflection.Without a parameterless constructor, it doesn’t know how to create LogItem from query results.
    public Flashcard() { }

    public Flashcard(int stack_id, string flashcard_question, string flashcard_answer)
    {
        Stack_id = stack_id;
        Flashcard_question = flashcard_question;
        Flashcard_answer = flashcard_answer;
    }

    
}