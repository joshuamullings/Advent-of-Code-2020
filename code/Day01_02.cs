using System;
using System.Collections.Generic;

/**

The Elves in accounting are thankful for your help; one of them even offers you a starfish coin they had left over from a past vacation. They offer you a second one if you can find three numbers in your expense report that meet the same criteria.

Using the above example again, the three entries that sum to 2020 are 979, 366, and 675. Multiplying them together produces the answer, 241861950.

In your expense report, what is the product of the three entries that sum to 2020?

**/

public class Day01_02
{
    List<int> inputs = new List<int>(); // hold int list of file inputs

    public void Main()
    {
        ReadFile();
        FindSum();

        Console.WriteLine(FindSum());
    }

    private void ReadFile()
    {
        string[] tempStrings; // hold a tempory string array of file inputs

        try
        {
            // load file into tempory string array
            tempStrings = System.IO.File.ReadAllLines(@"C:\Users\Joshua\Desktop\Programming\Advent of Code\2020\inputs\Day01.txt");

            // loop over all strings
            for (int i = 0; i < tempStrings.Length; i++)
            {
                // parse strings to input array
                inputs.Add(Int32.Parse(tempStrings[i]));
            }
        }
        catch
        {
            Console.WriteLine("Unable to load file!");
        }
    }

    private int FindSum()
    {
        foreach (int i in inputs)
        {
            foreach (int j in inputs)
            {
                foreach (int k in inputs)
                {
                    if (i + j + k == 2020)
                    {
                        return i * j * k;
                    }
                } 
            }
        }

        Console.WriteLine("No pair found!");

        return 0 ;
    }
}