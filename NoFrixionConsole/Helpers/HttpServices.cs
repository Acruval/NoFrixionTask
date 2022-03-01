using Architecture.AppSettingsOptions;
using Common.CoreCodeContracts;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;


namespace NoFrixionConsole.Helpers
{

    /// <summary>
    /// https://josef.codes/you-are-probably-still-using-httpclient-wrong-and-it-is-destabilizing-your-software/
    /// </summary>
    internal static class HttpServices
    {
        public static void Add(IServiceCollection services, AppSettingsOptions appSettings, IEnumerable<string> httpServicesNames)
        {
            CoreContracts.Precondition(httpServicesNames.Contains("Default"));

            if (appSettings.Proxy.IsEnabled)
            {
                foreach (var httpServicesName in httpServicesNames)
                {
                    services.AddHttpClient(httpServicesName, (sp, c) =>
                    {
                        c.DefaultRequestHeaders.Add("Accept", "application/json");
                        //  c.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                        //  c.DefaultRequestHeaders.Add("Pragma", "no-cache");
                        c.DefaultRequestHeaders.Add("Accept-Encoding", "br, gzip, deflate");

                    }
                    ).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
                    {
                        Proxy = new WebProxy(appSettings.Proxy.Address),
                        Credentials = CredentialCache.DefaultCredentials,
                        AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli,


                    });
                }

            }
            else
            {
                if (appSettings.Common.CheckSSLCertificateRevocation == false)
                {
                    //ServicePointManager.ServerCertificateValidationCallback = ServerCertificateCustomValidator;
                    // https://stackoverflow.com/questions/38138952/bypass-invalid-ssl-certificate-in-net-core
                    foreach (var httpServicesName in httpServicesNames)
                    {
                        services.AddHttpClient(httpServicesName, (sp, c) =>
                        {
                            c.DefaultRequestHeaders.Add("Accept", "application/json");
                            //   c.DefaultRequestHeaders.Add("Cache-Control", "no-cache");
                            //  c.DefaultRequestHeaders.Add("Pragma", "no-cache");
                            c.DefaultRequestHeaders.Add("Accept-Encoding", "br, gzip, deflate");

                        }

                            ).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler()
                            {
                                Credentials = CredentialCache.DefaultCredentials,
                                ServerCertificateCustomValidationCallback = ServerCertificateCustomValidatorForHttpClient,
                                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate | DecompressionMethods.Brotli,

                            });
                    }
                }
                else
                {
                    foreach (var httpServicesName in httpServicesNames)
                    {
                        services.AddHttpClient("Default");
                    }
                }
            }

        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
#pragma warning disable IDE0051 // Remove unused private members
#pragma warning disable IDE0060 // Remove unused parameter
        private static bool ServerCertificateCustomValidator(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)

        {
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool ServerCertificateCustomValidatorForHttpClient(HttpRequestMessage sender, X509Certificate2 cert, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

#pragma warning restore IDE0060 // Remove unused parameter
#pragma warning restore IDE0051 // Remove unused private members

    }
}

