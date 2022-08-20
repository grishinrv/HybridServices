using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace HybridServices.Utils.Helpers
{
    public static class ThrowHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void CheckArgumentNull<T>(T argument, string argumentName)
        {
            if (EqualityComparer<T>.Default.Equals(argument, default(T)))
                throw new ArgumentNullException($"{argumentName}");
        }
    }
}