using System.Threading.Tasks;

namespace BusinessModuleRemoteProxy
{
    public class BusinessModuleRemoteProxy : IBusinessModuleProxy
    {
        private readonly string _businessModuleEndPoint;
        public BusinessModuleRemoteProxy(string businessModuleEndPoint)
        {
            _businessModuleEndPoint = businessModuleEndPoint;
        }
        public Task<double> GetResultAsync()
        {
            throw new System.NotImplementedException();
        }
    }
}