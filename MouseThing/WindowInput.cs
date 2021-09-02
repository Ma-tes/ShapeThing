using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseThing
{
    public class WindowInput : ICoordinates<WindowInput>
    {
        public RECT Rect { get; set; }
        public void Write()
        {
            Console.WriteLine($"Left = {Rect.Left} Right = {Rect.Right} Top = {Rect.Top} Bottom = {Rect.Bottom}");
        }
        public bool Compare(WindowInput objectD)
        {
            return Rect.Equals(objectD.Rect);
        }
    }
}
