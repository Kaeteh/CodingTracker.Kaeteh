using Coding_Tracker.Models;
using Coding_Tracker.View;
using Dapper;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using Spectre.Console;

namespace Coding_Tracker.Controller
{
    internal static class Config
    {
        public static readonly string ConnectionString;

        static Config()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
            
            ConnectionString = configuration.GetConnectionString("CodingTrackerDb");
        }
    }
    internal class DataBaseSrvs
    {
        public static void CreateTable()
        {
            using var connection = new SqliteConnection(Config.ConnectionString);
            connection.Open();

            var tableCmd = connection.CreateCommand();
            tableCmd.CommandText =
            @"
                CREATE TABLE IF NOT EXISTS coding_sessions(
                    id INTEGER PRIMARY KEY AUTOINCREMENT,
                    startTime DATETIME NOT NULL,
                    endTime DATETIME NOT NULL,
                    duration TEXT
                );";
            tableCmd.ExecuteNonQuery();
        }

        internal static void SeedData()
        {
            using var connection = new SqliteConnection(Config.ConnectionString);
            connection.Open();

            var count = connection.ExecuteScalar<int>("SELECT COUNT(*) FROM coding_sessions;");
            if (count > 0)
            {
                Console.WriteLine("The data already exists in the table, skipping..");
                return;
            }

            var sessions = new List<CodingSession>
            {
                new CodingSession { StartTime = DateTime.Now.AddHours(-3), EndTime = DateTime.Now.AddHours(-2) },
                new CodingSession { StartTime = DateTime.Now.AddDays(-1).AddHours(-2), EndTime = DateTime.Now.AddDays(-1).AddHours(-1) },
                new CodingSession { StartTime = DateTime.Now.AddDays(-2).AddHours(-4), EndTime = DateTime.Now.AddDays(-2).AddHours(-2) }
            };

            foreach (var session in sessions)
            {
                string duration = Validation.CalculateDuration(session.StartTime, session.EndTime);

                string sql = @"
                    INSERT INTO coding_sessions (startTime, endTime, duration)
                    VALUES (@StartTime, @EndTime, @Duration);";

                connection.Execute(sql, new
                {
                    StartTime = session.StartTime,
                    EndTime = session.EndTime,
                    Duration = duration
                });
            }

            Console.WriteLine("Seed data inserted.");
        }

        internal static List<CodingSession> Query(string command, object parameters = null)
        {
            using var connection = new SqliteConnection(Config.ConnectionString);
            return connection.Query<CodingSession>(command, parameters).ToList();
        }

        internal static List<CodingSession> GetCodingSessions()
        {
            return Query("SELECT * FROM coding_sessions;");
        }

        internal static void CheckRecords()
        {
            var sessions = GetCodingSessions();
            if (sessions.Count == 0)
            {
                Console.WriteLine("There are no records");
                Console.WriteLine("Please add a record first.");
                Console.ReadLine();
                UserInterface.Run();
            }
            else
            {
                Display.PrintCodingSessions(sessions);
            }
        }

        internal static CodingSession SelectRecordByID(int recordId)
        {
            var sessions = Query("SELECT * FROM coding_sessions WHERE Id = @Id;", new { Id = recordId });
            return sessions.FirstOrDefault();
        }

        internal static bool RecordExists(int id)
        {
            using var connection = new SqliteConnection(Config.ConnectionString);
            connection.Open();

            string sql = "SELECT COUNT(1) FROM coding_sessions WHERE id = @Id;";
            int count = connection.ExecuteScalar<int>(sql, new { Id = id });

            return count > 0;
        }

        internal static void InsertRecord(DateTime startTime, DateTime endDateTime)
        {
            using var connection = new SqliteConnection(Config.ConnectionString);
            connection.Open();

            string duration = Validation.CalculateDuration(startTime, endDateTime);

            string sql = @"
                INSERT INTO coding_sessions (startTime, endTime, duration)
                VALUES (@StartTime, @EndTime, @Duration);";

            connection.Execute(sql, new
            {
                StartTime = startTime,
                EndTime = endDateTime,
                Duration = duration
            });
        }

        internal static bool DeleteRecordById(int id)
        {
            using var connection = new SqliteConnection(Config.ConnectionString);
            connection.Open();

            string deleteSql = "DELETE FROM coding_sessions WHERE id = @Id;";
            int affectedRows = connection.Execute(deleteSql, new { Id = id });

            return affectedRows > 0;
        }

        internal static void UpdateRecord(int id, DateTime newStartTime, DateTime newEndTime)
        {
            using var connection = new SqliteConnection(Config.ConnectionString);
            connection.Open();

            string duration = Validation.CalculateDuration(newStartTime, newEndTime);

            string sql = @"
                UPDATE coding_sessions 
                SET startTime = @StartTime,
                    endTime = @EndTime,
                    duration = @Duration
                WHERE id = @Id;";

            connection.Execute(sql, new
            {
                Id = id,
                StartTime = newStartTime,
                EndTime = newEndTime,
                Duration = duration
            });
        }
    }
}
