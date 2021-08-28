using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MouseThing
{
    interface ICooridinates<T>
    {
        void Write();
        bool Compare(T obeject);
    }
}
