
namespace Powielanie
{
    using Prism;

    public class Dodawanie : ICallable
    {
        [OpisAtrybutu("Metoda dodająca", "Liczba całkowitoliczbowa", "Liczba całkowitoliczbowa")]
        public int call(int a, int b)
        {
            return a + b;
        }

    }

}

