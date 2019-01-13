using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Powielanie
{
    using Prism;

    public class Odejmowanie : ICallable
    {
        [OpisAtrybutu("Metoda odejmująca", "Argument pierwszy liczba całkowitoliczbowa", "Argument drugi liczba całkowitoliczbowa")]
        public int call(int a, int b)
        {
            return a - b;
        }



    }
}
