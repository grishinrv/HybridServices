using System;
using System.Collections.Generic;

namespace HybridServices.Core
{
    public class EndpointDescriptor
    {
        internal Type ServiceType { get; set; }
        internal string MethodName { get; set; }
        internal List<EndpointArgumentDescriptor> Arguments { get; set; }
    }
}