using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Text;

namespace MouseThing
{
    class Program
    {
        static void Main(string[] args)
        {
            MouseInput mouseInput = new(new POINT { x = 0, y = 0 });
            MouseInput consoleMouseInput = new(new POINT { x = 0, y = 0 });
            MouseInput lastPosition;
            WindowInput windowCoord = new WindowInput();
            var RoadTypes = new List<RoadType>
            {
                new(new RECT {Left = 0, Top = -1, Right = 1, Bottom = 0 }, Shapes.LDownCorner),
                new(new RECT {Left = -1, Top = -1, Right = 0, Bottom = 0 }, Shapes.RDownCorner),
                new(new RECT {Left = 0, Top = 0, Right = 1, Bottom = 1 }, Shapes.LTopCorner),
                new(new RECT {Left = -1, Top = 0, Right = 0, Bottom = 1 }, Shapes.RTopCorner),
                new(new RECT {Left = -1, Top = 0, Right = 0, Bottom = 0 }, Shapes.HorizontalLine),
                new(new RECT {Left = 0, Top = 0, Right = 1, Bottom = 0 }, Shapes.HorizontalLine),
                new(new RECT {Left = -1, Top = 0, Right = 1, Bottom = 1}, Shapes.TShape),
                new(new RECT {Left = -1, Top = -1, Right = 1, Bottom = 0}, Shapes.ReverseTShape),
                new(new RECT {Left = 0, Top = -1, Right = 1, Bottom = 1}, Shapes.LeftTShape),
                new(new RECT {Left = -1, Top = -1, Right = 0, Bottom = 1}, Shapes.RightTShape),
                new(new RECT {Left = 0, Top = -1, Right = 0, Bottom = 0 }, Shapes.VerticalLine),
                new(new RECT {Left = 0, Top = 0, Right = 0, Bottom = 1 }, Shapes.VerticalLine),
                new(new RECT {Left = -1, Top = -1, Right = 1, Bottom = 1 }, Shapes.RoundaBoat),
                new(new RECT {Left = 0, Top = 0, Right = 0, Bottom = 0 }, Shapes.Box),

            };
            var RoadListPos = new List<POINT>();
            var RoadListChar = new List<char>();
            var RaodListRect = new List<RECT>();
            RoadListPos.Add(new POINT { x = 5, y = 15 });
            RoadListChar.Add((char)Shapes.HorizontalLine);
            var localProcesses = Process.GetProcesses();
            int processeID = 0;

            for (int i = 0; i < localProcesses.Length; i++)
            {
                Console.WriteLine($"[{i}] {localProcesses[i].ProcessName} ID: {localProcesses[i].Id}");
            }
            processeID = int.Parse(Console.ReadLine());
            while (processeID != 0)
            {
                Thread.Sleep(10);
                Console.Clear();
                Console.Title = "Test";
                windowCoord = new WindowInput { Rect = PinVokeHelper.GetWindowRectangle(Console.Title, windowCoord.Rect) };
                mouseInput = new MouseInput(PinVokeHelper.GetCursorPosition(mouseInput.CursorPosition, processeID));
                lastPosition = consoleMouseInput;
                consoleMouseInput = new MouseInput
                (
                     new POINT
                     {
                         x = (mouseInput.CursorPosition.x - windowCoord.Rect.Left) / 8,
                         y = (mouseInput.CursorPosition.y - windowCoord.Rect.Top) / 18
                     }
                );
                for (int i = 0; i < RoadListChar.Count; i++)
                {
                    Console.SetCursorPosition(RoadListPos[i].x, RoadListPos[i].y);
                    Console.Write(RoadListChar[i]);
                }

                Console.SetCursorPosition(consoleMouseInput.CursorPosition.x, consoleMouseInput.CursorPosition.y);
                Console.WriteLine((char)ConnectRoad.GetRoadShape(RoadTypes, ConnectRoad.GetNeighborsList(consoleMouseInput.CursorPosition, RoadListPos)));
                if (PinVokeHelper.OnInput(ConsoleKey.Spacebar))
                {
                    RoadListPos.Add(consoleMouseInput.CursorPosition);
                    RoadListChar.Add((char)ConnectRoad.GetRoadShape(RoadTypes, ConnectRoad.GetNeighborsList(consoleMouseInput.CursorPosition, RoadListPos)));
                }
                if (PinVokeHelper.OnInput(ConsoleKey.R))
                {
                    var newRoads = ConnectRoad.UpdateRect(RoadListPos);
                    ConnectRoad.UpdateChar(newRoads, ref RoadListChar, RoadTypes);
                }
            }
        }
    }
}
