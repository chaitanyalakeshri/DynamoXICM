using System.IO.Pipes;
using System.IO;
using System.Xml.Linq;
using System.Diagnostics;
using ICMNodes;
using DynamoXICM;

class Program()
{

    static void Main()
    {
        Log("Starting up + ");

        string pipeName = "ICMNamedPipeServer";
        //Get path of the current dll where this code exists
        string? path = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location);
        Log(path + "\\Exchange\\run_exchange.bat");
        DynamoXICM.DynamoXICM.StartExchange();

        // Create the named pipe server
        NamedPipeServerStream pipeServer = new NamedPipeServerStream(pipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Message);
        DynamoXICM.DynamoXICM.SetPipeServer(pipeServer);
        //Print name of the pipe
        Log("Pipe name: " + pipeServer);
        // Wait for a client to connect
        Log("Waiting for client connection...");
        DynamoXICM.DynamoXICM.GetPipeServer.WaitForConnection();
        Log("Client connected!");

        string dbName = "snumbat://localhost:40000/Databases/DynamoTest";
        string[] args = { dbName };

        object response = ExecuteFunction.Execute("open_database", args);
        Console.WriteLine("Program Complete");
    }
    public static void Log(string message)
    {
        Console.WriteLine(message);
    }

}