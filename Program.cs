using System;
using static System.Console;

namespace PasswordGenerator
{
    class Program
    {
        static void Main(string[] args)
        {
            //MENU 

            string userChoice;

            do
            {
                Clear();

                userChoice = MenuChoice();
                Clear();

                switch (userChoice)
                {
                    case "0":
                        WriteLine("Until the next!");
                        PressAnyKeyToContinue();
                        break;
                    case "1":
                        Password.GenerateRandomPass(false);
                        break;
                    case "2":
                        Password.GenerateRandomPass(true);
                        break;
                    case "3":
                        Password.SeeAllPass();
                        break;
                    default:
                        InvalidMessage();
                        break;
                }
            } while (userChoice != "0");
        }

        // Menu and return user choice
        private static string MenuChoice()
        {
            WriteLine(
                     "-=-=-=-=-=-=-=-=-= MENU =-=-=-=-=-=-=-=-=-\n\n" +
                     "==> 0 - Exit\n" +
                     "==> 1 - Generate random pass\n" +
                     "==> 2 - Generate custom random pass\n" +
                     "==> 3 - See all passwords (while running)\n"
                     );

            Write("--> My choice is... ");
            return ReadLine();
        }

        // When some error, an InvalidMessage
        public static void InvalidMessage(string msg = "Some error has occurred. Please, try again.")
        {
            ForegroundColor = ConsoleColor.Red;

            WriteLine(msg);
            PressAnyKeyToContinue();

            ForegroundColor = ConsoleColor.Gray;
        }

        // Press any key to continue... (ReadKey)
        public static void PressAnyKeyToContinue()
        {
            WriteLine("Press any key to continue...");
            ReadKey();
        }
    }
}