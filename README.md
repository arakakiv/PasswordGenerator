# PasswordGenerator

**The password must follow these rules:**

- Lenght between 5 and 4999 ( 5 < length < 500);
- No whitespaces.

**Letters:** abcdefghijklmnopqrstuvwxyz

**Numbers:** 0123456789

**Symbols:**

## RandomGenerator.cs

It's the class that generates **_secure_** random integers. It is used in many parts of the code and is what makes the essence of the project.

## Password.cs

**Created for:**
- Store all passwords while the program is running;
- Have all the methods for:
  - generate;
  - add;
  - see;
  - save;
  - delete a password.

## Program.cs

- The program;
- Have methods to prevent erros.

## How does the program generate?

The essence of the algorithm is very simple:

- Is custom? (input by the user);
- Length of the password (input by the user);
  - If the password will be custom, ask for the following options: **Letters, Numbers, Symbols, Upper Case**
  - Else, do not do anything;
- Fills the password with whitespaces, until the password stay with previous the stated length;
  - Generate random lengths of characters / 2 **(Letters, numbers, symbols)** based in generated random integers, and replace some random whitespace (based too in generated         random integers) with a character, until the password be only characters. If upper case allowed (true/false for custom passwords and true for random), randomly some letter       character will be upper case (based in an array of random numbers in range 0 - 10 and some random integer in range 0 - 30. If the random integer is in the range, then the       letter will be upper case. Else, not.
**_Avoiding possible exceptions_**

## Adding, Seeing, Saving and Deleting

**Just methods that use the following namespaces:**
- System;
- System.IO;
- System.Linq;

And that's it! Thank you :)
