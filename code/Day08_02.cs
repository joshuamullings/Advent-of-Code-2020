using System;
using System.Collections.Generic;
using System.Globalization;

/**

After some careful analysis, you believe that exactly one instruction is corrupted.

Somewhere in the program, either a jmp is supposed to be a nop, or a nop is supposed to be a jmp.(No acc instructions were harmed in the corruption of this boot code.)

The program is supposed to terminate by attempting to execute an instruction immediately after the last instruction in the file. By changing exactly one jmp or nop, you can repair the boot code and make it terminate correctly.

For example, consider the same program from above:

nop +0
acc +1
jmp +4
acc +3
jmp -3
acc -99
acc +1
jmp -4
acc +6

If you change the first instruction from nop +0 to jmp +0, it would create a single-instruction infinite loop, never leaving that instruction. If you change almost any of the jmp instructions, the program will still eventually find another jmp instruction and loop forever.

However, if you change the second-to-last instruction(from jmp -4 to nop -4), the program terminates! The instructions are visited in this order:

nop +0  | 1
acc +1  | 2
jmp +4  | 3
acc +3  |
jmp -3  |
acc -99 |
acc +1  | 4
nop -4  | 5
acc +6  | 6

After the last instruction(acc +6), the program terminates by attempting to run the instruction below the last instruction in the file. With this change, after the program terminates, the accumulator contains the value 8(acc +1, acc +1, acc +6).

Fix the program so that it terminates normally by changing exactly one jmp(to nop) or nop(to jmp). What is the value of the accumulator after the program terminates?

**/

public class Day08_02
{
    private class InstructionClass
    {
        public string instruction;
        public int value;

        public InstructionClass(string s, int v)
        {
            this.instruction = s;
            this.value = v;
        }
    }

    public void Main()
    {
        string[] instructions = ReadFile();
        List<InstructionClass> instructionList = ParseInstructions(instructions);
        Console.WriteLine(ModifyInstructions(instructionList));
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
        catch (Exception e)
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

    private int ModifyInstructions(List<InstructionClass> instructionList)
    {
        // hold acc
        int acc = 0;

        // jmp to nop
        // loop over instruction list, converting each jmp to nop and running the instructions
        for (int i = 0; i < instructionList.Count; i++)
        {
            // check if our instruction is either a jmp or nop, change to the inverse and run instructions
            if (instructionList[i].instruction == "jmp")
            {
                instructionList[i].instruction = "nop";
                acc = RunInstructions(instructionList);
                instructionList[i].instruction = "jmp";
            }
            else if (instructionList[i].instruction == "nop")
            {
                instructionList[i].instruction = "jmp";
                acc = RunInstructions(instructionList);
                instructionList[i].instruction = "nop";
            }

            // return acc if we've found a solution
            if (acc > 0)
            {
                return acc;
            }
        }

        // no solution found
        return 0;
    }

    private int RunInstructions(List<InstructionClass> instructionList)
    {
        // acc value
        int acc = 0;

        // instruction limit before break
        int instructionCount = 0;

        // loop over instructions list
        for (int i = 0; i < instructionList.Count; i++)
        {
            // if we've hit the instruction limit => infinte loop
            if (instructionCount > instructionList.Count)
            {
                return 0;
            }

            // count an instruction
            instructionCount++;

            // switch on instruction
            switch (instructionList[i].instruction)
            {
                // ignore instruction, go to next
                case("nop"):
                    break;
                
                // add value to acc, go to next
                case("acc"):
                    acc += instructionList[i].value;
                    break;

                // jump up or down value amount of instructions, -1 to account for the incomming i++
                case("jmp"):
                    i += instructionList[i].value - 1;
                    break;

                default:
                    break;
            }
        }

        return acc;
    }
}