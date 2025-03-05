using System.Dynamic;

namespace LOCALDATABASEPROJECT.Models;

internal class Stack
{
    public int Stack_id {get; set;}
    public string? Stack_name { get; set; }

    //parameterless constructor for dapper. When retrieving data, Dapper instantiates objects using reflection.Without a parameterless constructor, it doesn’t know how to create LogItem from query results.
    public Stack() { }

    public Stack(string stack_name)
    {
        Stack_name = stack_name;
    }

    
}