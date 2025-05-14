using HardwareConnectorPlugin;
using Newtonsoft.Json.Linq;
using Serilog;

namespace DemoTerminalPlugin
{
    internal static class DemoTerminalInstance
    {
        private static bool _initialized;

        public static void StartTransaction(Request request, IPluginSocketResponseManager pluginSocketResponseManager)
        {
            if (!_initialized)
            {
                // Initialize terminal .dll here
                // As this class is static it can keep an instance of a terminal SDK/.dll alive all day after connection is estasblished in the morning on first request.
                _initialized = true;
            }

            Log.Information($"Starting transaction, id: {request.TransactionId}, amount: {request.Amount}"); //Logs are written in %localappdata%\NPHardwareConnector\app-x.y.z\Logging\

            // Invoke terminal .dll to start purchase here, setup event listeners etc.

            // Send UI updates from terminal .dll back to POS Action as you receive them.
            pluginSocketResponseManager.InvokeSocket(JObject.FromObject(new Response()
            {
                TransactionId = request.TransactionId,
                Type = ResponseType.UIUpdate,
                UIMessage = "Processing..."
            }));

            // When transaction is done, build final response with error or approved amount and send to POS Action
            pluginSocketResponseManager.InvokeSocket(JObject.FromObject(new Response()
            {
                TransactionId = request.TransactionId,
                Type = ResponseType.Transaction,
                ApprovedAmount = request.Amount,
                CardBrand = "VISA",
                MaskedCardNumber = "4571XXXXXXXX1234",
                ReceiptLines = ["receiptLine1", "receiptLine2", "receiptLine3"]
            }));
        }

        public static void AbortTransaction(Request request, IPluginSocketResponseManager pluginSocketResponseManager)
        {
            Log.Information($"Aborting transaction, id: {request.TransactionId}, amount: {request.Amount}");

            // Invoke terminal .dll to abort any active transaction here.
        }
    }
}
