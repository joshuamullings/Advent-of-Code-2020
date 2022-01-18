using System;
using System.Collections.Generic;

/**

Time to check the rest of the slopes - you need to minimize the probability of a sudden arboreal stop, after all.

Determine the number of trees you would encounter if, for each of the following slopes, you start at the top-left corner and traverse the map all the way to the bottom:

Right 1, down 1.
Right 3, down 1.(This is the slope you already checked.)
Right 5, down 1.
Right 7, down 1.
Right 1, down 2.

In the above example, these slopes would find 2, 7, 3, 4, and 2 tree(s) respectively; multiplied together, these produce the answer 336.

What do you get if you multiply together the number of trees encountered on each of the listed slopes?

**/

public class Day03_02
{
    public void Main()
    {
        string[] slope = ReadFile();
        Console.WriteLine(CalculateRoute(slope));
    }

    private string[] ReadFile()
    {
        string[] fileStrings; // hold a tempory string array of file inputs

        try
        {
            // load file into tempory string array
            fileStrings = System.IO.File.ReadAllLines(@"C:\Users\Joshua\Desktop\Programming\Advent of Code\2020\inputs\Day03.txt");

            // return full string array
            return fileStrings;
        }
        catch
        {
            Console.WriteLine("Unable to load file!");
        }

        // return null array if we didn't open the file
        return null;
    }

    private Int64 CalculateRoute(string[] slope)
    {
        // hold total multiple, Int64 because of length of the integer
        Int64 treeMultiple = CheckSlope(slope, 1, 1);

        // multiply by each set of slope parameters
        treeMultiple *= CheckSlope(slope, 3, 1) *
                            CheckSlope(slope, 5, 1) *
                                CheckSlope(slope, 7, 1) *
                                    CheckSlope(slope, 1, 2);
    
        return treeMultiple;
    }

    private int CheckSlope(string[] slope, int right, int down)
    {
        // tree hit count
        int hitCount = 0;

        // loop over rows, exiting once we reach the bottom
        for (int rowPosition = 0, xPosition = 0;
                rowPosition < slope.GetLength(0);
                    rowPosition = rowPosition + down)
        {
            // check if we've hit a tree
            if (slope[rowPosition][xPosition].ToString() == "#")
            {
                hitCount++;
            }

            // move to the right
            xPosition = xPosition + right;

            // check if we've gone off the right of the slope
            if (xPosition >= slope[rowPosition].Length)
            {
                // loop back around to the left side
                xPosition = xPosition - slope[rowPosition].Length;
            }
        }

        return hitCount;
    }
}