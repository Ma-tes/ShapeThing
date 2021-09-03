using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace MouseThing
{
    public abstract class Roads
    {
        public List<char> Symbols = new();
        public List<POINT> Positions = new();
        public T SetRoad<T>(POINT position,char Symbol)
        {
            Symbols.Add(Symbol); 
            Positions.Add(position);
            return (T)Convert.ChangeType(this, typeof(T));
        }
        public void WriteRoads()
        {
            for (int i = 0; i < Symbols.Count; i++)
            {
                WriteRoad(i);
            } 
        }
        protected virtual void WriteRoad(int index) 
        {
            Console.SetCursorPosition(Positions[index].x, Positions[index].y);
            Console.Write(Symbols[index]);
        }
    }
    public class NormalRoads : Roads 
    {
        public int Count => Symbols.Count;
        public List<ConsoleColor> colors = new();

        public NormalRoads SetColor(ConsoleColor color) { colors.Add(color); return this; }
        protected override void WriteRoad(int index)
        {
            Console.ForegroundColor = colors.Count < index ? ConsoleColor.White : colors[index];
            base.WriteRoad(index);
            Console.ResetColor();
        }

    }
}
