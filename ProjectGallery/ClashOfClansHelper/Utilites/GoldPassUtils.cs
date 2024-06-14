using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows;

namespace ClashOfClansHelper;

public static class Utils
{
    public static bool SearchForMatchingString(List<string> values, string key)
    {
        string cleanKey = key.ToLower().Replace(" ", "");

        bool keyContainsName = false;

        foreach (string i in values)
        {
            if (cleanKey.Contains(i))
            {
                keyContainsName = true;
                break; // Exit the loop early if a match is found
            }
        }
        return keyContainsName;
    }

    public static string DisplayCorrectValueAfterGoldPass(string key, string value)
    // logic to calculate the value acording to the type of the key
    {
        List<string> goldPassDiscountsListResurces = new List<string>()
        {
            "gold", "elixir", "darkelixer",
        };
        string goldPassDiscoutBuilding = "build";

        string cleanKey = key.ToLower().Replace(" ", "");

        // finds if the key contains any of the gold pass resurce words
        if (goldPassDiscountsListResurces.Any(resource => cleanKey.IndexOf(resource) != -1))
        {
            // takes out anythings thats not a number
            string cleanValue = Regex.Replace(value, "[^0-9]", "");

            // returns the discounted resurce value of the inputed value
            if (double.TryParse(cleanValue, out double valueAsInt))
            {
                double result = valueAsInt * 0.8;
                string output = result.ToString();
                string finalOutput = "";
                string reversedString = Reverse(output);

                for (int i = 0; i < reversedString.Length; i++)
                {
                    if ((i % 3 == 0) & i > 0)
                    {
                        finalOutput += ",";
                    }
                    finalOutput += reversedString[i];
                }
                return Reverse(finalOutput);
            }
            // finds if the key has a build in it to assemble the discounted time
        }
        else if (cleanKey.Contains(goldPassDiscoutBuilding))
        {

            int hours = 0;

            string[] valueData = value.Split(' ');

            // test each value of the list and convert hours and days into hours
            foreach (string part in valueData)
            {

                string trimmedPart = part.Trim().ToLower();
                int currentPartInt = StringToNumber(trimmedPart);

                //finds the days
                if (trimmedPart.EndsWith("h"))
                {

                    hours += currentPartInt;
                }

                if (trimmedPart.EndsWith("d"))
                {

                    hours += currentPartInt * 24;
                }

            }

            double discountedHours = Math.Floor(hours * 0.8);

            if (discountedHours < 24)
            {
                return $"{discountedHours}h";

            }
            else if (discountedHours >= 24)
            {

                double days = Math.Floor(discountedHours / 24);

                double finalHours = discountedHours - days * 24;

                string finalString = $"{days}d " + (finalHours == 0 ? "" : $"{finalHours}h");

                return finalString;


            }
        }

        return value;
    }

    public static string Reverse(string s)
    {
        char[] charArray = s.ToCharArray();
        Array.Reverse(charArray);
        return new string(charArray);
    }

    private static int StringToNumber(string stringValue)
    {

        string cleanValue = Regex.Replace(stringValue, "[^0-9]", "");
        if (int.TryParse(cleanValue, out int valueAsInt))
        {
            return valueAsInt;
        }
        else
        {
            MessageBox.Show("there was an error converting a string to a number: returns 0");
            return 0;
        }
    }
}

