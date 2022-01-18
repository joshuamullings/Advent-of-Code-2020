using System;
using System.Collections.Generic;

/**

While it appears you validated the passwords correctly, they don't seem to be what the Official Toboggan Corporate Authentication System is expecting.

The shopkeeper suddenly realizes that he just accidentally explained the password policy rules from his old job at the sled rental place down the street! The Official Toboggan Corporate Policy actually works a little differently.

Each policy actually describes two positions in the password, where 1 means the first character, 2 means the second character, and so on.(Be careful; Toboggan Corporate Policies have no concept of "index zero"!) Exactly one of these positions must contain the given letter. Other occurrences of the letter are irrelevant for the purposes of policy enforcement.

Given the same example list from above:

1-3 a: abcde is valid: position 1 contains a and position 3 does not.
1-3 b: cdefg is invalid: neither position 1 nor position 3 contains b.
2-9 c: ccccccccc is invalid: both position 2 and position 9 contain c.

How many passwords are valid according to the new interpretation of the policies?

**/

public class Day02_02
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

            // integers of the lower and upper bounds
            int lower = Int32.Parse(s[i,0]) - 1;
            int upper = Int32.Parse(s[i,1]) - 1;

            // if the lower bound string chracter matches
            if (s[i,3][lower].ToString() == s[i,2])
            {
                matchCount++;
            }

            // if the upper bound string chracter matches
            if (s[i,3][upper].ToString() == s[i,2])
            {
                matchCount++;
            }

            // if we have exactly one match
            if (matchCount == 1)
            {
                passwordCount++;
            }
        }

        return passwordCount;
    }
}