using HttpClientManager.Interfaces;
using System.Collections.Generic;

namespace HttpClientManager.Implementation
{
    public class RequestHeaders : IRequestHeaders
    {
        public string UserAgent { get; set; } = "Custom";

        public string Authorization { get; set; } = string.Empty;

        public List<KeyValuePair<string, string>> CustomHeaders { get; set; }
    }
}
