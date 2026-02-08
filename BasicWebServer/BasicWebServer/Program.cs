using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        IPAddress ipAddress = IPAddress.Loopback; 
        int port = 8080; 

        TcpListener listener = new TcpListener(ipAddress, port);
        listener.Start();
        Console.WriteLine($"Server started at http://{ipAddress}:{port}");

        while (true)
        {
            Console.WriteLine("Waiting for a connection...");

            TcpClient client = listener.AcceptTcpClient();
            Console.WriteLine("Client connected!");

            NetworkStream stream = client.GetStream();

            byte[] buffer = new byte[1024];
            int bytesRead = stream.Read(buffer, 0, buffer.Length);
            string request = Encoding.UTF8.GetString(buffer, 0, bytesRead);
            Console.WriteLine("Received request:");
            Console.WriteLine(request);

            string message = "Hello from SoftUni Server!";
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);

            string response = "HTTP/1.1 200 OK\r\n" +
                              $"Content-Length: {messageBytes.Length}\r\n" +
                              "Content-Type: text/plain; charset=UTF-8\r\n\r\n" +
                              message;

            byte[] responseBytes = Encoding.UTF8.GetBytes(response);

            stream.Write(responseBytes, 0, responseBytes.Length);
            Console.WriteLine("Response sent to client.");

            client.Close();
        }
    }
}
