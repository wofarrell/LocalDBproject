
using LOCALDATABASEPROJECT.Models;
using Dapper;
using Microsoft.Data.SqlClient;
using LOCALDATABASEPROJECT;

namespace Main;
class Program

{
    static void Main(string[] args)
    {
        Console.Clear();
        UserInterface userInterface = new();
        userInterface.MainMenu();
    }
}

