namespace Multiplicate
{
    using Prism;
    public class Mnozenie : ICallable
    {
        [OpisAtrybutu("Metoda mnożąca", "Liczba całkowitoliczbowa", "Liczba całkowitoliczbowa")]
        public int call(int a, int b)
        {
            return a * b;
        }
    }
}
