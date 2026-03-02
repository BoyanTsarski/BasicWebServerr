using BasicWebServer.Server.HTTP;
using BasicWebServer.Server.HTTP.BasicWebServer.Server.HTTP;
using BasicWebServer.Server.Responses;

namespace BasicWebServer.Server.Routing
{
    public interface IRoutingTable
    {
        IRoutingTable Map(Method method,
              string path,
    Func<Request, Response> responseFunction);

        IRoutingTable MapGet(string path, Func<Request, Response> responseFunction);

        IRoutingTable MapPost(string path, Func<Request, Response> responseFunction);
    }
}
