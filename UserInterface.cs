


using LOCALDATABASEPROJECT.Controllers;
using LOCALDATABASEPROJECT.Models;
using Microsoft.IdentityModel.Tokens;


namespace LOCALDATABASEPROJECT;

internal class UserInterface
{
    FlashcardController flashcardController = new();
    StackController stackController = new();

    //private readonly LogInsert _logInsert = new();
    //private readonly LogDelete _logDelete = new();
    //private readonly LogUpdate _logUpdate = new();
    //private readonly LogView _logView = new();

    internal void MainMenu()
    {

        bool menuBool = true;
        while (menuBool)
        {
            Console.Clear();
            Console.WriteLine("Flashcard Study Session Console App");
            Console.WriteLine("Please make a selection by number");

            Console.WriteLine("1. Manage flashcard stacks \n2. Manage flashcards \n3. Start a study session \n4. View study sessions \n'exit'");
            bool validEntry = false;
            string menuchoice = "";
            string? inputChoice = "";
            do
            {
                inputChoice = Console.ReadLine();
                {
                    if (inputChoice != null && (inputChoice == "1" || inputChoice == "2" || inputChoice == "3" || inputChoice == "4" || inputChoice == "exit"))
                    {
                        //if (inputChoice == "1" || inputChoice == "2" || inputChoice == "3" || inputChoice == "4")
                        menuchoice = inputChoice;
                        validEntry = true;
                    }
                    else
                    {
                        Console.WriteLine("\nplease enter a valid input");
                    }
                }
            } while (validEntry == false);


            switch (menuchoice)
            {

                case "1": //Manage flashcard stacks
                    StackMenu();
                    break;

                case "2": //Manage flashcards
                    FlashcardMenu();
                    break;

                case "3":  //Start a study session                 
                    StudySession();
                    break;

                case "4": //View study sessions
                    StudySessionView();
                    break;

                //exit
                case "exit":
                    menuBool = false;
                    break;
            }
        }
    }

    internal void StackMenu()
    {

        bool stackmenubool = true;
        do
        {
            Console.WriteLine("Choose option by number:\n1. View flashcard stacks\n2. Create Flashcard Stack\n3. Delete Flashcard Stack\n4. exit");
            string? fcMenuChoice = Console.ReadLine();

            {
                //View stacks
                if (fcMenuChoice != null && (fcMenuChoice == "1"))
                {
                    stackController.StackView();
                    Console.ReadLine();
                    Console.WriteLine("Press any key to go back to the menu");
                }

                //Create a new stack by name
                if (fcMenuChoice != null && (fcMenuChoice == "2"))
                {
                    Console.WriteLine("Enter a name for the new stack of flashcards:");
                    string? stackNameEntry = "";
                    bool stackNameBool = false;
                    do
                    {
                        stackNameEntry = Console.ReadLine();
                        if (stackNameEntry != null && stackNameEntry != "")
                        {
                            stackController.StackCreate(stackNameEntry);
                            Console.WriteLine("Stack successfuly created");
                            stackNameBool = true;
                        }
                        else
                        {
                            Console.WriteLine("\nplease enter a valid name");
                        }
                    } while (stackNameBool == false);
                }

                //Delete flashcard stack by ID
                if (fcMenuChoice != null && (fcMenuChoice == "3"))
                {
                    Console.WriteLine("Enter the id of the flashcard stack that you want to delete, or type 'exit'");
                    int stackDeleteEntry;
                    string? stackDeleteEntryString = "";
                    bool stackDeleteBool = false;
                    do
                    {
                        stackDeleteEntryString = Console.ReadLine();
                        bool VariableEntry = int.TryParse(stackDeleteEntryString, out stackDeleteEntry);

                        if (stackDeleteEntry != 0)
                        {
                            stackController.StackDelete(stackDeleteEntry);
                            Console.WriteLine("Stack successfuly deleted");
                            stackDeleteBool = true;
                        }
                        else
                        {
                            Console.WriteLine("\nplease enter a valid name");
                        }
                    } while (stackDeleteBool == false);
                }

                //Exit
                if (fcMenuChoice != null && (fcMenuChoice == "4"))
                {
                    stackmenubool = false;
                }

            }


        } while (stackmenubool == true);
    }

    internal void FlashcardMenu()
    {


        bool flashcardmenubool = true;

        do
        {
            Console.WriteLine("Choose option by number or type 'exit':\n1. View flashcards by stack\n2. Create Flashcard\n3. Delete Flashcard");

            bool validEntry = false;
            string fcmenuchoice = "";
            do
            {
                string? fcinputChoice = Console.ReadLine();
                {
                    if (fcinputChoice != null && (fcinputChoice == "1" || fcinputChoice == "2" || fcinputChoice == "3" || fcinputChoice == "exit"))
                    {
                        //if (inputChoice == "1" || inputChoice == "2" || inputChoice == "3" || inputChoice == "4")
                        fcmenuchoice = fcinputChoice;
                        validEntry = true;
                    }
                    else
                    {
                        Console.WriteLine("\nplease enter a valid input");
                    }
                }
            } while (validEntry == false);


            switch (fcmenuchoice)
            {

                case "1": //View flashcards by stack
                    Console.WriteLine("Choose a stack by their ID below or type 'exit'");
                    stackController.StackView();

                    int stackSelectionEntry;
                    string? stackEntryString = "";
                    bool stackSelectionBool = false;
                    do
                    {
                        stackEntryString = Console.ReadLine();


                        if (stackEntryString != null)
                        {
                            bool VariableEntry = int.TryParse(stackEntryString, out stackSelectionEntry);
                            flashcardController.FlashcardView(stackSelectionEntry);
                            Console.WriteLine("Press any key to go back to the flashcard menu");
                            Console.ReadKey();
                            stackSelectionBool = true;
                        }
                        if (stackEntryString == "exit")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nplease enter a valid stack id");
                        }
                    } while (stackSelectionBool == false);

                    break;

                case "2": //Create Flashcard
                    //select stack. stack must exist. 

                    int stack_id = 0;
                    string? flashcard_question = "";
                    string? flashcard_answer = "";

                    Console.WriteLine("select the stack the flashcard will be under from the list below, or type 'exit to return");
                    stackController.StackView();
                    bool flashcardStackSelectionBool = false;
                    do
                    {
                        string? flashcardStackEntry = Console.ReadLine();
                        if (flashcardStackEntry != null)
                        {
                            bool VariableEntry = int.TryParse(flashcardStackEntry, out int flashcardStackSelectionEntry);

                            if (VariableEntry == true)
                            {
                                Console.WriteLine($"Flashcard stack {flashcardStackSelectionEntry} chosen ");
                                stack_id = flashcardStackSelectionEntry;
                                flashcardStackSelectionBool = true;
                            }
                            else
                            {
                                Console.WriteLine("\nplease enter a valid stack id");
                            }
                        }
                        if (flashcardStackEntry == "exit")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nplease enter a valid stack id");
                        }
                    } while (flashcardStackSelectionBool == false);

                    //can use flashcardStackSelectionEntry in final insert query

                    //flashcard question
                    Console.WriteLine("Enter the flashcard question");
                    bool flashcardQuestionBool = false;

                    do
                    {

                        string? flashcardQuestionEntry = Console.ReadLine();
                        if (flashcardQuestionEntry != null)
                        {

                            Console.WriteLine($"Flashcard question '{flashcardQuestionEntry}' entered.");
                            flashcard_question = flashcardQuestionEntry;
                            flashcardQuestionBool = true;
                        }
                        if (flashcardQuestionEntry == "exit")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nplease enter a valid flashcard question");
                        }

                    } while (flashcardQuestionBool == false);

                    //flashcard answer
                    Console.WriteLine("Enter the flashcard answer");
                    bool flashcardAnswerBool = false;

                    do
                    {
                        string? flashcardAnswerEntry = Console.ReadLine();
                        if (flashcardAnswerEntry != null)
                        {

                            Console.WriteLine($"Flashcard answer '{flashcardAnswerEntry}' entered.");
                            flashcard_answer = flashcardAnswerEntry;
                            flashcardAnswerBool = true;
                        }
                        if (flashcardAnswerEntry == "exit")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nplease enter a valid flashcard question");
                        }

                    } while (flashcardAnswerBool == false);

                    //converting them to these 
                    /*
                    int stack_id;
                    string? flashcard_question = "";
                    string? flashcard_answer = "";
                    */

                    flashcardController.FlashcardCreate(stack_id, flashcard_question, flashcard_answer);
                    Console.WriteLine("\nFlashcard successfully created");

                    break;

                case "3":  //Delete Flashcard 
                    //listing stacks to choose from                
                    Console.WriteLine("Choose a flashcard stack to view by their ID below or type 'exit'");
                    stackController.StackView();
                    int fcStackSelectionEntry;
                    string? fcStackEntryString = "";
                    bool fcStackSelectionBool = false;
                    do
                    {
                        fcStackEntryString = Console.ReadLine();
                        if (fcStackEntryString != null)
                        {
                            bool VariableEntry = int.TryParse(fcStackEntryString, out fcStackSelectionEntry);
                            flashcardController.FlashcardView(fcStackSelectionEntry);
                            Console.WriteLine("Press any key to go back to the flashcard menu");
                            Console.ReadKey();
                            stackSelectionBool = true;
                        }
                        if (fcStackEntryString == "exit")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nplease enter a valid stack id");
                        }
                    } while (fcStackSelectionBool == false);

                    //delete flashcard by flashcard_id
                    int flashcardIdSelection;
                    string? fcEntryString = "";
                    bool fcSelectionBool = false;

                    do
                    {
                        fcEntryString = Console.ReadLine();
                        if (fcEntryString != null)
                        {
                            bool VariableEntry = int.TryParse(fcEntryString, out flashcardIdSelection);
                            flashcardController.FlashcardDelete(flashcardIdSelection);
                            Console.WriteLine("Press any key to go back to the flashcard menu");
                            Console.ReadKey();
                            stackSelectionBool = true;
                        }
                        if (fcEntryString == "exit")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nplease enter a valid stack id");
                        }
                    } while (fcSelectionBool == false);

                    Console.WriteLine("\nFlashcard successfully deleted");
                    break;

                //exit
                case "exit":
                    flashcardmenubool = false;
                    break;
            }
        } while (flashcardmenubool == true);
    }

    internal void StudySession()
    {
        //load stack of flashcards into memory by  stack_id
        Console.WriteLine("Enter the number of the stack below to start a study session");
        stackController.StackView();

        string? ssStackSelectionString = "";
        int ssStackSelectionInt = 0;
        bool ssStackSelectionBool = false;
        do
                    {
                        ssStackSelectionString = Console.ReadLine();
                        if (ssStackSelectionString != null)
                        {
                            bool VariableEntry = int.TryParse(ssStackSelectionString, out ssStackSelectionInt);
                            Console.WriteLine($"Stack #{ssStackSelectionInt} loaded");

                            
                            Console.ReadKey();
                            ssStackSelectionBool = true;
                        }
                        if (ssStackSelectionString == "exit")
                        {
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nplease enter a valid stack id");
                        }
                    } while (ssStackSelectionBool == false);


        
    }

    internal void StudySessionView()
    {

    }
}


/*
    private void LogInsert()
    {
        _logInsert.LogOperation();
    }

    private void LogDelete()
    {
        _logDelete.LogOperation();
    }

    private void LogUpdate()
    {
        _logUpdate.LogOperation();
    }

    private void LogView()
    {
        _logView.LogOperation();
    }

            StackCreate();
    FlashcardCreate();
    StudySessionCreate();

*/
