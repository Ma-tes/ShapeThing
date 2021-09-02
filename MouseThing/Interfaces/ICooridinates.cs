using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseThing
{
    interface ICoordinates<T>
    {
        void Write();
        bool Compare(T objectC);
    }
}
