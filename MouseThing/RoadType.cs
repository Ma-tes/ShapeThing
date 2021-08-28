using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseThing
{
    public enum Shapes // TODO Better name
    {
        LDownCorner = '╚',
        RDownCorner = '╝',
        LTopCorner = '╔',
        RTopCorner = '╗',
        TShape = '╦',
        ReverseTShape = '╩',
        VerticalLine = '║',
        HorizontalLine = '═',
        Box = '▄',
        RoundaBoat = '╬',
        LeftTShape = '╠',
        RightTShape = '╣'
    }
    public class RoadType
    {
        public RECT ValidPoints { get; set; }

        public Shapes RoadShapeSymbol { get; set; }

        public RoadType(RECT validPoints, Shapes roadShapeSymbol) => (ValidPoints, RoadShapeSymbol) = (validPoints, roadShapeSymbol);
    }
    public static class ConnectRoad // TODO Better Name
    {
        public static Shapes GetRoadShape(List<RoadType> roadTypes, List<RECT> neighbors)
        {
            // Example -> roadPos = (45,46, 28, 29)... 
            // Exmaple -> cursorPos = (44,48)
            if (neighbors.Count == 0)
                return Shapes.Box;
            var Shape = ConnectNeighbors(RectToIntList(neighbors));
            for (int i = 0; i < roadTypes.Count; i++)
            {
                if (Shape.Equals(roadTypes[i].ValidPoints))
                {
                    return roadTypes[i].RoadShapeSymbol;
                }
            }
            return Shapes.HorizontalLine;
        }
        public static List<RECT> UpdateRect(List<POINT> roadPositions)
        {
            List<RECT> rectList = new();
            for (int i = 0; i < roadPositions.Count; i++)
            {
                var updatedList = roadPositions;
                rectList.Add(ConnectNeighbors(RectToIntList(GetNeighborsList(roadPositions[i], updatedList))));
            }
            return rectList;
        }
        public static void UpdateChar(List<RECT> connectedNeighbors, ref List<char> roadCharList, List<RoadType> shapes)
        {
            for (int i = 0; i < connectedNeighbors.Count; i++)
            {
                for (int j = 0; j < shapes.Count; j++)
                {
                    if (connectedNeighbors[i].Equals(shapes[j].ValidPoints))
                    {
                        if (roadCharList[i] != (char)shapes[j].RoadShapeSymbol)
                            roadCharList[i] = (char)shapes[j].RoadShapeSymbol;
                    }
                }
            }
        }
        public static List<RECT> GetNeighborsList(POINT cursorPosition, List<POINT> roadPositions)
        {
            List<RECT> pointList = new List<RECT>();
            for (int i = 0; i < roadPositions.Count; i++)
            {
                int x = cursorPosition.x - roadPositions[i].x;
                int y = cursorPosition.y - roadPositions[i].y;
                POINT point = new POINT(x, y);
                if ((Math.Abs(x) == 1 && y == 0) || Math.Abs(y) == 1 && x == 0)
                {
                    RECT pointRect = new RECT() { Left = 0, Right = 0, Bottom = 0, Top = 0 };
                    if (point.x == -1)
                        pointRect.Right = point.x;
                    else
                        pointRect.Left = point.x;
                    if (point.y == -1)
                        pointRect.Bottom = point.y;
                    else
                        pointRect.Top = point.y;
                    pointList.Add(pointRect);
                }
            }
            return pointList;
        }
        private static List<int[]> RectToIntList(List<RECT> neighbors)
        {
            if (neighbors.Count == 0)
                return new List<int[]> { new int[] { 0, 0, 0, 0 } };
            var neighborsList = new List<int[]>();
            for (int i = 0; i < neighbors.Count; i++)
            {
                neighborsList.Add(new int[] { neighbors[i].Left, neighbors[i].Top, neighbors[i].Right * -1, neighbors[i].Bottom * -1 });
            }
            return neighborsList;
        }
        private static RECT ConnectNeighbors(List<int[]> neighborsBytes)
        {
            int[] index = neighborsBytes[0];
            for (int i = 1; i < neighborsBytes.Count; i++)
            {
                for (int j = 0; j < neighborsBytes[i].Length; j++)
                {
                    index[j] |= neighborsBytes[i][j];
                }
            }
            return ByteToRect(index.IntoString());
        }
        private static string IntoString<T>(this T[] valueArray) where T : struct
        {
            string resultValue = string.Empty;
            for (int i = 0; i < valueArray.Length; i++)
            {
                resultValue += valueArray[i];
            }
            return resultValue;
        }
        private static RECT ByteToRect(string value)
        {
            string valueString = value;
            RECT resultRect = new()
            {
                Left = int.Parse(valueString[0].ToString()) * -1,
                Top = int.Parse(valueString[1].ToString()) * -1,
                Right = int.Parse(valueString[2].ToString()),
                Bottom = int.Parse(valueString[3].ToString()),
            };
            return resultRect;
        }
    }
}
