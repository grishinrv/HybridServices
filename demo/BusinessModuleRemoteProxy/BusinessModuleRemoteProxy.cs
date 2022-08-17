using System.Threading.Tasks;

namespace BusinessModuleRemoteProxy
{
    public class BusinessModuleRemoteProxy : IBusinessModuleProxy
    {
        public Task<double> AddAsync(double a, double b)
        {
            throw new System.NotImplementedException();
        }

        public Task<double> SubtractAsync(double a, double b)
        {
            throw new System.NotImplementedException();
        }

        public Task<double> DivideAsync(double a, double b)
        {
            throw new System.NotImplementedException();
        }

        public Task<double> MultiplyAsync(double a, double b)
        {
            throw new System.NotImplementedException();
        }
    }
}