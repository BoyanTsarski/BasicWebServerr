using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicWebServer.Server.HTTP
{
    public class Request
    {
        public Method Method { get; private set; }
        public string Url { get; private set; }
        public HeaderCollection Headers { get; private set; }
        public string Body { get; private set; }

        public static Request Parse(string requestText)
        {
            var lines = requestText.Split("\r\n");

            var startLine = lines[0].Split(' ');

            var method = ParseMethod(startLine[0]);
            var url = startLine[1];

            var emptyLineIndex = Array.IndexOf(lines, "");

            var headerLines = lines.Skip(1).Take(emptyLineIndex - 1);
            var headers = ParseHeaders(headerLines);

            var bodyLines = lines.Skip(emptyLineIndex + 1);
            var body = string.Join("\r\n", bodyLines);

            return new Request
            {
                Method = method,
                Url = url,
                Headers = headers,
                Body = body
            };
        }

        private static Method ParseMethod(string method)
        {
            if (!Enum.TryParse(method, true, out Method parsed))
                throw new InvalidOperationException("Invalid HTTP method.");

            return parsed;
        }

        private static HeaderCollection ParseHeaders(System.Collections.Generic.IEnumerable<string> headerLines)
        {
            var collection = new HeaderCollection();

            foreach (var line in headerLines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(": ", 2);
                if (parts.Length != 2)
                    continue;

                collection.Add(parts[0], parts[1]);
            }

            return collection;
        }
    }
}