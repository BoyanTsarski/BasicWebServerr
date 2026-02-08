using System.Net;
using System.Net.Sockets;
using System.Text;

namespace BasicWebServer.Server
{
    public class HttpServer
    {
        private readonly IPAddress ipAddress;
        private readonly int port;
        private readonly TcpListener tcpListener;

        public HttpServer(string ipAddress, int port)
        {
            this.ipAddress = IPAddress.Parse(ipAddress);
            this.port = port;
            this.tcpListener = new TcpListener(this.ipAddress, this.port);
        }

        public void Start()
        {
            tcpListener.Start();
            Console.WriteLine($"Server started on http://{ipAddress}:{port}");
            Console.WriteLine("Listening for requests...");

            while (true)
            {
                TcpClient client = tcpListener.AcceptTcpClient();
                NetworkStream networkStream = client.GetStream();

                string requestText = ReadRequest(networkStream);

                Console.WriteLine(requestText);

                WriteResponse(networkStream, "Hello from the HTTP Server!");

                client.Close();
            }
        }

        private string ReadRequest(NetworkStream networkStream)
        {
            byte[] buffer = new byte[1024];
            var builder = new StringBuilder();
            int totalBytes = 0;
            const int maxRequestSize = 10 * 1024;
            int bytesRead;

            do
            {
                bytesRead = networkStream.Read(buffer, 0, buffer.Length);
                totalBytes += bytesRead;

                if (totalBytes > maxRequestSize)
                    throw new InvalidOperationException("Request too large.");

                builder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));

            } while (networkStream.DataAvailable);

            return builder.ToString();
        }

        private void WriteResponse(NetworkStream networkStream, string message)
        {
            string response =
                "HTTP/1.1 200 OK\r\n" +
                "Content-Type: text/plain; charset=UTF-8\r\n" +
                $"Content-Length: {Encoding.UTF8.GetByteCount(message)}\r\n" +
                "\r\n" +
                message;

            byte[] responseBytes = Encoding.UTF8.GetBytes(response);

            networkStream.Write(responseBytes, 0, responseBytes.Length);
        }
    }
}