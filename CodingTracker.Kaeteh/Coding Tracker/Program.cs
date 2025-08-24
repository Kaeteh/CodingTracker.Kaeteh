using Spectre.Console;
using Dapper;
using Microsoft.Data.Sqlite;
using Coding_Tracker.View;
using Coding_Tracker.Controller;

DataBaseSrvs codingTracker = new DataBaseSrvs();
DataBaseSrvs.CreateTable();
DataBaseSrvs.SeedData();
UserInterface.Run();


