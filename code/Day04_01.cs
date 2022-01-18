using System;
using System.Collections.Generic;

/**

You arrive at the airport only to realize that you grabbed your North Pole Credentials instead of your passport. While these documents are extremely similar, North Pole Credentials aren't issued by a country and therefore aren't actually valid documentation for travel in most of the world.

It seems like you're not the only one having problems, though; a very long line has formed for the automatic passport scanners, and the delay could upset your travel itinerary.

Due to some questionable network security, you realize you might be able to solve both of these problems at the same time.

The automatic passport scanners are slow because they're having trouble detecting which passports have all required fields. The expected fields are as follows:

byr(Birth Year)
iyr(Issue Year)
eyr(Expiration Year)
hgt(Height)
hcl(Hair Color)
ecl(Eye Color)
pid(Passport ID)
cid(Country ID)

Passport data is validated in batch files(your puzzle input). Each passport is represented as a sequence of key:value pairs separated by spaces or newlines. Passports are separated by blank lines.

Here is an example batch file containing four passports:

ecl:gry pid:860033327 eyr:2020 hcl:#fffffd
byr:1937 iyr:2017 cid:147 hgt:183cm

iyr:2013 ecl:amb cid:350 eyr:2023 pid:028048884
hcl:#cfa07d byr:1929

hcl:#ae17e1 iyr:2013
eyr:2024
ecl:brn pid:760753108 byr:1931
hgt:179cm

hcl:#cfa07d eyr:2025 pid:166559648
iyr:2011 ecl:brn hgt:59in

The first passport is valid - all eight fields are present. The second passport is invalid - it is missing hgt(the Height field).

The third passport is interesting; the only missing field is cid, so it looks like data from North Pole Credentials, not a passport at all! Surely, nobody would mind if you made the system temporarily ignore missing cid fields. Treat this "passport" as valid.

The fourth passport is missing two fields, cid and byr. Missing cid is fine, but missing any other field is not, so this passport is invalid.

According to the above rules, your improved system would report 2 valid passports.

Count the number of valid passports - those that have all required fields. Treat cid as optional. In your batch file, how many passports are valid?

**/

public class Day04_01
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
            // check all required fields are there
            if (passports[i,0] != null &&
                passports[i,1] != null &&
                passports[i,2] != null &&
                passports[i,3] != null &&
                passports[i,4] != null &&
                passports[i,5] != null &&
                passports[i,6] != null)
            {
                validPassports++;
            }
        }

        return validPassports;
    }
}