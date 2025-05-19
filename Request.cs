using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DemoTerminalPlugin
{
    internal class Request
    {
        public string TransactionId { get; set; } = "";

        [JsonConverter(typeof(StringEnumConverter))]
        public RequestType Type { get; set; }
        public decimal Amount { get; set; }
        public string Currency { get; set; } = "";
        public string TerminalIP { get; set; } = "";
    }
}
