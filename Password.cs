using System;
using static System.Console;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace PasswordGenerator
{
    public class Password
    {
        // To generate random numbers (See RandomGenerator.cs class)

        private static RandomGenerator randomGenerator = new RandomGenerator();

        // All generated passwords.

        private static List<string> GeneratedPasswords = new();

        // Letters, numbers and symbols

        private static string letters = "abcdefghijklmnopqrstuvwxyz";
        private static string numbers = "0123456789";
        private static string symbols = @"!""#$%&'()*+,-./:;<=>?@[\]^_{|}~";

        // To generante a Random Password
        public static void GenerateRandomPass(bool isCustom)
        {
            #region Variables

            string password = ""; // Password
            int passLength; // Length of password

            int lengthOfLetters = 0, lengthOfNumbers = 0, lengthOfSymbols = 0; // Length of each character that wil be in password

            bool lettersAllowed = true, numbersAllowed = true, symbolsAllowed = true, upperCaseAllowed = true; // For custom passwords

            #endregion

            #region Building the password, showing it and saving it.

            // How many characters while(true) try-catch statment block

            do
            {
                WriteLine("How many characters?");

                try
                {
                    passLength = int.Parse(ReadLine());

                    if (passLength >= 5000 || passLength <= 4)
                    {
                        throw new Exception();
                    }

                    break;
                }
                catch
                {
                    WriteLine();

                    Program.InvalidMessage();
                    WriteLine();
                }
            } while (true);

            WriteLine();

            // If the password is customized

            if (isCustom)
            {
                CustomPass("letters", ref lettersAllowed);
                CustomPass("numbers", ref numbersAllowed);
                CustomPass("symbols", ref symbolsAllowed);
                
                if (lettersAllowed) CustomPass("upper case", ref upperCaseAllowed);

                if (!lettersAllowed && !numbersAllowed && !symbolsAllowed)
                {
                    Program.InvalidMessage("Then, you won't receive a password!");
                    return;
                }
            }

            EXISTINGPASSWORD:

            // Fillign the pass with whitespaces (in length of passLength)

            while (password.Length != passLength) password += " ";

            // Building the password, while password have a whitespace

            while (password.Contains(" "))
            {
                if (lettersAllowed)
                {
                    lengthOfLetters = (int)randomGenerator.Next(0, passLength - 1) / 2; // Some random number / 2
                    InsertCharacter(lengthOfLetters, letters); // Inserting the characters
                }

                if (numbersAllowed)
                {
                    lengthOfNumbers = (int)randomGenerator.Next(0, passLength - 1) / 2;
                    InsertCharacter(lengthOfNumbers, numbers);
                }

                if (symbolsAllowed)
                {
                    lengthOfSymbols = (int)randomGenerator.Next(0, passLength - 1) / 2;
                    InsertCharacter(lengthOfSymbols, symbols);
                }
            }

            // If there's one password already saved that is the same (99,999999% impossible)

            if (ExistsPassword(password))
            {
                goto EXISTINGPASSWORD; // Generate again.
            }

            // Showing pass

            WriteLine("You password is: \n\n" + password);

            WriteLine();
            Program.PressAnyKeyToContinue();

            // Saving password

            AddPassword(password);

            #endregion

            #region Methods

            // Setting the configs for password

            void CustomPass(string whichChar, ref bool charAllowed)
            {
                WriteLine($"Are {whichChar} allowed? [Y / Anything else]");
                if (ReadLine().ToUpper() != "Y") { charAllowed = false; }

                WriteLine();
            }

            // Put each char in the password (replacing the white space for the char)

            void InsertCharacter(int howManyChars, string whichChar)
            {
                // Variables

                int randomPosition; // To set the index of the character in password.

                char[] passwordCharacters = new char[passLength]; 
                List<int> avaibleIndex = new();

                // Building 

                for (int c = 0; c < passLength; c++)
                {
                    passwordCharacters[c] = password[c]; // p, a, s, s, w, o, r, d.
                    if (password[c].ToString() == " ") avaibleIndex.Add(c); // 0, 1, 2, 3, 4, ..., passLength 
                }

                // Changing passwordCharacters array and avaibleIndex list

                for (int i = 0; i < howManyChars;)
                {
                    if (avaibleIndex.Count == 0)
                    {
                        break;
                    }
                    randomPosition = avaibleIndex[randomGenerator.Next(0, avaibleIndex.Count)];

                    if (passwordCharacters[randomPosition].ToString() == " ")
                    {
                        passwordCharacters[randomPosition] = whichChar[randomGenerator.Next(0, whichChar.Length)];

                        avaibleIndex.Remove(randomPosition);
                        i++;
                    }
                }

                // Building password.

                password = "";

                int[] intArray = new int[10];

                for (int i = 0; i < 10; i++)
                {
                    intArray[i] = randomGenerator.Next(0, 30);
                }

                foreach (char c in passwordCharacters)
                {
                    if (upperCaseAllowed && intArray.Contains<int>(randomGenerator.Next(0, 30)))
                    {
                        password += c.ToString().ToUpper();
                    }
                    else
                    {
                        password += c.ToString();
                    }
                }
            }
            #endregion
        }

        // To add a password in "GeneratedPasswords" list
        private static void AddPassword(string password)
        {
            GeneratedPasswords.Add(password);
        }

        // Verifying the existence of any or some password
        private static bool ExistsPassword(string password)
        {
            // Linq search
            var pass =
                from p in GeneratedPasswords
                where (p == password)
                select p;

            // If the password do not exists, an exception will throw (because pass linq
            // search do not have results)

            try
            {
                if (pass.First() == null) return false;
                else { return true; }
            }
            catch
            {
                return false;
            }
        }

        // Seeing all passwords
        public static void SeeAllPass()
        {
            // Verifying the existence of any password
            try
            {
                if (!ExistsPassword(GeneratedPasswords[0]))
                {
                    Program.InvalidMessage("Password not finded.");
                    return;
                }
            }
            catch
            {
                Program.InvalidMessage("Password not finded.");
                return;
            }

            // Showing each password with an index = index in array + 1

            for (int i = 0; i < GeneratedPasswords.Count; i++)
            {
                WriteLine($"{i + 1} - {GeneratedPasswords[i]}");
            }

            // Menu

            WriteLine();

            WriteLine("0 - Go back");
            WriteLine("1 - Save password as Txt file");
            WriteLine("2 - Save all passwords as Txt file");
            WriteLine("3 - Delete password");

            /*
             * 0 - Go back
             * 1 - Save as txt file
             * 2 - Save all as txt file
             * 3 - Delete some password
             */

            // Switch user choice

            switch (ReadLine())
            {
                case "0":
                    return;
                case "1":
                    Write("\nThe index is... ");
                    
                    try
                    {
                        SavePassAsTxt(int.Parse(ReadLine()));
                    }
                    catch
                    {
                        Program.InvalidMessage();
                        return;
                    }
                    break;
                case "2":
                    SavePassAsTxt(1, true);
                    break;
                case "3":
                    Write("\nThe index is... ");

                    try
                    {
                        DeletePassword(int.Parse(ReadLine()));
                    }
                    catch
                    {
                        Program.InvalidMessage();
                        return;
                    }
                    break;
                default:
                    WriteLine();
                    Program.InvalidMessage();
                    break;
            }
        }

        // Saving some pass as txt
        private static void SavePassAsTxt(int index, bool isAll = false)
        {
            // Variables

            string passwordName = $"{DateTime.Today.Day}{DateTime.Today.Month}password{index}";
            string path = @$"{Environment.GetFolderPath(Environment.SpecialFolder.Desktop)}\{passwordName}.txt";

            int i = 0;

            // Verifying the existence of password
            
            try
            {
                if (!ExistsPassword(GeneratedPasswords[index - 1]))
                {
                    Program.InvalidMessage("\nPassword not finded.");
                    return;
                }
            }
            catch
            {
                Program.InvalidMessage("\nPassword not finded.");
                return;
            }

            // If the file already exists, the name of file will change
            
            ChangeName(ref i);

            // Saving

            if (isAll)
            {
                File.Create(path).Dispose();
                File.AppendAllLines(path, GeneratedPasswords);
            }
            else
            {
                File.Create(path).Dispose();
                File.WriteAllText(path, GeneratedPasswords[index - 1]);
            }
            
            // Change the name of file if file already exists

            void ChangeName(ref int i)
            {
                if (File.Exists(path))
                {
                    path = path.Replace(".txt", $"({i})") + ".txt";
                    i++;

                    ChangeName(ref i);
                }
            }
        }

        // Delete some pass
        private static void DeletePassword(int index)
        {
            // Verifying the existence of password

            try
            {
                if (!ExistsPassword(GeneratedPasswords[index - 1]))
                {
                    Program.InvalidMessage("\nPassword not finded.");
                    return;
                }
            }
            catch
            {
                Program.InvalidMessage("\nPassword not finded.");
                return;
            }

            // Removing the password from "GeneratedPasswords" list

            GeneratedPasswords.RemoveAt(index - 1);
        }
    }
}
