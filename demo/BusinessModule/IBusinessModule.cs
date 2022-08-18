using System.Threading.Tasks;

namespace BusinessModule
{
    public interface IBusinessModule
    {
        Task<double> GetResultAsync();
    }
}