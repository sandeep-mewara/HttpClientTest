using HttpClientManager.Interfaces;

namespace HttpClientManager.Implementation
{
    public class RequestPayload : IRequestPayload
    {
        public IRequestHeaders Headers { get; set; }

        public string Url { get; set; } = string.Empty;

        public RequestType RequestType { get; set; } = RequestType.GET_ASYNC;

        public string ContentJson { get; set; } = string.Empty;
    }
}
