using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DemoTerminalPlugin
{
    internal class Response
    {
        public string Id { get; set; } = "";
        public string PSPReference { get; set; } = "";
        public string PSPErrorCode { get; set; } = "";

        [JsonConverter(typeof(StringEnumConverter))]
        public ResponseType Type { get; set; }
        public decimal ApprovedAmount { get; set; }
        public string[]? ReceiptLines { get; set; }
        public string CardType { get; set; } = "";
        public string MaskedCardNumber { get; set; } = "";
        public string Currency { get; set; } = "";
        public string ApplicationID { get; set; } = "";
        public string UIMessage { get; set; } = "";
        public bool Success { get; set; }
    }
}
