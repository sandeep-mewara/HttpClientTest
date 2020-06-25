using System.Collections.Generic;

namespace HttpClientManager.Interfaces
{
    public interface IRequestHeaders
    {
        string UserAgent { get; set; }
        string Authorization { get; set; }
        List<KeyValuePair<string, string>> CustomHeaders { get; set; }
    }
}
