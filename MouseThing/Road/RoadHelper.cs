using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseThing
{
    /// <summary>
    /// This ASCII shapes i found here <see href="https://theasciicode.com.ar/">See more</see> 
    /// </summary>
    public enum Shape // TODO Better name
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
    public static class RoadHelper // TODO Better Name
    {
        public static char GetRoadShape(List<RoadType> roadTypes, List<RECT> neighbors)
        {
            if (neighbors.Count == 0)
                return (char)Shape.Box;
            var shapeRect = ConnectNeighbors(RectToIntList(neighbors));
            for (int i = 0; i < roadTypes.Count; i++)
            {
                if (shapeRect.Equals(roadTypes[i].ValidPoints))
                {
                    return roadTypes[i].RoadShapeSymbol;
                }
            }
            return (char)Shape.Box;
        }
        private static List<RECT> UpdateRect(List<POINT> roadPositions)
        {
            List<RECT> rectList = new();
            for (int i = 0; i < roadPositions.Count; i++)
            {
                var updatedList = roadPositions;
                rectList.Add(ConnectNeighbors(RectToIntList(GetNeighborsList(roadPositions[i], updatedList))));
            }
            return rectList;
        }
        public static void UpdateRoadChar<T>(ref T roads, List<RoadType> shapes) where T : Roads
        {
            var connectedNeighbors = UpdateRect(roads.Positions);
            for (int i = 0; i < connectedNeighbors.Count; i++)
            {
                for (int j = 0; j < shapes.Count; j++)
                {
                    if (connectedNeighbors[i].Equals(shapes[j].ValidPoints))
                    {
                        if (roads.Symbols[i] != shapes[j].RoadShapeSymbol)
                            roads.Symbols[i] = shapes[j].RoadShapeSymbol;
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
                return new List<int[]> { new int[4] };
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
            return new RECT
            {
                Left = (index[0]) * -1,
                Top = (index[1]) * -1,
                Right = (index[2]),
                Bottom = (index[3]),
            };
        }
    }
}
