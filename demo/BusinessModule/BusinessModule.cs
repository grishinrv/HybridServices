using System;
using System.Threading.Tasks;
using HybridServices.Contract;

namespace BusinessModule
{
    [HybridService]
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

        /// <summary>
        /// Simulate IO call.
        /// </summary>
        public async Task<string> GetResultWithParamAsync(string parameter)
        {
            int milliseconds = _random.Next(300, 5000);
            await Task.Delay(milliseconds);
            return $"Calculated response on \"{parameter}\" for {milliseconds / 1000.0} seconds";
        }
    }
}