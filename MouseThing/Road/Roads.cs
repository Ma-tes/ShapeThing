using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MouseThing
{
    public abstract class Roads
    {
        public List<char> Symbols = new();
        public List<POINT> Positions = new();
        public virtual void AddNewRoad(POINT position, char symbol) 
        {
            this.Positions.Add(position);
            this.Symbols.Add(symbol);
        }
        public virtual void WriteOnPosition() 
        {
            for (int i = 0; i < Positions.Count; i++)
            {
                Console.SetCursorPosition(Positions[i].x, Positions[i].y);
                Console.Write(Symbols[i]);
            }
        }
    }
    public class NormalRoads : Roads 
    {
        public int Count => Symbols.Count;
        public List<ConsoleColor> colors = null; 
        public sealed override void AddNewRoad(POINT position, char symbol)
        {
            base.AddNewRoad(position, symbol);
        }
        public override void WriteOnPosition()
        {
            for (int i = 0; i < Positions.Count; i++)
            {
                Console.ForegroundColor = colors is null || colors.Count < i ? ConsoleColor.White : colors[i];
                Console.SetCursorPosition(Positions[i].x, Positions[i].y);
                Console.Write(Symbols[i]);
                Console.ResetColor();
            }
        }

    }
}
