using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseThing
{
    public class MouseInput : ICooridinates<MouseInput>
    {
        public POINT CursorPosition { get; set; }
    
        public MouseInput(POINT cursorPosition) => CursorPosition = cursorPosition;

        public void Write() 
        {
            Console.WriteLine($"X = {CursorPosition.x} Y = {CursorPosition.y}");
        }
        public bool Compare(MouseInput obeject) 
        {
            return CursorPosition.Equals(obeject.CursorPosition);
        } 
    }

}
