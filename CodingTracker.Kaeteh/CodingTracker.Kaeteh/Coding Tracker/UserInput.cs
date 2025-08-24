using Coding_Tracker.Models;
using Coding_Tracker.View;
using System;

namespace Coding_Tracker
{
    public static class UserInput
    {
        public static class AppConstants
        {
            public const string DateTimeFormat = "yyyy-MM-dd HH:mm";
        }
        public static DateTime GetValidDateTime(string prompt)
        {
            while (true)
            {
                Console.WriteLine(prompt);
                string input = Console.ReadLine()?.Trim();

                if (Validation.IsValidDateTime(input, out DateTime result))
                    return result;

                Console.WriteLine($"Invalid format! Please use: {AppConstants.DateTimeFormat}");
            }
        }

        public static (DateTime StartTime, DateTime EndTime) GetSessionTimes()
        {
            Console.WriteLine("Enter your coding session times:");

            DateTime startTime = GetValidDateTime($"Start time ({AppConstants.DateTimeFormat}):");

            DateTime endTime;

            do
            {
                endTime = GetValidDateTime($"End time ({AppConstants.DateTimeFormat}):");

                if (endTime <= startTime)
                    Console.WriteLine("End time must be later than start time!");
            }
            while (endTime <= startTime);

            return (startTime, endTime);
        }
    }
}
