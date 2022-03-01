
using System.Text.Json;


namespace Architecture.Services.JsonSerializer
{
    public interface IJSonSerializerService
    {
        /// <summary>
        /// The is another version found in the code. faster than <see cref="DeserializeSlow"/> 
        /// But the remomende version is <see cref="DeserializeAsync"/> 
        /// Recmended in dubuging to spy easily the content.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="response"></param>
        /// <returns></returns>
        Task<T> DeserializeReadingAsString<T>(HttpResponseMessage response);

        /// <summary>
        /// Faster vesion unising streams
        /// </summary>
        /// <typeparam name="T">Type of the objec</typeparam>
        /// <param name="response">HttpReaponse. In fact the content</param>
        /// <returns>Object</returns>
        Task<T> DeserializeAsync<T>(HttpResponseMessage response, JsonSerializerOptions options = null);

        /// <summary>
        /// This is the Original way to do the things
        /// Looking for better preformance. <see cref="CreateStreamContent"/>   but it is not working
        ///  -- Serializing
        ///  -- Using stram but this poind could change how to use
        /// </summary>
        /// <typeparam name="T">Type of the object</typeparam>
        /// <param name="content">object to serialize</param>
        /// <returns>StringContent</returns>
        HttpContent CreateStringContent<T>(T content);

        /// <summary>
        /// Not Working
        /// For sure the is a way to use streams, but It taking me a bit of time
        /// Should be used in this way
        ///    HttpResponseMessage response;
        ///    using (var data = _jSonSerializeService.CreateStreamContent(request))
        ///    {
        ///request.Content = httpContent;
        /// response = await _httpClient.PutAsync("ConflictChecks/Status", data);
        /// }
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="content"></param>
        /// <returns></returns>
        //HttpContent CreateStreamContent<T>(T content);
    }

}
