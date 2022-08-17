using System;

namespace HybridServices.Contract
{
    [AttributeUsage(AttributeTargets.Class)]
    public class HybridServiceImplementationAttribute : Attribute
    {
        public ResolveAs Implementation { get; set; }
    }
}