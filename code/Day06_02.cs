using System;
using System.Collections.Generic;

/**

As you finish the last group's customs declaration, you notice that you misread one word in the instructions:

You don't need to identify the questions to which anyone answered "yes"; you need to identify the questions to which everyone answered "yes"!

Using the same example as above:

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

In the first group, everyone(all 1 person) answered "yes" to 3 questions: a, b, and c.
In the second group, there is no question to which everyone answered "yes".
In the third group, everyone answered yes to only 1 question, a. Since some people did not answer "yes" to b or c, they don't count.
In the fourth group, everyone answered yes to only 1 question, a.
In the fifth group, everyone(all 1 person) answered "yes" to 1 question, b.
In this example, the sum of these counts is 3 + 0 + 1 + 1 + 1 = 6.

For each group, count the number of questions to which everyone answered "yes". What is the sum of those counts?

**/

public class Day06_02
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
        // total responses, number of group members
        int totalResponses = 0, groupMembers = 0;

        // hold group letter responses
        int[] letterArray = new int[26];

        // loop over all question responses
        for (int i = 0; i < questions.Length; i++)
        {
            // if we've reached the end of the group responses
            if (questions[i].ToString() == "")
            {
                // check how many letters the group responded with
                for (int j = 0; j < letterArray.Length; j++)
                {
                    // everyone responed the same if the count of letter responses equals the group member count
                    if (letterArray[j] == groupMembers)
                    {
                        totalResponses++;
                    }
                }

                // reset group members
                groupMembers = 0;

                // reset letter array
                for (int j = 0; j < letterArray.Length; j++)
                {
                    letterArray[j] = 0;
                }

                continue;
            }

            // count the group members
            groupMembers++;

            // loop over group response
            for (int j = 0; j < questions[i].Length; j++)
            {
                // get ascii value
                int asciiCode =(int)questions[i][j];

                // flag letter present in letter array
                // -97 from the index because ascii 'a' has a value of 97, meaning we end up at index 0 for a, 1 for b etc..
                letterArray[asciiCode - 97] += 1;;
            }
        }

        // check one last time how many letters the group responded with
        for (int j = 0; j < letterArray.Length; j++)
        {
            // everyone responed the same if the count of letter responses equals the group member count
            if (letterArray[j] == groupMembers)
            {
                totalResponses++;
            }
        }

        return totalResponses;
    }
}