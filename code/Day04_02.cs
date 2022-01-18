using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

/**

The line is moving more quickly now, but you overhear airport security talking about how passports with invalid data are getting through. Better add some data validation, quick!

You can continue to ignore the cid field, but each other field has strict rules about what values are valid for automatic validation:

byr(Birth Year) - four digits; at least 1920 and at most 2002.
iyr(Issue Year) - four digits; at least 2010 and at most 2020.
eyr(Expiration Year) - four digits; at least 2020 and at most 2030.
hgt(Height) - a number followed by either cm or in:
If cm, the number must be at least 150 and at most 193.
If in, the number must be at least 59 and at most 76.
hcl(Hair Color) - a # followed by exactly six characters 0-9 or a-f.
ecl(Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
pid(Passport ID) - a nine-digit number, including leading zeroes.
cid(Country ID) - ignored, missing or not.

Your job is to count the passports where all required fields are both present and valid according to the above rules. Here are some example values:

byr valid:   2002
byr invalid: 2003

hgt valid:   60in
hgt valid:   190cm
hgt invalid: 190in
hgt invalid: 190

hcl valid:   #123abc
hcl invalid: #123abz
hcl invalid: 123abc

ecl valid:   brn
ecl invalid: wat

pid valid:   000000001
pid invalid: 0123456789

Here are some invalid passports:

eyr:1972 cid:100
hcl:#18171d ecl:amb hgt:170 pid:186cm iyr:2018 byr:1926

iyr:2019
hcl:#602927 eyr:1967 hgt:170cm
ecl:grn pid:012533040 byr:1946

hcl:dab227 iyr:2012
ecl:brn hgt:182cm pid:021572410 eyr:2020 byr:1992 cid:277

hgt:59cm ecl:zzz
eyr:2038 hcl:74454a iyr:2023
pid:3556412378 byr:2007

Here are some valid passports:

pid:087499704 hgt:74in ecl:grn iyr:2012 eyr:2030 byr:1980
hcl:#623a2f

eyr:2029 ecl:blu cid:129 byr:1989
iyr:2014 pid:896056539 hcl:#a97842 hgt:165cm

hcl:#888785
hgt:164cm byr:2001 iyr:2015 cid:88
pid:545766238 ecl:hzl
eyr:2022

iyr:2010 hgt:158cm hcl:#b6652a ecl:blu byr:1944 eyr:2021 pid:093154719

Count the number of valid passports - those that have all required fields and valid values. Continue to treat cid as optional. In your batch file, how many passports are valid?

**/

public class Day04_02
{
    public void Main()
    {
        string[,] passports = ReadFile();
        Console.WriteLine(CheckPassports(passports));
    }

    private string[,] ReadFile()
    {
        string[] fileStrings; // hold a tempory string array of file inputs

        try
        {
            // load file into tempory string array
            fileStrings = System.IO.File.ReadAllLines(@"C:\Users\Joshua\Desktop\Programming\Advent of Code\2020\inputs\Day04.txt");

            // passport count
            int passportCount = 0;

            // count how many passports we have
            for (int i = 0; i < fileStrings.Length; i++)
            {
                if (fileStrings[i] == "")
                {
                    passportCount++;
                }
            }

            // hold strings in a 2d array, each column holding 1 field
            string[,] passportStrings = new string[passportCount + 1,8];

            // reset passport count
            passportCount = 0;

            // parse strings
            for (int i = 0; i < fileStrings.Length; i++)
            {
                // jump to next passport entry if we have a blank line
                if (fileStrings[i] == "")
                {
                    passportCount++;
                    continue;
                }
                    
                // split string into elements
                string[] tempStrings = fileStrings[i].Split(" ");

                // loop over elements
                for (int j = 0; j < tempStrings.Length; j++)
                {
                    // split string into components, always seperated by a :
                    string[] switchString = tempStrings[j].Split(":");

                    // switch on field name
                    switch(switchString[0]) 
                    {
                        case "byr": passportStrings[passportCount,0] = switchString[1]; break;
                        case "iyr": passportStrings[passportCount,1] = switchString[1]; break;
                        case "eyr": passportStrings[passportCount,2] = switchString[1]; break;
                        case "hgt": passportStrings[passportCount,3] = switchString[1]; break;
                        case "hcl": passportStrings[passportCount,4] = switchString[1]; break;
                        case "ecl": passportStrings[passportCount,5] = switchString[1]; break;
                        case "pid": passportStrings[passportCount,6] = switchString[1]; break;
                        case "cid": passportStrings[passportCount,7] = switchString[1]; break;
                        default: break;
                    }
                }
            }

            // return full string array
            return passportStrings;
        }
        catch(Exception e)
        {
            Console.WriteLine("Unable to load file!");
            Console.WriteLine(e);
        }

        // return null array if we didn't open the file
        return null;
    }

    private int CheckPassports(string[,] passports)
    {
        int validPassports = 0;
        
        // loop over passports
        for (int i = 0; i < passports.GetLength(0); i++)
        {
            // start of validation check loop
            loopStart:

                // check for invalid passports due to null fields
                // ending a < 7 not < 8 because cid field is not considered
                for (int j = 0; j < 7; j++)
                {
                    // if a field is null
                    if (passports[i,j] == null)
                    {
                        // jump to next passport
                        i++;

                        // check if the next i is valid, else leave the loop
                        if (i < passports.GetLength(0))
                        {
                            goto loopStart;
                        }
                        else
                        {
                            goto loopEnd;
                        }
                    }
                }

                // byr(Birth Year) - four digits; at least 1920 and at most 2002.
                if (Int32.Parse(passports[i,0]) < 1920 || Int32.Parse(passports[i,0]) > 2002)
                {
                    continue;
                }

                // iyr(Issue Year) - four digits; at least 2010 and at most 2020.
                if (Int32.Parse(passports[i,1]) < 2010 || Int32.Parse(passports[i,1]) > 2020)
                {
                    continue;
                }
                
                // eyr(Expiration Year) - four digits; at least 2020 and at most 2030.
                if (Int32.Parse(passports[i,2]) < 2020 || Int32.Parse(passports[i,2]) > 2030)
                {
                    continue;
                }

                // hgt(Height) - a number followed by either cm or in:
                if (passports[i,3].Contains("cm"))
                {
                    // extract height
                    int height = Int32.Parse(Regex.Match(passports[i,3], @"\d+").Value);
                    
                    // If cm, the number must be at least 150 and at most 193.
                    if (height < 150 || height > 193)
                    {
                        continue;
                    }
                }
                else if (passports[i,3].Contains("in"))
                {
                    // extract height
                    int height = Int32.Parse(Regex.Match(passports[i,3], @"\d+").Value);

                    // If in, the number must be at least 59 and at most 76.
                    if (height < 59 || height > 76)
                    {
                        continue;
                    }
                }
                else
                {
                    continue;
                }

                // hcl(Hair Color) - a # followed by exactly six characters 0-9 or a-f.
                // count vaild characters
                int characterCount = Regex.Matches(passports[i,4], "[a-f0-9]").Count;

                if (!passports[i,4].Contains("#") || characterCount != 6)
                {
                    continue;
                }

                // ecl(Eye Color) - exactly one of: amb blu brn gry grn hzl oth.
                if (passports[i,5] != "amb" &&
                    passports[i,5] != "blu" &&
                    passports[i,5] != "brn" &&
                    passports[i,5] != "gry" &&
                    passports[i,5] != "grn" &&
                    passports[i,5] != "hzl" &&
                    passports[i,5] != "oth")
                {
                    continue;
                }

                // pid(Passport ID) - a nine-digit number, including leading zeroes.
                if (passports[i,6].Length != 9)
                {
                    continue;
                }

                validPassports++;
        }

        // end of validation loop
        loopEnd:
        
            return validPassports;
    }
}