using Architecture.BusinessLayer.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.BusinessLayer
{
    public interface IBusinessLayer
    {
        Task<TickerDto> GetBinancePrice();
    }
}
