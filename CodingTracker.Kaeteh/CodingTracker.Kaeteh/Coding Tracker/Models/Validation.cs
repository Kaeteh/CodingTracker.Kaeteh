using System;
using System.Globalization;
using static Coding_Tracker.UserInput;

namespace Coding_Tracker.Models
{
    internal static class Validation
    {
        public static bool IsValidDateTime(string input, out DateTime result)
        {
            return DateTime.TryParseExact(
                input,
                AppConstants.DateTimeFormat,
                CultureInfo.InvariantCulture,
                DateTimeStyles.None,
                out result
            );
        }

        internal static int GetNumberInput(string message)
        {
            Console.WriteLine(message);
            string numberInput = Console.ReadLine();

            while (!int.TryParse(numberInput, out int finalInput) || finalInput < 0)
            {
                Console.WriteLine("\nInvalid number. Try again.\n");
                numberInput = Console.ReadLine();
            }

            return int.Parse(numberInput);
        }

        internal static string CalculateDuration(DateTime startTime, DateTime endTime)
        {
            TimeSpan duration = endTime - startTime;

            int days = duration.Days;
            int hours = duration.Hours;
            int minutes = duration.Minutes;

            string result = "";

            if (days > 0)
                result += $"{days}d ";

            if (hours > 0 || days > 0)
                result += $"{hours}h ";

            result += $"{minutes}m";

            return result.Trim();
        }

    }
}
