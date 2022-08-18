using System;

namespace HybridServices.Core
{
    public abstract class HybridServicesCoreException : Exception
    {
        protected HybridServicesCoreException(string message) : base(message)
        {
        }
    }
}