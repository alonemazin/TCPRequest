
# TCPRequest

A simple library to make some request directly to (TCP Client) in easy way
writed with C# in .NET CORE 6


## Features

- Easy & simple to use
- Fast in make requests
- Support Multi-Types of proxies (HTTPS, SOCKS4, SOCKS4a, SOCKS5)


## Examples

How to make GET in C#
```csharp
TCPRequest.HttpRequest request = new TCPRequest.HttpRequest();
TCPRequest.Headers headers = new TCPRequest.Headers();
headers.Accept = "*/*";
headers.UserAgent = "Chrome/51.0.2704.103 Safari/537.36";
headers.ContentType = "application/json";
string response = request.Get("https://alonemazin.online/", headers);
```

How to make POST in C#
```csharp
TCPRequest.HttpRequest request = new TCPRequest.HttpRequest();
TCPRequest.Headers headers = new TCPRequest.Headers();
headers.Accept = "*/*";
headers.UserAgent = "Chrome/51.0.2704.103 Safari/537.36";
headers.ContentType = "application/json";
string response = request.Post("https://alonemazin.online/", headers, data);
```

How to make GET in VB.net
```vb
Dim request As TCPRequest.HttpRequest = New TCPRequest.HttpRequest()
Dim headers As TCPRequest.Headers = New TCPRequest.Headers()
headers.Accept = "*/*"
headers.UserAgent = "Chrome/51.0.2704.103 Safari/537.36"
headers.ContentType = "application/json"
Dim response As String = request.[Get]("https://alonemazin.online/", headers)
```

How to make POST in VB.net
```vb
Dim request As TCPRequest.HttpRequest = New TCPRequest.HttpRequest()
Dim headers As TCPRequest.Headers = New TCPRequest.Headers()
headers.Accept = "*/*"
headers.UserAgent = "Chrome/51.0.2704.103 Safari/537.36"
headers.ContentType = "application/json"
Dim response As String = request.Post("https://alonemazin.online/", headers, data)
```
Use Proxies in POST requests
```csharp
TCPRequest.HttpRequest request = new TCPRequest.HttpRequest();
TCPRequest.Headers headers = new TCPRequest.Headers();
headers.Accept = "*/*";
headers.UserAgent = "Chrome/51.0.2704.103 Safari/537.36";
headers.ContentType = "application/json";

// Https
string response = request.Get("https://alonemazin.online/", headers, Data, TCPRequest.Proxy.HttpsProxy("ip:port"));

// Socks4
string response = request.Get("https://alonemazin.online/", headers, Data, TCPRequest.Proxy.Socks4Proxy("ip:port"));

// Socks4a
string response = request.Get("https://alonemazin.online/", headers, Data, TCPRequest.Proxy.Socks4aProxy("ip:port"));

// Socks5
string response = request.Get("https://alonemazin.online/", headers, Data, TCPRequest.Proxy.Socks5Proxy("ip:port"));
```

Use Proxies in GET requests
```csharp
TCPRequest.HttpRequest request = new TCPRequest.HttpRequest();
TCPRequest.Headers headers = new TCPRequest.Headers();
headers.Accept = "*/*";
headers.UserAgent = "Chrome/51.0.2704.103 Safari/537.36";
headers.ContentType = "application/json";

// Https
string response = request.Get("https://alonemazin.online/", headers, TCPRequest.Proxy.HttpsProxy("ip:port"));

// Socks4
string response = request.Get("https://alonemazin.online/", headers, TCPRequest.Proxy.Socks4Proxy("ip:port"));

// Socks4a
string response = request.Get("https://alonemazin.online/", headers, TCPRequest.Proxy.Socks4aProxy("ip:port"));

// Socks5
string response = request.Get("https://alonemazin.online/", headers, TCPRequest.Proxy.Socks5Proxy("ip:port"));
```
How To Add Headers C#
```csharp
TCPRequest.HttpRequest request = new TCPRequest.HttpRequest();
TCPRequest.Headers headers = new TCPRequest.Headers();
headers.Accept = "*/*";
headers.UserAgent = "Chrome/51.0.2704.103 Safari/537.36";
headers.ContentType = "application/json";
headers.add("name" , "value");
string response = request.Post("https://alonemazin.online/", headers, data);
```
How To Add Headers VB.net
```vb
Dim request As TCPRequest.HttpRequest = New TCPRequest.HttpRequest()
Dim headers As TCPRequest.Headers = New TCPRequest.Headers()
headers.Accept = "*/*"
headers.UserAgent = "Chrome/51.0.2704.103 Safari/537.36"
headers.ContentType = "application/json"
headers.add("name" , "value")
Dim response As String = request.Post("https://alonemazin.online/", headers, data)
```
## FAQ

#### Is it faster than the regular libraries?

Yp (:

#### Is the library finished?

Not yet, it's on progress, we'll make it better over time

## Authors

- [@alonemazin](https://www.instagram.com/alonemazin/)
- [@_824](https://www.instagram.com/_824/)
