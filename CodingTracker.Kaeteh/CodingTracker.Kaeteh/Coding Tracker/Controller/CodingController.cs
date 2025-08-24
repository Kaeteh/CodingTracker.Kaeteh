using Coding_Tracker;
using Coding_Tracker.Controller;
using Coding_Tracker.Models;
using Coding_Tracker.View;
using Spectre.Console;

internal static class CodingController
{
    public static void ViewRecords(List<CodingSession> sessions)
    {
        var table = new Table();
        table.AddColumn("ID");
        table.AddColumn("Date");
        table.AddColumn("Start Time");
        table.AddColumn("End Time");
        table.AddColumn("Duration");

        foreach (var session in sessions)
        {
            string duration;

            if (!string.IsNullOrWhiteSpace(session.Duration))
            {
                duration = session.Duration;
            }
            else
            {
                var span = session.EndTime - session.StartTime;
                duration = span.ToString(@"hh\:mm");
            }

            table.AddRow(
                session.Id.ToString(),
                session.StartTime.ToString("yyyy-MM-dd"),
                session.StartTime.ToString("HH:mm"),
                session.EndTime.ToString("HH:mm"),
                duration
            );
        }

        AnsiConsole.Write(table);
        Console.ReadLine();
    }

    public static void Insert()
    {
        var (startTime, endTime) = UserInput.GetSessionTimes();

        CodingSession session = new CodingSession
        {
            StartTime = startTime,
            EndTime = endTime
        };

        DataBaseSrvs.InsertRecord(startTime, endTime);

    }

    public static void Update()
    {
        DataBaseSrvs.CheckRecords();

        int recordId;

        while (true)
        {
            recordId = Validation.GetNumberInput("Please type the Id of the record you would like to update. Type 0 to return to main menu.");

            if (recordId == 0)
            {
                UserInterface.Run();
                return;
            }

            if (DataBaseSrvs.RecordExists(recordId))
            {
                break;
            }

            Console.WriteLine($"\nRecord with Id {recordId} doesn't exist. Please try again.");
        }

        Console.WriteLine("Enter the new start and end times for the session:");
        var (newStart, newEnd) = UserInput.GetSessionTimes();

        DataBaseSrvs.UpdateRecord(recordId, newStart, newEnd);

        Console.WriteLine("Record successfully updated.");
        Console.ReadKey();
    }

    public static void Delete()
    {
        DataBaseSrvs.CheckRecords();

        int recordId;

        while (true)
        {
            recordId = Validation.GetNumberInput("Enter the Id of the record to delete. Type 0 to return to the main menu:");

            if (recordId == 0)
            {
                return; // Exit the method, back to menu
            }

            if (DataBaseSrvs.RecordExists(recordId))
            {
                break;
            }

            Console.WriteLine($"\nRecord with Id {recordId} doesn't exist. Please try again.");
        }

        bool deleted = DataBaseSrvs.DeleteRecordById(recordId);

        if (deleted)
        {
            Console.WriteLine($"\nRecord with Id {recordId} was deleted.");
        }
        else
        {
            Console.WriteLine($"\nFailed to delete record with Id {recordId}.");
        }

        Console.WriteLine("Press any key to return to the main menu.");
        Console.ReadKey();
    }



    internal static bool CheckReturnToMenu(string input)
    {
        if (input == "0")
        {
            UserInterface.Run();
            return true;
        }
        return false;
    }

    public static void CheckForRecord()
    {
        DataBaseSrvs.CheckRecords();
        Console.ReadLine();
    }

}

