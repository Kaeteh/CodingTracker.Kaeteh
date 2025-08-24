using Coding_Tracker.Models;
using Spectre.Console;
using static Coding_Tracker.Enums;

public static class Display
{
    private static readonly Dictionary<string, MenuActions> _menuOptions = new()
    {
        { "View All Records", MenuActions.ViewRecords },
        { "Add Record", MenuActions.AddRecords },
        { "Edit Record", MenuActions.UpdateRecords },
        { "Delete Record", MenuActions.DeleteRecords },
        { "Exit Application", MenuActions.Exit }
    };

    internal static MenuActions PrintMainMenu()
    {
        Console.Clear();
        
        AnsiConsole.Write(
            new Panel("[bold fuchsia]*** Coding Tracker *** [/]")
                .Border(BoxBorder.Double)
                .Padding(1, 1)
        );

        var selected = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .PageSize(10)
                .HighlightStyle("slateblue1")
                .AddChoices(_menuOptions.Keys)
        );

        return _menuOptions[selected];
    }

    internal static void PrintCodingSessions(IEnumerable<CodingSession> sessions)
    {
        if (sessions == null)
        {
            AnsiConsole.MarkupLine("[red]No sessions to display.[/]");
            return;
        }
        var table = new Table();
        table.Border = TableBorder.Rounded;
        table.Expand = true;
        
        table.AddColumn("[magenta]ID[/]");
        table.AddColumn("[magenta]Start Time[/]");
        table.AddColumn("[magenta]End Time[/]");
        table.AddColumn("[magenta]Duration[/]");

        foreach (var session in sessions)
        {
            table.AddRow(
                session.Id.ToString(),
                session.StartTime.ToString("yyyy-MM-dd HH:mm"),
                session.EndTime.ToString("yyyy-MM-dd HH:mm"),
                session.Duration);
        }
        AnsiConsole.Render(table);

    }
}
