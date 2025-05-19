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
                // Initialize terminal .dll here and open terminal connection based on Request.TerminalIP if needed.
                // As this class is static it can keep an instance of a terminal SDK/.dll alive all day after connection is estasblished in the morning on first request.
                _initialized = true;
            }

            Log.Information($"Starting transaction, id: {request.TransactionId}, amount: {request.Amount}"); //Logs are written in %localappdata%\NPHardwareConnector\logs\

            // Invoke terminal .dll to start purchase here, setup event listeners etc.

            // Send UI updates from terminal .dll back to POS Action as you receive them.
            pluginSocketResponseManager.InvokeSocket(JObject.FromObject(new Response()
            {
                Id = request.TransactionId,
                Type = ResponseType.UIUpdate,
                UIMessage = "Processing..."
            }));

            var success = request.Amount != 666;

            // When transaction is done, build final response with error or approved amount and send to POS Action
            pluginSocketResponseManager.InvokeSocket(JObject.FromObject(new Response()
            {
                Id = request.TransactionId,
                PSPReference = "1234567890",
                Type = ResponseType.Transaction,
                CardType = "VISA",
                MaskedCardNumber = "487145XXXXXX1234",
                ApplicationID = "A0000001211010",
                ReceiptLines = ["receiptLine1", "receiptLine2", "receiptLine3"],
                Success = success,
                PSPErrorCode = success ? "" : "WRONG_PIN",
                ApprovedAmount = success ? request.Amount : 0,
                Currency = request.Currency
            }));
        }

        public static void AbortTransaction(Request request, IPluginSocketResponseManager pluginSocketResponseManager)
        {
            Log.Information($"Aborting transaction, id: {request.TransactionId}, amount: {request.Amount}");

            // Invoke terminal .dll to abort any active transaction here.
        }
    }
}
