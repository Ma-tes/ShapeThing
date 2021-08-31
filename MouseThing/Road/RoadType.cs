using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseThing
{
    /// <summary>
    /// The <c>RoadType</c> demonstrate road rectangles and shape
    /// </summary>
    public sealed class RoadType
    {
        /// <summary>
        ///     Pre-create List with all different shapes and TEXT to correspond with end of each char.
        /// </summary>
        public static readonly List<RoadType> DefaultRoadTypes = new() 
            {
                new(new RECT (0, -1, 1, 0), (char)Shape.LDownCorner),
                new(new RECT (-1,-1, 0, 0), (char)Shape.RDownCorner),
                new(new RECT (0, 0, 1, 1), (char)Shape.LTopCorner),
                new(new RECT (-1, 0, 0, 1), (char)Shape.RTopCorner),
                new(new RECT (-1, 0, 0, 0), (char)Shape.HorizontalLine),
                new(new RECT (0, 0, 1, 0 ), (char)Shape.HorizontalLine),
                new(new RECT (-1, 0, 1, 1), (char)Shape.TShape),
                new(new RECT (-1, -1, 1, 0), (char)Shape.ReverseTShape),
                new(new RECT (0, -1, 1, 1), (char)Shape.LeftTShape),
                new(new RECT (-1, -1, 0, 1), (char)Shape.RightTShape),
                new(new RECT (0, -1, 0, 0 ), (char)Shape.VerticalLine),
                new(new RECT (0, 0, 0, 1 ), (char)Shape.VerticalLine),
                new(new RECT (-1, -1, 1, 1), (char)Shape.RoundaBoat),
                new(new RECT (0, 0, 0, 0 ), (char)Shape.Box),
            };

        /// <example>
        /// Set connecting points
        ///     <code>
        ///         new(0,0,0,0)
        ///     </code>
        /// </example>
        public RECT ValidPoints { get; set; }

        public char RoadShapeSymbol { get; set; }

        public RoadType(RECT validPoints, char roadShapeSymbol) => (ValidPoints, RoadShapeSymbol) = (validPoints, roadShapeSymbol);
    }
}
