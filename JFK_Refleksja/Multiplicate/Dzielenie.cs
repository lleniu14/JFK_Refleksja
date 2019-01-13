namespace Multiplicate
{
    using Prism;
    public class Dzielenie : ICallable
    {
        [OpisAtrybutu("Metoda dzieląca", "Liczba całkowitoliczbowa", "Liczba całkowitoliczbowa")]
        public int call(int a, int b)
        {
            return (a / b);
        }
    }
}
