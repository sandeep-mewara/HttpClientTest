namespace HttpClientManager.Interfaces
{
    public interface IRequestPayload
    {
        IRequestHeaders Headers { get; set; }
        string Url { get; set; }
        RequestType RequestType { get; set; }
        string ContentJson { get; set; }
    }

    public enum RequestType
    {
        GET_ASYNC,
        POST_ASYNC,
        PUT_ASYNC,
        DELETE_ASYNC,
    }
}
