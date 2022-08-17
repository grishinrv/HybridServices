namespace HybridServices.Contract
{
    /// <summary>
    /// When applied to a parameter, indicates that configuration (Local/Remote) should be checked to resolve the parameter.
    /// </summary>
    [AttributeUsage(AttributeTargets.Parameter)]
    public class HybridServiceAttribute : Attribute
    {
        
    } 
}
