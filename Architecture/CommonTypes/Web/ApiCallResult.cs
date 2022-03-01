using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.CommonTypes.Web
{
    /// <summary>
    /// Result of web api call in data with some info like expire get from header
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ApiCallResult<T>
    {
        // Result of Web Api Call
        public T Data { get; set; }

        public DateTimeOffset? Date { get; set; }

        public TimeSpan? Age { get; set; }

        public TimeSpan? MaxAge { get; set; }

    }

    /// <summary>
    /// Result of web api call in data with some info like expire get from header
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public sealed class ApiCallResultIE<T>
    {
        /// <summary>
        /// Result of Web Api Call
        /// </summary>
        public IEnumerable<T> Data { get; set; }


        public DateTimeOffset? Date { get; set; }

        public TimeSpan? Age { get; set; }

        public TimeSpan? MaxAge { get; set; }

    }

}