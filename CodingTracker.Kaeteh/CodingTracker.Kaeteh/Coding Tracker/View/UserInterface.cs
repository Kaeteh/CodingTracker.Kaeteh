using Spectre.Console;
using System.Diagnostics;
using static Coding_Tracker.Enums;

namespace Coding_Tracker.View
    {
        public static class UserInterface
        {
    
            public static void Run()
            {
            bool isRunning = true;

            while (isRunning)
                {
                    Console.Clear();

                var actionChoice = Display.PrintMainMenu();

                switch (actionChoice)
                    {
                        case MenuActions.ViewRecords:
                            CodingController.CheckForRecord();
                            break;
                        case MenuActions.AddRecords:
                            CodingController.Insert();
                            break;
                        case MenuActions.UpdateRecords:
                            CodingController.Update();
                            break;
                        case MenuActions.DeleteRecords:
                            CodingController.Delete();
                        break;
                        case MenuActions.Exit:
                            return;
                    }
                }
            }

        }
    }

