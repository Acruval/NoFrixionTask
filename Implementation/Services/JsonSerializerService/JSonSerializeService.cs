using Architecture.Services.JsonSerializer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;


namespace Implementation.Services.JsonSerializerService
{
    public sealed class JSonSerializeService
    {
        /// <summary>
        /// The purpose of this class is isolate how JSon is serialize deserialize.
        /// https://michaelscodingspot.com/the-battle-of-c-to-json-serializers-in-net-core-3/
        /// Expected to be injected like Singleton
        /// </summary>
        public sealed class JSonSerializerService : IJSonSerializerService
        {

            #region  Deserialize
            /// <summary>
            /// This was used in a lot of places
            /// I even not going to expose the Interface
            /// http://byterot.blogspot.com/2014/10/performance-series-how-poor-performance-httpcontent-readasasync-httpclient-asp-net-web-api.html
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="response"></param>
            /// <returns></returns>
            public async Task<T> DeserializeSlow<T>(HttpResponseMessage response)
            {
                await Task.FromResult(0);
                throw new NotImplementedException();
               //var ret = await response.Content.ReadAsAsync<T>();
               // return ret;

            }

            /// <summary>
            /// The is another version found in the code. faster than <see cref="DeserializeSlow"/> 
            /// But the remomende version is <see cref="DeserializeAsync"/> 
            /// Recmended in dubuging to spy easily the content.
            /// </summary>
            /// <typeparam name="T"></typeparam>
            /// <param name="response"></param>
            /// <returns></returns>
            public async Task<T> DeserializeReadingAsString<T>(HttpResponseMessage response)
            {
                string json = await response.Content.ReadAsStringAsync();
                var type = typeof(T);
                if (type.Name != "String")
                {
                    // TODO A  Way to force I Enumerable to String. To check
                    // var cc = JsonConvert.DeserializeObject<T>(json);
                    var cc = JsonSerializer.Deserialize<T>(json);
                    return cc;
                }

                TypeConverter tc = TypeDescriptor.GetConverter(type);
                return (T)tc.ConvertFrom(json);

            }


            /// <summary>
            /// Faster vesion unising streams
            /// </summary>
            /// <typeparam name="T">Type of the objec</typeparam>
            /// <param name="response">HttpReaponse. In fact the content</param>
            /// <returns>Object</returns>
            public async Task<T> DeserializeAsync<T>(HttpResponseMessage response, JsonSerializerOptions options = null)
            {

                T ret;

                using (var contentStream = await response.Content.ReadAsStreamAsync())
                {
                    ret = await JsonSerializer.DeserializeAsync<T>(contentStream, options);
                }
                return ret;
            }



            #endregion  Deserialize

            #region Serialize

            /// <summary>
            /// This is the Original way to do the things. remember it only serializes properties. not attributes
            /// Looking for better preformance. <see cref="CreateStreamContent"/>   but it is not working
            ///  -- Serializing
            ///  -- Using stram but this poind could change how to use
            /// </summary>
            /// <typeparam name="T">Type of the object</typeparam>
            /// <param name="content">object to serialize</param>
            /// <returns>StringContent</returns>
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public HttpContent CreateStringContent<T>(T content)
            {
                // remember it only serializes properties. not attributes
                //string jsonStr = JsonSerializer.SerializeObject(content);
                string jsonStr = JsonSerializer.Serialize<T>(content);
                var ret = new StringContent(jsonStr, Encoding.UTF8, "application/json");
                return ret;
            }


            #endregion Serialize
        }
    }
}
