using Architecture.AppSettingsOptions;
using Architecture.BusinessLayer;
using Architecture.BusinessLayer.Dto;
using Architecture.Services.CoinDeskService;
using Common.CoreCodeContracts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.BusinessLayer
{
    public sealed class BusinessLayer : IBusinessLayer
    {
        private readonly ILogger<BusinessLayer> _logger;

        private readonly AppSettingsOptions _appSettings;

        private readonly ICoinDeskService _coinDeskService;
        public BusinessLayer(ILogger<BusinessLayer> logger, AppSettingsOptions appSettings,  ICoinDeskService coinDeskService)
        {
            _logger = logger;
            _appSettings = appSettings;
            _coinDeskService = coinDeskService;
            CoreContracts.Postcondition(_logger!= null);
            CoreContracts.Postcondition(_appSettings != null);
            CoreContracts.Postcondition(_coinDeskService != null);
        }


        public async Task<TickerDto> GetBinancePrice()
        {
            var result = await _coinDeskService.GetCurrentPrice();

            // We have result.MaxAge we could cache the result and avoid new call untils expire depemnding some settings


            var data = result.Data;
            var ret = new TickerDto { Coin = data.ChartName, Price = data.Bpi.EUR.Rate };
            return ret;
        }



    }
} 
