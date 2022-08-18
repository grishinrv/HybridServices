using System;
using System.Threading.Tasks;

namespace BusinessModule
{
    public class BusinessModuleImplementation : IBusinessModule
    {
        private readonly Random _random = new Random();
        
        /// <summary>
        /// Simulate IO call.
        /// </summary>
        public async Task<double> GetResultAsync()
        {
            int milliseconds = _random.Next(300, 5000);
            await Task.Delay(milliseconds);
            return milliseconds / 1000.0;
        }
    }
}