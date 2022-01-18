using System;
using System.Collections.Generic;

/**

Ding! The "fasten seat belt" signs have turned on. Time to find your seat.

It's a completely full flight, so your seat should be the only missing boarding pass in your list. However, there's a catch: some of the seats at the very front and back of the plane don't exist on this aircraft, so they'll be missing from your list as well.

Your seat wasn't at the very front or back, though; the seats with IDs +1 and -1 from yours will be in your list.

What is the ID of your seat?

**/

public class Day05_02
{
    public void Main()
    {
        string[] boardingPasses = ReadFile();
        Console.WriteLine(CheckBoardingPass(boardingPasses));
    }

    private string[] ReadFile()
    {
        string[] fileStrings; // hold a tempory string array of file inputs

        try
        {
            // load file into tempory string array
            fileStrings = System.IO.File.ReadAllLines(@"C:\Users\Joshua\Desktop\Programming\Advent of Code\2020\inputs\Day05.txt");

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

    private int CheckBoardingPass(string[] boardingPasses)
    {
        // 2d seats array, esentially a seating plan
        bool[,] seats = new bool[128, 8];

        // loop over boarding passes
        for (int i = 0; i < boardingPasses.Length; i++)
        {
            // track the seating position and an xy index, upper and lower because we zero in on the seat
            int frontIndex = 0, backIndex = 127, leftIndex = 0, rightIndex = 7;

            // loop over instructions
            for (int j = 0; j < boardingPasses[i].Length; j++)
            {
                // if the instructions are for front or back allocation
                if (boardingPasses[i][j] == 'F' || boardingPasses[i][j] == 'B')
                {
                    // calculate the shift in index
                    int difference = backIndex - frontIndex;
                    double shift = difference / 2.0;

                    // update index
                    if (boardingPasses[i][j] == 'F')
                    {
                        backIndex = backIndex -(int)Math.Ceiling(shift);
                    }
                    else
                    {
                        frontIndex = frontIndex +(int)Math.Ceiling(shift);
                    }
                }
                // else the instructions are for left or right allocation
                else
                {
                    // claculate the shift in index
                    int difference = rightIndex - leftIndex;
                    double shift = difference / 2.0;

                    // update index
                    if (boardingPasses[i][j] == 'L')
                    {
                        rightIndex = rightIndex -(int)Math.Ceiling(shift);
                    }
                    else
                    {
                        leftIndex = leftIndex +(int)Math.Ceiling(shift);
                    }
                }
            }

            // set seat array to filled
            seats[frontIndex, leftIndex] = true;
        }

        // hold out row and column position
        int row = 0, column = 0, columnEmpty = 0;

        // loop over rows
        for (; row < 128; row++)
        {
            // how many empty seats are in the row
            int emptyCount = 0;

            // reset column and column empty
            column = columnEmpty = 0;

            // loop over columns
            for (; column < 8; column++)
            {
                // count empty seats, logging the position of the last empty one
                if (seats[row,column] == false)
                {
                    columnEmpty = column;
                    emptyCount++;
                }
            }

            // if there's exactly 1 seat free it's ours
            if (emptyCount == 1)
            {
                return(row * 8) + columnEmpty;
            }
        }

        return 0;
    }
}