
using Architecture.AppSettingsOptions;
using Architecture.CommonTypes.Web;
using Architecture.Services.CoinDeskService;
using Architecture.Services.CoinDeskService.Dto;
using Architecture.Services.JsonSerializer;
using Common.CoreCodeContracts;
using Implementation.Services.ApiClientServiceBase;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Implementation.Services.CoinDeskService
{
    public sealed class CoinDeskService :  ApiClientServiceBase.ApiClientServiceBase, ICoinDeskService
    {
        private readonly AppSettingsOptions _appSettings;

        public CoinDeskService(AppSettingsOptions appSettings, IHttpClientFactory httpClientFactory, IJSonSerializerService jSonSerializeService, ILogger<CoinDeskService> logger ) :
    base(httpClientFactory, jSonSerializeService, logger)
        {
            _appSettings = appSettings;
            CoreContracts.Postcondition(_appSettings != null);
        }

        public async Task<ApiCallResult<CoinDeskJsonDto>> GetCurrentPrice()
        {

            var client = _httpClientFactory.CreateClient("CoinDesk");
            var requestUri = _appSettings.CoinDeskWebApiUrl;
            var request = new HttpRequestMessage(HttpMethod.Get, requestUri);

            var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead);
            if (!response.IsSuccessStatusCode)
            {
                return HandleUnsuccessfulResponse<ApiCallResult<CoinDeskJsonDto>>(response);
            }
            var content = await _jSonSerializeService.DeserializeAsync<CoinDeskJsonDto>(response);


            var ret = new ApiCallResult<CoinDeskJsonDto>()
            {
                Data = content,
                Age = response.Headers.Age,
                MaxAge = response.Headers.CacheControl.MaxAge,
                Date = response.Headers.Date
            };
            return ret;
        }

    }
}
