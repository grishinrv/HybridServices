using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace HybridServices.Core
{
    public abstract class EndpointResolverBase
    {
        protected readonly List<EndpointDescriptor> _endpoints;
        protected EndpointResolverBase(List<EndpointDescriptor> endpoints)
        {
            _endpoints = endpoints;
        }

        private EndpointDescriptor MatchEndpoint(ParameterInfo[] parameters, string methodName = "", string classShortName = "")
        {
            List<EndpointDescriptor> matchedEndpoints = null;
            if (methodName != string.Empty && classShortName != string.Empty)
            {
                matchedEndpoints = _endpoints.Where(x => x.ServiceType.Name == classShortName 
                        && x.MethodName == methodName
                        && x.Arguments.Count == parameters.Length
                        && x.Arguments.All(a => a.Type == parameters[a.Order].ParameterType))
                    .ToList();
            }
            else
            {
                matchedEndpoints = _endpoints.Where(x=> x.Arguments.Count == parameters.Length
                        && x.Arguments.All(a => a.Type == parameters[a.Order].ParameterType))
                    .ToList();
            }

            if (matchedEndpoints.Count > 1)
                throw new EndpointResolveException("Could not resolve an Endpoint: several endpoints matched!");

            if (matchedEndpoints.Count == 0)
                throw new EndpointResolveException("Could not resolve an Endpoint: no endpoints matched!");

            return matchedEndpoints.First();
        }
    }
}