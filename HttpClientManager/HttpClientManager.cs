using System;
using System.Net;
using System.Net.Http;
using System.Text;

namespace HttpClientManager.Interfaces
{
    public class HttpClientManager : IHttpClientManager
    {
        private const string USERAGENT = "User-Agent";
        private const string AUTHORIZATION = "Authorization";
        private const string MEDIATYPE_JSON = "application/json";
        private readonly Encoding ENCODING = Encoding.UTF8;

        private readonly HttpClient _httpClient;

        public HttpClientManager() : this(new HttpClientHandler())
        {
            ServicePointManager.DefaultConnectionLimit = 48;
        }

        public HttpClientManager(HttpMessageHandler messageHandler)
        {
            _httpClient = new HttpClient(messageHandler);
        }

        private HttpRequestMessage SetupRequest(IRequestPayload requestPayload)
        {
            var request = new HttpRequestMessage
            {
                RequestUri = new Uri(requestPayload.Url)
            };

            switch (requestPayload.RequestType)
            {
                case RequestType.POST_ASYNC:
                    request.Method = HttpMethod.Post;
                    request.Content = GetHttpContent(requestPayload.ContentJson);
                    break;
                case RequestType.PUT_ASYNC:
                    request.Method = HttpMethod.Put;
                    request.Content = GetHttpContent(requestPayload.ContentJson);
                    break;
                case RequestType.DELETE_ASYNC:
                    request.Method = HttpMethod.Delete;
                    break;
                case RequestType.GET_ASYNC:
                    request.Method = HttpMethod.Get;
                    break;
                default:
                    request.Method = HttpMethod.Get;
                    break;
            }

            if (requestPayload.Headers == null)
                return request;

            request.Headers.Add(USERAGENT, requestPayload.Headers.UserAgent);
            request.Headers.Add(AUTHORIZATION, requestPayload.Headers.Authorization);


            if (requestPayload.Headers.CustomHeaders == null)
                return request;

            foreach (var customerHeader in requestPayload.Headers.CustomHeaders)
            {
                request.Headers.Add(customerHeader.Key, customerHeader.Value);
            }
            return request;
        }

        public HttpResponseMessage ExecuteRequest(IRequestPayload requestPayload)
        {
            HttpRequestMessage httpRequestMessage = SetupRequest(requestPayload);

            HttpResponseMessage httpResponseMessage = _httpClient.SendAsync(httpRequestMessage, HttpCompletionOption.ResponseHeadersRead).Result;

            return httpResponseMessage;
        }

        private HttpContent GetHttpContent(string contentJson)
        {
            return new StringContent(contentJson, ENCODING, MEDIATYPE_JSON);
        }
    }
}
