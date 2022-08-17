using System.Threading.Tasks;

namespace BusinessModuleRemoteProxy
{
    public interface IBusinessModuleProxy
    {
        Task<double> AddAsync(double a, double b);
        Task<double> SubtractAsync(double a, double b);
        Task<double> DivideAsync(double a, double b);
        Task<double> MultiplyAsync(double a, double b);
    }
}