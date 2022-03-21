Imports System
Imports TCPRequest

Module Program
    Sub Main(args As String())
        Dim http As New HttpRequest
        Dim head As New Headers
        head.UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/99.0.4844.51 Safari/537.36"
        head.ContentType = "application/x-www-form-urlencoded"
        head.Add("x-csrftoken", "missing")
        Dim response As New String(http.Post("https://i.instagram.com/api/v1/accounts/username_suggestions/", head, "name=_824", Proxy.HttpsProxy("127.0.0.1:80")))
        Console.WriteLine(response)
        Console.ReadLine()
    End Sub
End Module
