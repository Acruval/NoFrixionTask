using Architecture.Services.JsonSerializer;
using Common.CoreCodeContracts;
using Microsoft.Extensions.Logging;


namespace Implementation.Services.ApiClientServiceBase
{
    public abstract class ApiClientServiceBase
    {
        protected readonly IHttpClientFactory _httpClientFactory;
        protected readonly IJSonSerializerService _jSonSerializeService;
        protected readonly ILogger _logger;


        public ApiClientServiceBase(IHttpClientFactory httpClientFactory, IJSonSerializerService jSonSerializeService, ILogger logger)
        {
            CoreContracts.Precondition(httpClientFactory != null);
            CoreContracts.Precondition(jSonSerializeService != null);
            CoreContracts.Precondition(logger != null);
            _httpClientFactory = httpClientFactory;
            _jSonSerializeService = jSonSerializeService;
            _logger = logger;
            CoreContracts.Postcondition(_httpClientFactory != null);
            CoreContracts.Postcondition(_jSonSerializeService != null);
            CoreContracts.Precondition(_logger != null);
        }


        //TODO For other project manage Token
        #region Dead_code
        /// <summary>
        /// 
        /// </summary>
        ///// <param name="token"></param>
        //public void UseToken(string token)
        //{
        //    if (!string.IsNullOrEmpty(token)) _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        //}

        //TODO REview this to to like
        //UnauthorizedAccessException("Invalid Conflict Checker Api Token");
        //UnauthorizedAccessException("Invalid Conflict Checker Api Token");


        ///// <summary>
        /////  If response.StatusCode == System.Net.HttpStatusCode.Unauthorized throw UnauthorizedAccessException();
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="response">HttpResponseMessage </param>
        ///// <returns>T or UnauthorizedAccessException </returns>
        ///// <exception cref="System.Net.HttpStatusCode.Unauthorized">Launch if response.StatusCode == System.Net.HttpStatusCode.Unauthorized</exception>
        //protected T HandleUnsuccessfulResponse<T>(HttpResponseMessage response)
        //{
        //    return HandleUnsuccessfulResponse<T>(response, handle500: true);
        //}
        #endregion Dead_code


        /// <summary>
        ///  Manage error usually if response.Statuscode!= 200 Ok
        ///  If response.StatusCode=!= System.Net.HttpStatusCode.Unauthorized throw UnauthorizedAccessException();
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response">HttpResponseMessage </param>
        /// <param name="errorContext">error Context Info</param>
        /// <exception cref="System.Net.HttpStatusCode.Unauthorized">Launch if response.StatusCode == System.Net.HttpStatusCode.Unauthorized</exception>
        protected T HandleUnsuccessfulResponse<T>(HttpResponseMessage response, string errorContext = null)
        {
            switch (response.StatusCode)
            {
                //Bad Request could be an especial case.
                case System.Net.HttpStatusCode.Unauthorized:
                    throw new UnauthorizedAccessException($"{errorContext} Web Api Call (401) Unauthorized server error {response.RequestMessage.Method} {response.RequestMessage.RequestUri} Error");
                //break;                
                default:
                    var jsonError = _jSonSerializeService.DeserializeReadingAsString<string>(response).GetAwaiter().GetResult();
                    string error = $"Web Api Call ({(int)response.StatusCode}) {response.StatusCode} {response.RequestMessage.Method} {response.RequestMessage.RequestUri} Error";
                    var ex = new Exception(error + ": '" + jsonError);
                    _logger.LogError(ex, error);
                    throw ex;
                    //break;

            }
            // It is going to launch an exception always
            //return default(T);
        }


        /// <summary>
        /// Handle no conted to be called to the inherit class if it is needed
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        protected T HandleNoContentResponse<T>(HttpResponseMessage response)
        {
            //var jsonError = _jSonSerializeService.DeserializeReadingAsString<string>(response).GetAwaiter().GetResult();
            string error = $"Web Api Call ({(int)response.StatusCode}) {response.StatusCode} {response.RequestMessage.Method} {response.RequestMessage.RequestUri} Error";
            var ex = new Exception(error + ": '" /*+ jsonError*/);
            _logger.LogError(ex, error);
            throw ex;
        }
    }
}
