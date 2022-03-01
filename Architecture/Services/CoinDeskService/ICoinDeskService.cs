using Architecture.CommonTypes.Web;
using Architecture.Services.CoinDeskService.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.Services.CoinDeskService
{
    public interface ICoinDeskService
    {
       Task<ApiCallResult<CoinDeskJsonDto>> GetCurrentPrice();
    }
}
