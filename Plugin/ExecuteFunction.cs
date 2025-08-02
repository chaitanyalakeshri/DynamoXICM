using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WSClass;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DynamoXICM
{
    public class ExecuteFunction
    {
        /// <summary>
        /// Executes a function on the server by sending a JSON request and receiving a JSON response.
        /// </summary>
        /// <param name="functionName">The name of the function to execute.</param>
        /// <param name="args">The arguments to pass to the function.</param>
        /// <returns>The response from the server as a string.</returns>
        public static object Execute(string functionName, string[] args)
        {
            if (!System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
            {
                return "Platform not supported. Windows 11 is a supported platform";
            }

            string jsonRequest = CreateJsonRequest(functionName, args);
            WriteToServer(jsonRequest);
            string response = ReadFromServer();
            return ParseResponse(response);
        }

        /// <summary>
        /// Creates a JSON request string with the function name and arguments.
        /// </summary>
        /// <param name="functionName">The name of the function to execute.</param>
        /// <param name="args">The arguments to pass to the function.</param>
        /// <returns>The JSON request string with an appended delimiter.</returns>
        private static string CreateJsonRequest(string functionName, string[] args)
        {
            var dict = new Dictionary<string, object>
                {
                    { "function", functionName },
                    { "args", args }
                };
            string jsonString = JsonSerializer.Serialize(dict);
            jsonString += "\0END\0";
            return jsonString;
        }

        /// <summary>
        /// Writes the JSON request string to the server.
        /// </summary>
        /// <param name="jsonRequest">The JSON request string to send to the server.</param>
        private static void WriteToServer(string jsonRequest)
        {
            DynamoXICM.GetPipeServer.Write(Encoding.UTF8.GetBytes(jsonRequest));
            DynamoXICM.GetPipeServer.Flush();
        }

        /// <summary>
        /// Reads the response from the server, handling responses larger than the buffer size.
        /// </summary>
        /// <returns>The response string from the server without the delimiter.</returns>
        private static string ReadFromServer()
        {
            byte[] buffer = new byte[1024];
            StringBuilder responseBuilder = new StringBuilder();
            int bytesRead;
            string endDelimiter = "\0END\0";

            do
            {
                bytesRead = DynamoXICM.GetPipeServer.Read(buffer, 0, buffer.Length);
                responseBuilder.Append(Encoding.UTF8.GetString(buffer, 0, bytesRead));
            } while (!responseBuilder.ToString().Contains(endDelimiter));

            string response = responseBuilder.ToString();
            //Remove end delimiter from the response

            return response.Substring(0, response.Length - endDelimiter.Length);
        }

        /// <summary>
        /// Parses the JSON response string and extracts the response property.
        /// </summary>
        /// <param name="response">The JSON response string from the server.</param>
        /// <returns>The extracted response property as a string.</returns>
        private static object ParseResponse(string response)
        {
            JsonDocument json = JsonDocument.Parse(response);
            JsonElement data = json.RootElement.GetProperty("Data");
            JsonElement dataType = json.RootElement.GetProperty("DataType");

            // Ensure dataType.GetString() is not null before calling Type.GetType
            string? dataTypeString = dataType.GetString();
            if (string.IsNullOrEmpty(dataTypeString))
            {
                throw new InvalidOperationException("Null or empty data type received from Exchange.");
            }

            // Handle standard .NET types
            Type? targetType = Type.GetType(dataTypeString, throwOnError: false);
            if (targetType == null)
            {
                // Dynamically resolve the assembly containing the type in WSClass namespace
                Assembly? wsClassAssembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(assembly => assembly.GetType($"WSClass.{dataTypeString}") != null);

                if (wsClassAssembly == null)
                {
                    throw new InvalidOperationException($"Unable to find assembly containing type: WSClass.{dataTypeString}");
                }

                targetType = wsClassAssembly.GetType($"WSClass.{dataTypeString}");
                if (targetType == null)
                {
                    throw new InvalidOperationException($"Unable to resolve type: WSClass.{dataTypeString}");
                }
            }

            // Deserialize the data to the resolved type
            var deserializedObject = JsonSerializer.Deserialize(data.ToString(), targetType);

            // Throw an exception if the deserialized object is a WSError
            if (deserializedObject is WSError error)
            {
                throw new InvalidOperationException($"Error from server: {error.Message}");
            }

            return deserializedObject;
        }
    }
}
