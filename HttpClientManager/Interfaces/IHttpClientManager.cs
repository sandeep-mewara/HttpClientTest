using System.Net.Http;

namespace HttpClientManager.Interfaces
{
    public interface IHttpClientManager
    {
        HttpResponseMessage ExecuteRequest(IRequestPayload requestPayload);
    }
}
