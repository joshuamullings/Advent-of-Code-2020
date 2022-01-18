using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/**

You land at the regional airport in time for your next flight. In fact, it looks like you'll even have time to grab some food: all flights are currently delayed due to issues in luggage processing.

Due to recent aviation regulations, many rules(your puzzle input) are being enforced about bags and their contents; bags must be color-coded and must contain specific quantities of other color-coded bags. Apparently, nobody responsible for these regulations considered how long they would take to enforce!

For example, consider the following rules:

light red bags contain 1 bright white bag, 2 muted yellow bags.
dark orange bags contain 3 bright white bags, 4 muted yellow bags.
bright white bags contain 1 shiny gold bag.
muted yellow bags contain 2 shiny gold bags, 9 faded blue bags.
shiny gold bags contain 1 dark olive bag, 2 vibrant plum bags.
dark olive bags contain 3 faded blue bags, 4 dotted black bags.
vibrant plum bags contain 5 faded blue bags, 6 dotted black bags.
faded blue bags contain no other bags.
dotted black bags contain no other bags.

These rules specify the required contents for 9 bag types. In this example, every faded blue bag is empty, every vibrant plum bag contains 11 bags(5 faded blue and 6 dotted black), and so on.

You have a shiny gold bag. If you wanted to carry it in at least one other bag, how many different bag colors would be valid for the outermost bag?(In other words: how many colors can, eventually, contain at least one shiny gold bag?)

In the above rules, the following options would be available to you:

A bright white bag, which can hold your shiny gold bag directly.
A muted yellow bag, which can hold your shiny gold bag directly, plus some other bags.
A dark orange bag, which can hold bright white and muted yellow bags, either of which could then hold your shiny gold bag.
A light red bag, which can hold bright white and muted yellow bags, either of which could then hold your shiny gold bag.

So, in this example, the number of bag colors that can eventually contain at least one shiny gold bag is 4.

How many bag colors can eventually contain at least one shiny gold bag?(The list of rules is quite long; make sure you get all of it.)

**/

public class Day07_01
{
    private class TreeNode
    {
        public string Data;
        public int Value;
        public int Reference;
        public TreeNode FirstChild;
        public TreeNode NextSibling;
        public TreeNode(string data, int value, TreeNode firstChild, TreeNode nextSibling, int reference)
        {
            this.Data = data;
            this.Value = value;
            this.FirstChild = firstChild;
            this.NextSibling = nextSibling;
            this.Reference = reference;
        }
    }

    private struct TreeRules
    {
        public string name;
        public int value;

        public TreeRules(string n, int v)
        {
            name = n;
            value = v;
        }
    }

    // blank global tree
    List<TreeNode> tree = new List<TreeNode>();

    public void Main()
    {
        string[] rules = ReadFile();
        ParseRules(rules);
    }
    
    private string[] ReadFile()
    {
        string[] fileStrings; // hold a tempory string array of file inputs

        try
        {
            // load file into tempory string array
            fileStrings = System.IO.File.ReadAllLines(@"C:\Users\Joshua\Desktop\Programming\Advent of Code\2020\inputs\test.txt");

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

    private void ParseRules(string[] rules)
    {
        // loop over strings
        for (int i = 0; i < rules.Length; i++)
        {
            // rule list
            List<TreeRules> ruleList = new List<TreeRules>();

            // rules not containing "no" are instructions
            if (!rules[i].Contains("no"))
            {
                // split on contain
                string[] splitContain = rules[i].Split("contain");

                // split commas list
                List<string> splitList = new List<string>();
                
                // add first split contain, which will always be the root
                splitList.Add(splitContain[0]);

                // if we have multiple instructions per root
                if (splitContain.Length > 1)
                {
                    // split on commas
                    string[] splitComma = splitContain[1].Split(",");

                    // loop over enteries and add to split list
                    for (int j = 0; j < splitComma.Length; j++)
                    {
                        splitList.Add(splitComma[j]);
                    }
                }
                // else there's only one instruction per root
                else
                {
                    splitList.Add(splitContain[1]);
                }

                // first in list will always be root, split on space
                string[] splitSpace = splitList[0].Split(" ");

                // add to rules list, root will always have value = 0
                ruleList.Add(
                    new TreeRules(
                        splitSpace[0] + " " + splitSpace[1],
                        0
                    )
                );

                // loop over remainder
                for (int j = 1; j < splitList.Count; j++)
                {
                    // split on spaces, take first three
                    splitSpace = splitList[j].Split(" ");

                    // add to rules list
                    ruleList.Add(
                    new TreeRules(
                        splitSpace[2] + " " + splitSpace[3],
                        Int32.Parse(splitSpace[1])
                        )
                    );
                }                
            }
            // else they are an end point
            else
            {
                // split on spaces
                string[] splitSpace = rules[0].Split(" ");

                // add to rules list, root will always have value = -1
                ruleList.Add(
                    new TreeRules(
                        splitSpace[0] + " " + splitSpace[1],
                        -1
                    )
                );
            }

            // add each rule to tree
            AddToTree(ruleList);
        }
    }

    private void AddToTree(List<TreeRules> ruleList)
    {
        // loop over rules
        for (int i = 0; i < ruleList.Count; i++)
        {
            // first pass is root and never has children
            if (i == 0)
            {
                // loop over existing tree
                for (int j = 0; j < tree.Count; j++)
                {
                    // check if root already exists within our tree
                    if (ruleList[i].name == tree[j].Data)
                    {
                        // found root
                        Console.WriteLine("Root found!");

                        // add to existing root
                        tree[j].FirstChild = new TreeNode(
                            ruleList[i].name,
                            ruleList[i].value,
                            null,
                            null,
                            j
                        );
                    }
                    // we need to create a new root
                    else
                    {
                        // couldn't find root
                        Console.WriteLine("Root not found!");

                        tree.Add(
                            new TreeNode(
                                ruleList[0].name,
                                ruleList[0].value,
                                null,
                                null,
                                j
                            )
                        );
                    }
                }
            }
            // further passes are children
            else
            {
                // loop over existing tree
                for (int j = 0; j < tree.Count; j++)
                {
                    // find root within our tree
                    if (ruleList[0].name == tree[j].Data)
                    {
                        // found root
                        Console.WriteLine("Root found!");

                        // check if child already exists
                        if (tree[j].FirstChild != null)
                        {
                            // add as next sibling
                            tree[tree[j].Reference].FirstChild = new TreeNode(
                                ruleList[i].name,
                                ruleList[i].value,
                                null,
                                null,
                                j
                            );

                        }
                        // it can be added as first child
                        else
                        {
                            // add to existing root
                            tree[j].FirstChild = new TreeNode(
                                ruleList[i].name,
                                ruleList[i].value,
                                null,
                                null,
                                j
                            );
                        }
                    }
                }
            }
        }
    }
}