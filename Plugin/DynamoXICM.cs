using System.Diagnostics;
using System.IO.Pipes;
using Dynamo.Extensions;
using Dynamo.Logging;


namespace DynamoXICM
{
    public class DynamoXICM : IExtension, ILogSource
    {
        #region ILogSource implementation

        public event Action<ILogMessage> MessageLogged;

        private void Log(ILogMessage obj)
        {
            if (MessageLogged != null)
            {
                MessageLogged(obj);
            }
        }

        public void Log(string message)
        {
            Log(LogMessage.Info(message));
        }
        #endregion


        public void Dispose()
        {
        }

        public void Startup(StartupParams sp)
        {

            Log("Starting up + " + Name);

            string pipeName = "ICMNamedPipeServer";
            //Get path of the current dll where this code exists
            string? path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            Log(path + "\\Exchange\\run_exchange.bat");
            StartExchange();

            // Create the named pipe server
            NamedPipeServerStream pipeServer = new NamedPipeServerStream(pipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Message);
            SetPipeServer(pipeServer);
            //Print name of the pipe
            Log("Pipe name: " + pipeServer);
            // Wait for a client to connect
            Log("Waiting for client connection...");
            GetPipeServer.WaitForConnection();
            Log("Client connected!");
        }
        public void Ready(ReadyParams rp)
        {

        }

        public void Shutdown()
        {
            Log("Shutting down " + Name);
            //Write exit command to the pipe
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes("exit\n");
            GetPipeServer.Write(buffer, 0, buffer.Length);
            GetPipeServer.Flush();

            if (_exchangeProcess != null)
            {
                _exchangeProcess.Kill();
            }
            if (_pipeServer != null)
            {
                _pipeServer.Close();
            }
        }

        public string Name
        {
            get
            {
                return "DynamoXICM";
            }
        }
        public string UniqueId
        {
            get
            {
                return Guid.NewGuid().ToString();
            }
        }
        //Create attribute to store named pipe server instance which will be accessed by other classes as well
        private static NamedPipeServerStream? _pipeServer;

        //Create an attribute to store the process instance which will be created to run the exchange
        private static Process? _exchangeProcess;

        //Create a method to set the _pipeServer attribute which can be used from the current class only
        public static void SetPipeServer(NamedPipeServerStream pipeServer)
        {
            if (_pipeServer == null)
            {
                _pipeServer = pipeServer;
            }
            _pipeServer = pipeServer;
        }
        //Create a method to get the _pipeServer attribute which can be used from the any class
        public static NamedPipeServerStream GetPipeServer
        {
            get
            {
                return _pipeServer;
            }
        }

        //Below method will be used to start the exchange process
        public static void StartExchange()
        {
            //Get path of the current dll where this code exists
            string? path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
            string runExchange = Path.Combine(path, "Exchange\\run_exchange.bat");
            _exchangeProcess = Process.Start(runExchange);
        }

    }
}
