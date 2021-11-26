using System;
using System.Linq;
using System.Text;
using System.IO;

class Program
{

    static void Main(string[] args)
    {
        Console.WriteLine("Please Login: ");
        login();
        mainMenu();
    }

    public static void login()
    {

        string username = "Pratyush";
        string password = "Password";
        int attempts = 3;

        do
        {

            Console.WriteLine("Enter username:");
            string userInputName = Console.ReadLine();
            Console.WriteLine("Enter password:");
            string userInputPassword = Console.ReadLine();
            if (username == userInputName && password == userInputPassword)
            {

                Console.WriteLine("Accepted... WELCOME!");
                break;
            }
            else
            {

                Console.WriteLine("WRONG");
                attempts--;
                Console.WriteLine($"Remaining attempts {attempts}");
                if (attempts == 0)
                {

                    Console.WriteLine("SECURITY ERROR");
                    Environment.Exit(0);
                }
                Console.ReadKey();
                Console.Clear();
            }
        } while (attempts != 0); 
    }

    public static void mainMenu()
    {

        Console.Clear();
        Console.WriteLine("WELCOME to the main menu!");
        Console.WriteLine("What would you like to do?");
        Console.WriteLine("1. Run Quiz");
        Console.WriteLine("2. Add student Details and Ski Times");
        Console.WriteLine("3. View all student details.");
        Console.WriteLine("4. View students in each group");
        Console.WriteLine("5. View quiz scores");
        Console.WriteLine("6. View student detail by ID");
        Console.WriteLine("7. Exit Portal");
        string choice = Console.ReadLine();

        if (choice == "1")
        {

            Console.WriteLine("Quiz Running...");
            quizGame();
        }
        else if (choice == "2")
        {

            Console.Clear();
            Console.WriteLine("Welcome to the student Addin Details");
            addStudentDetails();
        }
        else if (choice == "3")
        {

            Console.Clear();
            Console.WriteLine("Here are the details of each individual student");
            listStudentDetails();
        }
        else if (choice == "4")
        {

            Console.Clear();
            Console.WriteLine("Student Groups");
            listStudentByGroup();
        }
        else if (choice == "5")
        {
            
            Console.Clear();
            Console.WriteLine("Here are the quiz scores");
            getQuizScores();
        }
        else if (choice == "6")
        {
            
            Console.Clear();
            Console.WriteLine("What is the id of the pupil");
            getStudentDetailsByID(Console.ReadLine());
        }
        else if (choice == "7")
        {

            Console.WriteLine("Bye bye!");
            Environment.Exit(0);
        }
    }
    
    public static void quizGame()
    {

        Console.WriteLine("Please put in the Pupil's ID: ");
        string id = Console.ReadLine();
        string name = null;
        
        string[] lines = File.ReadAllLines("../../../PupilSkiTimes.txt");

        for (int i = 0; i < lines.Length; i++)
        {
            
            string[] splitStudentDetails = lines[i].Split("|");

            if (splitStudentDetails[9] == id)
            {

                name = splitStudentDetails[0];
            }
        }
        
        Console.WriteLine($"User forename: {name}");

        //Console.Clear();

        int score = 0;
        string[] questions = new string[] { "Largest planet ? ", "Worse song ever ? ", "Capital of Peru?", "Roman god of War ?" };
        string[] answers = new string[] { "Jupiter", "Castles in the Sky", "Lima", "Mars" };

        Random random = new Random();

        for (int i = 0; i < questions.Length + 5; i++)
        {

            int a = random.Next(answers.Length);

            Console.WriteLine(questions[a]);
            string userAnswer = Console.ReadLine();

            if (userAnswer.Equals(answers[a], StringComparison.OrdinalIgnoreCase))
            {

                Console.WriteLine("Correct!");
                score++;
            }
            else
            {

                Console.WriteLine("Wrong");
            }
        }
        
        using (StreamWriter writer = new StreamWriter("../../../PupilQuizScores.txt", true))
        {
            
            writer.Write($"{id}|{name}|{score}");
            writer.Write(Environment.NewLine);
            writer.Flush();
            writer.Close();
        }

        Console.WriteLine($"Good Job! Final Score: {score}");
        Console.WriteLine("Hit Enter to go back to the main menu");
        Console.ReadKey();
        mainMenu();

    }

    public static string getPreviousID()
    {
        
        string[] lines = File.ReadAllLines("../../../PupilSkiTimes.txt");
        string[] splitStudentDetails = lines[lines.Length-1].Split("|");

        string id = splitStudentDetails[9];
        return id;
    }
    
    public static void addStudentDetails()
    {
        Console.WriteLine("Enter User forename");
        string foreName = Console.ReadLine();

        Console.WriteLine("Enter User lastname");
        string lastName = Console.ReadLine();

        Console.WriteLine("Ski time 1");
        int skiTime1 = int.Parse(Console.ReadLine());

        Console.WriteLine("Ski time 2");
        int skiTime2 = int.Parse(Console.ReadLine());

        Console.WriteLine("Ski time 3");
        int skiTime3 = int.Parse(Console.ReadLine());

        Console.WriteLine("Ski time 4");
        int skiTime4 = int.Parse(Console.ReadLine());

        Console.WriteLine("Ski time 5");
        int skiTime5 = int.Parse(Console.ReadLine());

        int averageSkiTime = (skiTime1 + skiTime2 + skiTime3 + skiTime4 + skiTime5) / 5;

        string group = null;

        int pupilID = int.Parse(getPreviousID())+1;

        if (averageSkiTime < 15)
        {

            group = "Intermediate";
        }
        else if (averageSkiTime >= 16 && averageSkiTime <= 30)
        {

            group = "Advanced";
        }
        else if (averageSkiTime > 30)
        {

            group = "Beginner";
        }

        using (StreamWriter writer = new StreamWriter("../../../PupilSkiTimes.txt", true))
        {


            writer.Write($"{foreName}|{lastName}|{skiTime1}|{skiTime2}|{skiTime3}|{skiTime4}|{skiTime5}|{averageSkiTime}|{group}|{pupilID}");
            writer.Write(Environment.NewLine);
            writer.Flush();

            writer.Close();
        }
        Console.WriteLine("Hit Enter to go back to the main Menu");
        Console.ReadKey();
        mainMenu();
    }

    public static void listStudentDetails()
    {
        string[] lines = File.ReadAllLines("../../../PupilSkiTimes.txt");

        for (int i = 0; i < lines.Length; i++)
        {

            string[] splitStudentDetails = lines[i].Split("|");
            Console.WriteLine($"Forename: {splitStudentDetails[0]}");
            Console.WriteLine($"Lastname: {splitStudentDetails[1]}");
            Console.WriteLine($"Ski time 1: {splitStudentDetails[2]}");
            Console.WriteLine($"Ski time 2: {splitStudentDetails[3]}");
            Console.WriteLine($"Ski time 3: {splitStudentDetails[4]}");
            Console.WriteLine($"Ski time 4: {splitStudentDetails[5]}");
            Console.WriteLine($"Ski time 5: {splitStudentDetails[6]}");
            Console.WriteLine($"Average ski time: {splitStudentDetails[7]}");
            Console.WriteLine($"Ski group: {splitStudentDetails[8]}");
            Console.WriteLine($"User ID: {splitStudentDetails[9]}");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
        }

        Console.WriteLine("Hit Enter to go back to the main Menu");
        Console.ReadKey();
        mainMenu();
    }

    public static void listStudentByGroup()
    {

        string[] lines = File.ReadAllLines("../../../PupilSkiTimes.txt");

        string[] beginner = new string[lines.Length];
        string[] advanced = new string[lines.Length];
        string[] intermediate = new string[lines.Length];

        for (int i = 0; i < lines.Length; i++)
        {

            string[] splitStudentDetails = lines[i].Split("|");
            if (splitStudentDetails[8] == "Beginner")
            {

                beginner[i] = ($"{splitStudentDetails[0]} {splitStudentDetails[1]}");
            } else if (splitStudentDetails[8] == "Advanced")
            {

                advanced[i] = ($"{splitStudentDetails[0]} {splitStudentDetails[1]}");
            } else if (splitStudentDetails[8] == "Intermediate")
            {

                intermediate[i] = ($"{splitStudentDetails[0]} {splitStudentDetails[1]}");
            }
        }
        Console.WriteLine("People in Beginner group: ");
        for (int i = 0; i < beginner.Length; i++)
        {

            Console.WriteLine(beginner[i]);
        }
        Console.WriteLine("\n");
        Console.WriteLine("\n");



        Console.WriteLine("People in Advanced group: ");
        for (int i = 0; i < advanced.Length; i++)
        {

            Console.WriteLine(advanced[i]);
        }
        Console.WriteLine("\n");
        Console.WriteLine("\n");



        Console.WriteLine("People in Intermediate group: ");
        for (int i = 0; i < intermediate.Length; i++)
        {

            Console.WriteLine(intermediate[i]);
        }
        Console.WriteLine("\n");
        Console.WriteLine("\n");

        Console.WriteLine("Hit Enter to go back to the main Menu");
        Console.ReadKey();
        mainMenu();
    }

    public static void getQuizScores()
    {
        string[] lines = File.ReadAllLines("../../../PupilQuizScores.txt");

        for (int i = 0; i < lines.Length; i++)
        {
            
            string[] splitStudentDetails = lines[i].Split("|");
            Console.WriteLine($"Pupil ID: {splitStudentDetails[0]}");
            Console.WriteLine($"Pupil Forename: {splitStudentDetails[1]}");
            Console.WriteLine($"Pupil Quiz Score: {splitStudentDetails[2]}");
            Console.WriteLine("\n");
            Console.WriteLine("\n");
        }
        
        Console.WriteLine("Hit Enter to go back to the main Menu");
        Console.ReadKey();
        mainMenu();
    }

    public static void getStudentDetailsByID(string id)
    {
        
        string[] quizScores = File.ReadAllLines("../../../PupilQuizScores.txt");
        string[] SkiTimes = File.ReadAllLines("../../../PupilSkiTimes.txt");

        for (int i = 0; i < SkiTimes.Length; i++)
        {
            
            string[] splitStudentDetails = SkiTimes[i].Split("|");
            if (splitStudentDetails[9] == id)
            {
                
                Console.WriteLine($"Forename: {splitStudentDetails[0]}");
                Console.WriteLine($"Lastname: {splitStudentDetails[1]}");
                Console.WriteLine($"Ski time 1: {splitStudentDetails[2]}");
                Console.WriteLine($"Ski time 2: {splitStudentDetails[3]}");
                Console.WriteLine($"Ski time 3: {splitStudentDetails[4]}");
                Console.WriteLine($"Ski time 4: {splitStudentDetails[5]}");
                Console.WriteLine($"Ski time 5: {splitStudentDetails[6]}");
                Console.WriteLine($"Average ski time: {splitStudentDetails[7]}");
                Console.WriteLine($"Ski group: {splitStudentDetails[8]}");
            }
        }

        for (int i = 0; i < quizScores.Length; i++)
        {
            
            string[] splitStudentDetails = quizScores[i].Split("|");
            if (splitStudentDetails[0] == id)
            {
                
                Console.WriteLine($"Quiz Score: {splitStudentDetails[2]}");
                Console.WriteLine($"User ID: {splitStudentDetails[0]}");
            }
        }
        
        Console.WriteLine("Hit Enter to go back to the main Menu");
        Console.ReadKey();
        mainMenu();
    }
}