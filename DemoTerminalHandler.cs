using HardwareConnectorPlugin;
using Serilog;

namespace DemoTerminalPlugin
{
    public class DemoTerminalHandler : IPluginHandler
    {
        string IPluginHandler.Name => "DemoTerminal";
        bool IPluginHandler.IsThreadSafe => false; //if set to true, hardware connector allows calling your handler concurrently. Only do this if you need it for your integration.

        Task IPluginHandler.InvokeAsync(HandlerContent handlerContent, IPluginSocketResponseManager socketResponseManager, string context, ILogger logger, CancellationToken cancellationToken)
        {
            var request = handlerContent.DeserializeHandlerContent<Request>();
            Log.Logger = logger;

            switch (request.Type)
            {
                case RequestType.Transaction:
                    DemoTerminalInstance.StartTransaction(request, socketResponseManager);
                    break;
                case RequestType.Abort:
                    DemoTerminalInstance.AbortTransaction(request, socketResponseManager);
                    break;
            }

            return Task.CompletedTask;
        }

        public Task Finalize()
        {
            // if the terminal SDK needs to be cleaned up, you can add code here. It will run when hardware connector is closing down, but it only gets 5 seconds before process is killed.
            return Task.CompletedTask;
        }
    }
}
