using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Text;

namespace MouseThing
{
    class Program
    {
        static void Main()
        {
            Random random = new Random();
            Console.Title = "Alpha 0.0.1";
            NormalRoads roads = new NormalRoads();
            roads.colors = new();
            #region Write all Processes on computer
            var localProcesses = Process.GetProcesses();
            int processeID;
            for (int i = 0; i < localProcesses.Length; i++)
            {
                Console.WriteLine($"[{i}] {localProcesses[i].ProcessName} ID: {localProcesses[i].Id}");
            }
            processeID = int.Parse(Console.ReadLine());
            #endregion

            while (processeID != 0)
            {
                MouseInput mouseInput = new( POINT.Zero );
                WindowInput windowCoord = new WindowInput();
                MouseInput consoleMouseInput;

                Thread.Sleep(10);
                Console.Clear();
                windowCoord = new WindowInput { Rect = PinVokeHelper.GetWindowRectangle(Console.Title, windowCoord.Rect) };
                mouseInput = new MouseInput(PinVokeHelper.GetCursorPosition(mouseInput.CursorPosition));
                consoleMouseInput = new MouseInput
                (
                     new POINT
                     {
                         x = (mouseInput.CursorPosition.x - windowCoord.Rect.Left) / 8,
                         y = (mouseInput.CursorPosition.y - windowCoord.Rect.Top) / 18
                     }
                );
                roads.WriteRoads();
                // Write current shape on your mouse position without saving them
                Console.SetCursorPosition(consoleMouseInput.CursorPosition.x, consoleMouseInput.CursorPosition.y);
                Console.WriteLine(RoadHelper.GetRoadShape(RoadType.DefaultRoadTypes, RoadHelper.GetNeighborsList(consoleMouseInput.CursorPosition, roads.Positions)));

                if (PinVokeHelper.OnInput(ConsoleKey.Spacebar))
                {
                    roads.SetRoad<NormalRoads>(consoleMouseInput.CursorPosition, RoadHelper.GetRoadShape(RoadType.DefaultRoadTypes, RoadHelper.GetNeighborsList(consoleMouseInput.CursorPosition, roads.Positions)))
                         .SetColor((ConsoleColor)random.Next(0,10));
                }

                if (PinVokeHelper.OnInput(ConsoleKey.R))
                    RoadHelper.UpdateRoadChar(ref roads, RoadType.DefaultRoadTypes);
                
            }
        }
    }
}
