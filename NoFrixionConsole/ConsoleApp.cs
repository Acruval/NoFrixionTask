using Architecture.BusinessLayer;
using Common.CoreCodeContracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoFrixionConsole
{
    public class ConsoleApp
    {
        private readonly IBusinessLayer _businessLayer;

        private readonly ILogger<ConsoleApp> _logger;
        public ConsoleApp(ILogger<ConsoleApp> logger,  IBusinessLayer businessLayer)
        {
            _logger = logger;
            _businessLayer = businessLayer;
            CoreContracts.Postcondition(_logger != null);
            CoreContracts.Postcondition(_businessLayer != null);
        }


        public async Task RunAsync()
        {
            var result = await _businessLayer.GetBinancePrice();
            // Ishoukld get pauts. But No I will show like floart
            //Console.Write("*BTC Price EUR" + string.Format(new System.Globalization.CultureInfo("en-IE"), "{0:C}*", result.Price)) ;
            Console.Write("*BTC Price EUR {0:f}*", result.Price);

        }

    }
}
