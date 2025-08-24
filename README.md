A console application I built to track and log daily coding sessions. This project reinforced my C# knowledge while introducing more advanced concepts such as DateTime handling, external libraries, Separation of Concerns, and ORM usage.

## Requirements

-  To show the data on the console, you should use the "Spectre.Console" library.
- You're required to have separate classes in different files (ex. UserInput.cs, Validation.cs, CodingController.cs)
- You should tell the user the specific format you want the date and time to be logged and not allow any other format.
- You'll need to create a configuration file that you'll contain your database path and connection strings.
- ou'll need to create a "CodingSession" class in a separate file. It will contain the properties of your coding session: Id, StartTime, EndTime, Duration
- The user shouldn't input the duration of the session. It should be calculated based on the Start and End times, in a separate "CalculateDuration" method.
- The user should be able to input the start and end times manually.
- You need to use Dapper ORM for the data access instead of ADO.NET. (This requirement was included in Feb/2024)
- When reading from the database, you can't use an anonymous object, you have to read your table into a List of Coding Sessions.
- Follow the DRY Principle, and avoid code repetition.
  

## Usage

- The console app connects to the database
- A user menu is created which allows the user to make a choice
- Each menu option has a method that performs the function selected, by using the menu you can perform operations against the database to add, remove and edit entries.
- The app stores and retrieves data from Coding Session table.
- The user can perform CRUD operations on the sessions.

## Lessons Learned
- Don't spend so long on one project and continuously come back to it freshing yourself over several months.
- Utilising code from previous projects to not 'reinvent the wheel' 
