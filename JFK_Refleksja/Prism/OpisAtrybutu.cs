
namespace Prism
{

    using System;

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = true)]
    public sealed class OpisAtrybutu : Attribute
    {
        public OpisAtrybutu(String opiss, String paraA, String paraB)
        {
            this.opis = opiss;
            this.ParametrA = paraA;
            this.ParametrB = paraB;
        }
        public String opis { get; set; }
        public String ParametrA { get; set; }
        public String ParametrB { get; set; }
    }
}
