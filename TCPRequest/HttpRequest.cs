using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.Net;
using SocketProxy.Proxy;
namespace TCPRequest
{
    public enum ProxyType
    {
        None,
        Http,
        Socks4,
        Socks4a,
        Socks5
    }
    public class HttpRequest
    {
        TcpClient client = new TcpClient();
        public string Request = "";
        public string Response = "";
        public int StatusCode;
        public bool KeepAlive;
        public bool NoDelay;

        public void Post(string URL, Headers Headers, string Body = null, string proxyString = null, ProxyType type = ProxyType.None)
        {
            System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)(3072 | 768 | 192);
            if (string.IsNullOrWhiteSpace(URL))
            {
                throw new ArgumentNullException("URL can not be null or empty");
            }
            Uri urlRequest = new Uri(URL);
            // Connect socket
            if (proxyString != null)
            {
                if (type == ProxyType.Http)
                {
                    Uri proxy = new Uri($"https://{proxyString}");
                    HttpProxyClient httpProxyClient = new HttpProxyClient(proxy.Host, proxy.Port);
                    client = httpProxyClient.CreateConnection(urlRequest.Host, urlRequest.Port);
                }
                else if (type == ProxyType.Socks4a)
                {
                    Uri proxy = new Uri($"Socks4a://{proxyString}");
                    Socks4aProxyClient socks4aProxyClient = new Socks4aProxyClient(proxy.Host, proxy.Port);
                    client = socks4aProxyClient.CreateConnection(urlRequest.Host, urlRequest.Port);
                }
                else if (type == ProxyType.Socks4)
                {
                    Uri proxy = new Uri($"Socks4://{proxyString}");
                    Socks4ProxyClient socks4ProxyClient = new Socks4ProxyClient(proxy.Host, proxy.Port);
                    client = socks4ProxyClient.CreateConnection(urlRequest.Host, urlRequest.Port);
                }
                else if (type == ProxyType.Socks5)
                {
                    Uri proxy = new Uri($"Socks5://{proxyString}");
                    Socks5ProxyClient socks5ProxyClient = new Socks5ProxyClient(proxy.Host, proxy.Port);
                    client = socks5ProxyClient.CreateConnection(urlRequest.Host, urlRequest.Port);
                }
                else
                {
                    client.Connect(urlRequest.Host, urlRequest.Port);
                }
            }
            else
            {
                client.Connect(urlRequest.Host, urlRequest.Port);
            }

            if (NoDelay == true)
            {
                client.NoDelay = true;
            }
            else if (NoDelay == false)
            {
                client.NoDelay = false;
            }
            else
            {
                client.NoDelay = true;
            }
            SslStream networkStream = new SslStream(client.GetStream());
            networkStream.AuthenticateAsClient(urlRequest.Host);
            networkStream.ReadTimeout = 2000;
            var builder = new StringBuilder();
            builder.AppendLine($"POST {urlRequest.AbsolutePath} HTTP/1.1");
            builder.AppendLine($"Host: {urlRequest.Host}");
            builder.Append(Headers.headersHandler);
            builder.AppendLine("Content-Length: " + Body.Length);
            if (KeepAlive == true)
            {
                builder.AppendLine("Connection: keep-alive");
            }
            else if (KeepAlive== false)
            {
                builder.AppendLine("Connection: close");
            }
            else
            {
                builder.AppendLine("Connection: close");
            }
            builder.AppendLine();
            builder.AppendLine(Body);
            var reader = new StreamReader(networkStream, Encoding.UTF8);
            Request = builder.ToString();
            byte[] bytes = Encoding.UTF8.GetBytes(Request);
            networkStream.WriteAsync(bytes, 0, bytes.Length);
            Response = reader.ReadToEnd();
            networkStream.Dispose();
            reader.Dispose();
        }
        public void Dispose()
        { 
            client.Dispose();
        }
    }
    public class Headers
    {
        public StringBuilder headersHandler = new StringBuilder();
        public static string Accept = "Accept";
        public static string ContentType = "Content-Type";
        public static string UserAgent = "User-Agent";
        public static string Referer = "Referer";
        public static string Origin = "Origin";
        public static string AcceptLanguage = "Accept-Language";
        public static string AcceptEncoding = "Accept-Encoding";
        public void Add(string Name, string Value)
        {
            headersHandler.AppendLine($"{Name}: {Value}");
        }
        public void Clear()
        {
            headersHandler.Clear();
        }
    }
}
