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
            ExtendedRoads roads = new ExtendedRoads();

            while (true)
            {
                MouseInput mouseInput = new( POINT.Zero );
                WindowInput windowCoord = new WindowInput();
                MouseInput consoleMouseInput;

                Thread.Sleep(10);
                Console.Clear();
                windowCoord = new WindowInput { Rect = PInvokeHelper.GetWindowRectangle(Console.Title, windowCoord.Rect) };
                mouseInput = new MouseInput(PInvokeHelper.GetCursorPosition(mouseInput.CursorPosition));
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
                if (PInvokeHelper.OnInput(ConsoleKey.Spacebar))
                    roads.IsWritable(true).SetRoad(consoleMouseInput.CursorPosition, RoadHelper.GetRoadShape(RoadType.DefaultRoadTypes, RoadHelper.GetNeighborsList(consoleMouseInput.CursorPosition, roads.Positions)))
                         .SetColor((ConsoleColor)random.Next(0,10));

                if (PInvokeHelper.OnInput(ConsoleKey.R))
                    RoadHelper.UpdateRoadChar(ref roads, RoadType.DefaultRoadTypes);
            }
        }
    }
}
