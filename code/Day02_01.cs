using System;
using System.Collections.Generic;

/**

Your flight departs in a few days from the coastal airport; the easiest way down to the coast from here is via toboggan.

The shopkeeper at the North Pole Toboggan Rental Shop is having a bad day. "Something's wrong with our computers; we can't log in!" You ask if you can take a look.

Their password database seems to be a little corrupted: some of the passwords wouldn't have been allowed by the Official Toboggan Corporate Policy that was in effect when they were chosen.

To try to debug the problem, they have created a list(your puzzle input) of passwords(according to the corrupted database) and the corporate policy when that password was set.

For example, suppose you have the following list:

1-3 a: abcde
1-3 b: cdefg
2-9 c: ccccccccc

Each line gives the password policy and then the password. The password policy indicates the lowest and highest number of times a given letter must appear for the password to be valid. For example, 1-3 a means that the password must contain a at least 1 time and at most 3 times.

In the above example, 2 passwords are valid. The middle password, cdefg, is not; it contains no instances of b, but needs at least 1. The first and third passwords are valid: they contain one a or nine c, both within the limits of their respective policies.

How many passwords are valid according to their policies?

**/

public class Day02_01
{
    public void Main()
    {
        string[,] strings = ReadFile();
        Console.WriteLine(CheckPasswords(strings));
    }

    private string[,] ReadFile()
    {
        string[] fileStrings; // hold a tempory string array of file inputs

        try
        {
            // load file into tempory string array
            fileStrings = System.IO.File.ReadAllLines(@"C:\Users\Joshua\Desktop\Programming\Advent of Code\2020\inputs\Day02.txt");

            // define 2d string array based on password list enteries
            string[,] strings = new string[fileStrings.Length,4];

            // loop over all strings
            for (int i = 0; i < fileStrings.Length; i++)
            {
                // extract limits, character and password
                string[] splitString = fileStrings[i].Split(" ", 3);

                // extract limits
                string[] splitLimits = splitString[0].Split("-", 2);

                strings[i,0] = splitLimits[0];                  // lower limit
                strings[i,1] = splitLimits[1];                  // upper limit
                strings[i,2] = splitString[1][0].ToString();   // character
                strings[i,3] = splitString[2];                  // password
            }

            // return full 2d string array
            return strings;
        }
        catch
        {
            Console.WriteLine("Unable to load file!");
        }

        // return null array if we didn't open the file
        return null;
    }

    private int CheckPasswords(string[,] s)
    {
        // how many passwords are valid
        int passwordCount = 0;

        // loop over each password
        for (int i = 0; i < s.GetLength(0); i++)
        {
            // how many character matches per password
            int matchCount = 0;

            // loop over each password character
            for (int j = 0; j < s[i,3].Length; j++)
            {
                // if the password character matches the character
                if (s[i,3][j].ToString() == s[i,2])
                {
                    matchCount++;
                }
            }

            // if the match count falls between the lower and upper limit
            if (matchCount >= Int32.Parse(s[i,0]) && matchCount <= Int32.Parse(s[i,1]))
            {
                passwordCount++;
            }
        }

        return passwordCount;
    }
}