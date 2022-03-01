using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.BusinessLayer.Dto
{

    /// <summary>
    /// At the moment it is doing just small transformation of data.
    /// It is good to have your own Ticker class, candleClass, Time Interval Enum, despite your provider
    /// </summary>
    public sealed  class TickerDto
    {
        /// <summary>
        /// Name of the Crypto coin
        /// </summary>
       public string Coin { get; set; }


        /// <summary>
        /// Price. The standard is in EUR. It it easy operate with a base currency. 
        /// May be it when we are buying selling to avoid exchange fees could be interesting no use our base currency.
        /// In that casewe would need another field to mark the currency
        /// </summary>
        public float Price { get; set; }

    }
}
