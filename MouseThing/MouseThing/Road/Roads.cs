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
    }
    public class NormalRoads : Roads 
    {
        public int Count => Symbols.Count;
    }
}
