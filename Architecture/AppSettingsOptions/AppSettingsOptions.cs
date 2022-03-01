using System;
using System.Collections.Generic;
using System.Text;

namespace Architecture.AppSettingsOptions
{
    public sealed class AppSettingsOptions
    {
        public ConnectionStringsOptions ConnectionStrings { get; set; }



        public int Environment { get; set; }

        /// <summary>
        /// CoindDesk
        /// </summary>
        public string CoinDeskWebApiUrl { get; set; }
        public CommonOptions Common { get; set; }

        public ProxyOptions Proxy { get; set; }

 

    }

    /// <summary>
    /// Common options from Config
    /// </summary>
    public sealed class ConnectionStringsOptions
    {

    }


    /// <summary>
    /// Common options from Config
    /// </summary>
    public sealed class CommonOptions
    {
        /// <summary>
        /// 
        //
        /// </summary>
        public bool CheckSSLCertificateRevocation { get; set; }

    }

    /// <summary>
    /// Proxy Options from Config. Allow spy with fiddler
    /// </summary>
    public sealed class ProxyOptions
    {
        public bool IsEnabled { get; set; }

        /// <summary> HttpClient use this proxy if enabled. Allow spy with fiddler.</summary>
        public string Address { get; set; }
    }


}


