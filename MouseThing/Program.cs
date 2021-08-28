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
            MouseInput MouseInput = new (new POINT { x = 0, y = 0 });
            MouseInput ConsoleMouseInput = new(new POINT { x = 0, y = 0 });
            MouseInput LastPosition;
            WindowInput WindowCoord = new WindowInput();
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
                 WindowCoord = new WindowInput { Rect = PinVokeHelper.GetWindowRectangle(Console.Title, WindowCoord.Rect) };
                MouseInput = new MouseInput(PinVokeHelper.GetCursorPosition(MouseInput.CursorPosition, processeID));
                LastPosition = ConsoleMouseInput;
                ConsoleMouseInput = new MouseInput
                (
                     new POINT
                     {
                         x = (MouseInput.CursorPosition.x - WindowCoord.Rect.Left) / 8,
                         y = (MouseInput.CursorPosition.y - WindowCoord.Rect.Top) / 18
                     }
                );
                for (int i = 0; i < RoadListChar.Count; i++)
                {
                    Console.SetCursorPosition(RoadListPos[i].x, RoadListPos[i].y);
                    Console.Write(RoadListChar[i]);
                }
                var objectMoving = ConsoleMouseInput.Compare(LastPosition) ? ("STOP", ConsoleColor.Red) : ("MOVE", ConsoleColor.Green);
                Console.ForegroundColor = objectMoving.Item2;
                Console.ResetColor();
                
                Console.SetCursorPosition(ConsoleMouseInput.CursorPosition.x, ConsoleMouseInput.CursorPosition.y);
                Console.WriteLine((char)ConnectRoad.GetRoadShape(RoadTypes, ConnectRoad.GetNeighborsList(ConsoleMouseInput.CursorPosition, RoadListPos)));
                if (PinVokeHelper.OnInput(ConsoleKey.Spacebar)) 
                {
                    RoadListPos.Add(ConsoleMouseInput.CursorPosition);
                    RoadListChar.Add((char)ConnectRoad.GetRoadShape(RoadTypes, ConnectRoad.GetNeighborsList(ConsoleMouseInput.CursorPosition, RoadListPos)));
                }
                if (PinVokeHelper.OnInput(ConsoleKey.R)) 
                {
                    var newRoads = ConnectRoad.UpdateRect(RoadListPos);
                    foreach (var road in newRoads) 
                    {
                        Console.WriteLine($"{road.Left}{road.Top}{road.Right}{road.Bottom}");
                    }
                    Console.WriteLine("----");
                    foreach (var road in RaodListRect) 
                    {
                        Console.WriteLine($"{road.Left}{road.Top}{road.Right}{road.Bottom}");
                    }
                     ConnectRoad.UpdateChar(newRoads,ref RoadListChar,RoadTypes);
                }

            }
        }
    }
}
