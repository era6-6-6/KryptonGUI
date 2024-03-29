﻿using Krypton_Core.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Krypton_Core
{
    public class HttpClient
    {

        public string lastUrl = "https://www.google.com";

        public string userAgent = "BigpointClient/1.6.7";
        public readonly CookieContainer cookies = new CookieContainer();

        private readonly WebHeaderCollection headers = new WebHeaderCollection();
        public Tuple<string, int, string, string> GetterProxy = ProxyList.ReturnOneProxy();

        public string Post(string url, string data , bool useProxy = false)
        {
            
            WebProxy currentProxy = new WebProxy(GetterProxy.Item1, GetterProxy.Item2);
            currentProxy.Credentials = new NetworkCredential(GetterProxy.Item3, GetterProxy.Item4);
            currentProxy.BypassProxyOnLocal = false;

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression =
                DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None;
            request.CookieContainer = cookies;
            if (useProxy)
            {
              // request.Proxy = currentProxy;
            }
            else
            {
                request.Proxy = null;
            }
            request.UserAgent = userAgent;
            request.Headers = headers;
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            request.Referer = lastUrl;

            using (var writer = new StreamWriter(request.GetRequestStream()))
            {
                writer.Write(data);
            }

            var response = (HttpWebResponse)request.GetResponse();
            lastUrl = response.ResponseUri.ToString();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }

        public string Get(string url, bool useProxy = false)
        {
            
            WebProxy currentProxy = new WebProxy(GetterProxy.Item1, GetterProxy.Item2);
            currentProxy.Credentials = new NetworkCredential(GetterProxy.Item3, GetterProxy.Item4);
            currentProxy.BypassProxyOnLocal = false;

            var request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression =
                DecompressionMethods.Deflate | DecompressionMethods.GZip | DecompressionMethods.None;
            request.CookieContainer = cookies;
            if (useProxy)
            {
               // request.Proxy = currentProxy;
            }
            else
            {
                request.Proxy = null;
            }

            request.UserAgent = userAgent;
            //request.Headers = headers;
            request.Method = "GET";


            var response = (HttpWebResponse)request.GetResponse();
            lastUrl = response.ResponseUri.ToString();
            using (var reader = new StreamReader(response.GetResponseStream()))
            {
                return reader.ReadToEnd();
            }
        }

        public void AddHeader(string header, string value)
        {
            if (header == "BigpointClient/1.6.3")
            {
                userAgent = value;
                return;
            }

            headers.Add(header, value);
        }
    }
}