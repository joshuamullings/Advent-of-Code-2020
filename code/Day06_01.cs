using System;
using System.Collections.Generic;

/**

As your flight approaches the regional airport where you'll switch to a much larger plane, customs declaration forms are distributed to the passengers.

The form asks a series of 26 yes-or-no questions marked a through z. All you need to do is identify the questions for which anyone in your group answers "yes". Since your group is just you, this doesn't take very long.

However, the person sitting next to you seems to be experiencing a language barrier and asks if you can help. For each of the people in their group, you write down the questions for which they answer "yes", one per line. For example:

abcx
abcy
abcz

In this group, there are 6 questions to which anyone answered "yes": a, b, c, x, y, and z.(Duplicate answers to the same question don't count extra; each question counts at most once.)

Another group asks for your help, then another, and eventually you've collected answers from every group on the plane(your puzzle input). Each group's answers are separated by a blank line, and within each group, each person's answers are on a single line. For example:

abc

a
b
c

ab
ac

a
a
a
a

b

This list represents answers from five groups:

The first group contains one person who answered "yes" to 3 questions: a, b, and c.
The second group contains three people; combined, they answered "yes" to 3 questions: a, b, and c.
The third group contains two people; combined, they answered "yes" to 3 questions: a, b, and c.
The fourth group contains four people; combined, they answered "yes" to only 1 question, a.
The last group contains one person who answered "yes" to only 1 question, b.
In this example, the sum of these counts is 3 + 3 + 3 + 1 + 1 = 11.

For each group, count the number of questions to which anyone answered "yes". What is the sum of those counts?

**/

public class Day06_01
{
    public void Main()
    {
        string[] questions = ReadFile();
        Console.WriteLine(ProcessQuestions(questions));
    }

    private string[] ReadFile()
    {
        string[] fileStrings; // hold a tempory string array of file inputs

        try
        {
            // load file into tempory string array
            fileStrings = System.IO.File.ReadAllLines(@"C:\Users\Joshua\Desktop\Programming\Advent of Code\2020\inputs\Day06.txt");

            // return full string array
            return fileStrings;
        }
        catch(Exception e)
        {
            Console.WriteLine("Unable to load file!");
            Console.WriteLine(e);
        }

        // return null array if we didn't open the file
        return null;
    }

    private int ProcessQuestions(string[] questions)
    {
        // total responses
        int totalResponses = 0;

        // hold group letter responses
        bool[] letterArray = new bool[26];

        // loop over all question responses
        for (int i = 0; i < questions.Length; i++)
        {
            // if we've reached the end of the group responses
            if (questions[i].ToString() == "")
            {
                // check how many letters the group responded with
                for (int j = 0; j < letterArray.Length; j++)
                {
                    if (letterArray[j])
                    {
                        totalResponses++;
                    }
                }

                // reset letter array
                for (int j = 0; j < letterArray.Length; j++) 
                {
                    letterArray[j] = false;
                }

                continue;
            }

            // loop over group response
            for (int j = 0; j < questions[i].Length; j++)
            {
                // get ascii value
                int asciiCode =(int)questions[i][j];

                // flag letter present in letter array
                // -97 from the index because ascii 'a' has a value of 97, meaning we end up at index 0 for a, 1 for b etc..
                letterArray[asciiCode - 97] = true;
            }
        }

        // check one last time how many letters the group responded with
        for (int j = 0; j < letterArray.Length; j++)
        {
            if (letterArray[j])
            {
                totalResponses++;
            }
        }

        return totalResponses;
    }
}