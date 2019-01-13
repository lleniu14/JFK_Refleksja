

namespace Prism
{
    using System;
    using System.IO;
    using System.Reflection;

    using static System.Console;

    public static class Program
    {
        public static void Main(string[] args)
        {
            var path = 0 == args.Length ? string.Empty : args[0];//określenie ścieżki do załadowania
            if (!File.Exists(path))
            {
                return;
            }
            var assembly = Assembly.LoadFrom(path);//Ładowanie biblioteki

            Console.WriteLine("Podaj pierwszą liczbę całkowitą:");
            int a = Int32.Parse(Console.ReadLine());
            Console.WriteLine("Podaj drugą liczbę całkowitą:");
            int b = Int32.Parse(Console.ReadLine());

            foreach (var type in assembly.GetExportedTypes())//dla każdego typu //reprezentacja deklaracji typu petlą przechodzimy po wszystkich typach biblioteki
            {
                var descriptionAttribute = type.GetCustomAttribute<OpisAtrybutu>(true);//przszukiwanie typu szukanego atrybutu
                if (null != descriptionAttribute)
                {
                    Write("Opis: '{0}'", descriptionAttribute.opis);
                    Write("Argument pierwszy: '{1}'", descriptionAttribute.ParametrA);
                    Write("Argument drugi: '{2}'", descriptionAttribute.ParametrB);
                }

                if (!typeof(ICallable).IsAssignableFrom(type))//czy implementuje kontrakt
                {
                    continue;
                }

                if (!(Activator.CreateInstance(type) is ICallable callable))
                {
                    throw new InvalidOperationException();
                }

                WriteLine("Wynik : {0}", callable.call(a, b));

            }
        }

    }
}
