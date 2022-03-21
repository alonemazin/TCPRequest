using System.Net.Security;
using System.Net.Sockets;
using System.Text;
using System.Net;
using SocketProxy.Proxy;

namespace TCPRequest
{
    public static class Proxy
    {
        public static HttpProxyClient HttpsProxy(string Proxy, string Username = "", string Password = "")
        {
            Uri URIproxy = new Uri($"https://{Proxy}");
            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
            {
                return new HttpProxyClient(URIproxy.Host, URIproxy.Port, Username, Password);
            }
            else
            {
                return new HttpProxyClient(URIproxy.Host, URIproxy.Port);
            }
        }

        public static HttpProxyClient Socks4Proxy(string Proxy, string Username = "", string Password = "")
        {
            Uri URIproxy = new Uri($"Socks4a://{Proxy}");
            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
            {
                return new HttpProxyClient(URIproxy.Host, URIproxy.Port, Username, Password);
            }
            else
            {
                return new HttpProxyClient(URIproxy.Host, URIproxy.Port);
            }
        }
        public static HttpProxyClient Socks4aProxy(string Proxy, string Username = "", string Password = "")
        {
            Uri URIproxy = new Uri($"Socks4://{Proxy}");
            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
            {
                return new HttpProxyClient(URIproxy.Host, URIproxy.Port, Username, Password);
            }
            else
            {
                return new HttpProxyClient(URIproxy.Host, URIproxy.Port);
            }
        }

        public static HttpProxyClient Socks5Proxy(string Proxy, string Username = "", string Password = "")
        {
            Uri URIproxy = new Uri($"Socks5://{Proxy}");
            if (!string.IsNullOrWhiteSpace(Username) && !string.IsNullOrWhiteSpace(Password))
            {
                return new HttpProxyClient(URIproxy.Host, URIproxy.Port, Username, Password);
            }
            else
            {
                return new HttpProxyClient(URIproxy.Host, URIproxy.Port);
            }
        }
    }
    public class HttpRequest
    {
        TcpClient client = new TcpClient();
        public string Request = String.Empty;
        public string Response = String.Empty;
        public int StatusCode;
        public bool KeepAlive = false;
        public bool NoDelay = true;

        public string Post(string URL, Headers Headers = null, string Body = "", HttpProxyClient Proxy = null)
        {
            if (string.IsNullOrWhiteSpace(URL))
            {
                throw new ArgumentNullException("URL can not be null or empty");
            }
            return CreateRequest("POST", URL, Headers, Body, Proxy);
        }

        public string Get(string URL, Headers Headers = null, HttpProxyClient Proxy = null)
        {
            if (string.IsNullOrWhiteSpace(URL))
            {
                throw new ArgumentNullException("URL can not be null or empty");
            }
            return CreateRequest("GET", URL, Headers, null, Proxy);
        }

        private string CreateRequest(string Method, string URL, Headers Headers = null, string Body= "", HttpProxyClient Proxy = null)
        {
            System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)(3072 | 768 | 192);

            Uri urlRequest = new Uri(URL);
            // Connect socket

            if (Proxy !=null)
            {
                client = Proxy.CreateConnection(urlRequest.Host, urlRequest.Port);
            }
            else
            {
                client.Connect(urlRequest.Host, urlRequest.Port);
            }
            
            if (!NoDelay)
            {
                NoDelay = false;
            }

            client.NoDelay = NoDelay;
            SslStream networkStream = new SslStream(client.GetStream());
            networkStream.AuthenticateAsClient(urlRequest.Host);
            networkStream.ReadTimeout = 2000;
            var builder = new StringBuilder();
            builder.AppendLine($"{Method} {urlRequest.AbsolutePath} HTTP/1.1");
            builder.AppendLine($"Host: {urlRequest.Host}");

            if (Headers != null)
            {
                builder.AppendLine($"Accept: {Headers.Accept}");
                builder.AppendLine($"Content-Type: {Headers.ContentType}");
                builder.AppendLine($"User-Agent: {Headers.UserAgent}");
                if (Headers.Referer != String.Empty)
                {
                    builder.AppendLine($"Referer: {Headers.Referer}");
                }
                if (Headers.Origin != String.Empty)
                {
                    builder.AppendLine($"Origin: {Headers.Origin}");
                }
                builder.AppendLine($"Accept-Language: {Headers.AcceptLanguage}");
                builder.AppendLine($"Accept-Encoding: {Headers.AcceptEncoding}");
                builder.Append(Headers.headersHandler);
            }

           if (Body != null)
            {
                builder.AppendLine("Content-Length: " + Body.Length);
            }
            else
            {
                builder.AppendLine("Content-Length: 0");
            }

            if (KeepAlive)
            {
                builder.AppendLine("Connection: keep-alive");
            }
            else
            {
                builder.AppendLine("Connection: close");
            }
            builder.AppendLine();
           if (Body != null)
            {
                builder.AppendLine(Body);
            }
            var reader = new StreamReader(networkStream, Encoding.UTF8);
            Request = builder.ToString();
            byte[] bytes = Encoding.UTF8.GetBytes(Request);
            networkStream.WriteAsync(bytes, 0, bytes.Length);
            Response = reader.ReadToEnd();
            networkStream.Dispose();
            reader.Dispose();
            return Response;
        }

        public void Dispose()
        { 
            client.Dispose();
        }
    }
    public class Headers
    {
        public StringBuilder headersHandler = new StringBuilder();
        public string Accept = "*/*";
        public string ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
        public string UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.4844.74 Safari/537.36";
        public string Referer = String.Empty;
        public string Origin = String.Empty;
        public string AcceptLanguage = "en-US,en;";
        public string AcceptEncoding = "gzip, deflate, br";
        private string[] BlockedHeaders = { "content-type", "user-agent","accept","referer","origin","accept-language","acceptencoding" };
        public void Add(string Name, string Value)
        {
            if (BlockedHeaders.Contains(Name.ToLower())) {
                throw new ArgumentNullException($"Blocked header, you need to add it like this ({Name.ToLower()}.{Name})");
            }
            headersHandler.AppendLine($"{Name}: {Value}");
        }
        public void Clear()
        {
            headersHandler.Clear();
        }
    }
}
