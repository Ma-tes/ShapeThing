using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Text;
using System.Threading.Tasks;

namespace MouseThing
{
    public interface IRoad 
    {
         public char Symbols { get; } 
         public POINT Positions { get; } 
    }
    public class Road : IRoad
    {
        public char Symbols { get; set; }

        public POINT Positions { get; set; }
        public Road SetRoadParameter(char symbol, POINT position) => new Road {Symbols = symbol, Positions = position };
    }
    public interface IRoads 
    {
        public List<Road> RoadList { get; set; } 
    }
    public abstract class Roads<T> : Road, IRoads
    {
        public List<Road> RoadList { get; set; } = new();
       
        public T SetRoad(POINT position,char Symbol)
        {
            RoadList.Add(SetRoadParameter(Symbol, position));
            if (this is T t)
                return t;
            throw new InvalidCastException();
        }

        public void WriteRoads()
        {
            for (int i = 0; i < RoadList.Count; i++)
            {
                WriteRoad(i);
            } 
        }
        protected virtual void WriteRoad(int index) 
        {
            Console.SetCursorPosition(RoadList[index].Positions.x, RoadList[index].Positions.y);
            Console.Write(RoadList[index].Symbols);
        }
    }
    public class NormalRoads : Roads<NormalRoads>, IRoads
    {
        public List<ConsoleColor> colors = new();
        public NormalRoads SetColor(ConsoleColor color) { colors.Add(color); return this; }
        protected  override void WriteRoad(int index)
        {
            Console.ForegroundColor = colors.Count < index ? ConsoleColor.White : colors[index];
            base.WriteRoad(index);
            Console.ResetColor();
        }
    }
    public class ExtendedRoads : NormalRoads, IRoads 
    {
        public List<bool> Writable = new();
        public ExtendedRoads IsWritable(bool statement) 
        {
            Writable.Add(statement);
            return this;
        }
        protected override void WriteRoad(int index)
        {
            if(Writable[index] != false)
                base.WriteRoad(index);
        }
    }
}
