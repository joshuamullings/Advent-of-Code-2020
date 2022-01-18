using System;
using System.Collections.Generic;
using System.Globalization;

/**

Your flight to the major airline hub reaches cruising altitude without incident. While you consider checking the in-flight menu for one of those drinks that come with a little umbrella, you are interrupted by the kid sitting next to you.

Their handheld game console won't turn on! They ask if you can take a look.

You narrow the problem down to a strange infinite loop in the boot code(your puzzle input) of the device. You should be able to fix it, but first you need to be able to run the code in isolation.

The boot code is represented as a text file with one instruction per line of text. Each instruction consists of an operation(acc, jmp, or nop) and an argument(a signed number like +4 or -20).

acc increases or decreases a single global value called the accumulator by the value given in the argument. For example, acc +7 would increase the accumulator by 7. The accumulator starts at 0. After an acc instruction, the instruction immediately below it is executed next.
jmp jumps to a new instruction relative to itself. The next instruction to execute is found using the argument as an offset from the jmp instruction; for example, jmp +2 would skip the next instruction, jmp +1 would continue to the instruction immediately below it, and jmp -20 would cause the instruction 20 lines above to be executed next.
nop stands for No OPeration - it does nothing. The instruction immediately below it is executed next.

For example, consider the following program:

nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6

These instructions are visited in this order:

nop +0  | 1
acc +1  | 2, 8(!)
jmp +4  | 3
acc +3  | 6
jmp -3  | 7
acc -99 |
acc +1  | 4
jmp -4  | 5
acc +6  |

First, the nop +0 does nothing. Then, the accumulator is increased from 0 to 1(acc +1) and jmp +4 sets the next instruction to the other acc +1 near the bottom. After it increases the accumulator from 1 to 2, jmp -4 executes, setting the next instruction to the only acc +3. It sets the accumulator to 5, and jmp -3 causes the program to continue back at the first acc +1.

This is an infinite loop: with this sequence of jumps, the program will run forever. The moment the program tries to run any instruction a second time, you know it will never terminate.

Immediately before the program would run an instruction a second time, the value in the accumulator is 5.

Run your copy of the boot code. Immediately before any instruction is executed a second time, what value is in the accumulator?

**/

public class Day08_01
{
    private class InstructionClass
    {
        public string instruction;
        public int value;
        public bool executed;

        public InstructionClass(string s, int v)
        {
            this.instruction = s;
            this.value = v;
            this.executed = false;
        }
    }

    public void Main()
    {
        string[] instructions = ReadFile();
        List<InstructionClass> instructionList = ParseInstructions(instructions);
        Console.WriteLine(RunInstructions(instructionList));
    }
    
    private string[] ReadFile()
    {
        string[] fileStrings; // hold a tempory string array of file inputs

        try
        {
            // load file into tempory string array
            fileStrings = System.IO.File.ReadAllLines(@"C:\Users\Joshua\Desktop\Programming\Advent of Code\2020\inputs\Day08.txt");

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

    private List<InstructionClass> ParseInstructions(string[] instructionStrings)
    {
        // hold all instructions and values
        List<InstructionClass> instructionList = new List<InstructionClass>();

        // loop over each instruction
        for (int i = 0; i < instructionStrings.Length; i++)
        {
            // split string on spaces
            string[] splitString = instructionStrings[i].Split(" ");

            // add non null strings from split string to a list
            List<string> splitStringList = new List<string>();

            for (int j = 0; j < splitString.Length; j++)
            {
                if (splitString[j] != "")
                {
                    splitStringList.Add(splitString[j]);
                }
            }

            // add each instruction value pair to the instruction list
            // this requires makes a new instruction strut pair for each
            instructionList.Add(
                new InstructionClass(
                    splitStringList[0],
                    Int32.Parse(splitStringList[1], NumberStyles.AllowLeadingSign)
                )
            );
        }

        return instructionList;
    }

    private int RunInstructions(List<InstructionClass> instructionList)
    {
        // acc value
        int acc = 0;

        // loop over instructions list
        for (int i = 0; i < instructionList.Count; i++)
        {
            // if we've executed the instruction previously => infinte loop
            if (instructionList[i].executed == true)
            {
                break;
            }

            // switch on instruction
            switch (instructionList[i].instruction)
            {
                // ignore instruction, go to next
                case("nop"):
                    instructionList[i].executed = true;
                    break;
                
                // add value to acc, go to next
                case("acc"):
                    instructionList[i].executed = true;
                    acc += instructionList[i].value;
                    break;

                // jump up or down value amount of instructions, -1 to account for the incomming i++
                case("jmp"):
                    instructionList[i].executed = true;
                    i += instructionList[i].value - 1;
                    break;

                default:
                    break;
            }
        }

        return acc;
    }
}