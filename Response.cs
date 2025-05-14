using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace DemoTerminalPlugin
{
    internal class Response
    {
        public string TransactionId { get; set; } = "";

        [JsonConverter(typeof(StringEnumConverter))]
        public ResponseType Type { get; set; }
        public decimal ApprovedAmount { get; set; }
        public string[]? ReceiptLines { get; set; }
        public string CardBrand { get; set; } = "";
        public string MaskedCardNumber { get; set; } = "";
        public string UIMessage { get; set; } = "";
    }
}
