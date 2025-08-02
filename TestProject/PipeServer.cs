//using System;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.IO.Pipes;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace TestProject
//{
//    internal class PipeServer
//    {
//        //Create attribute to store named pipe server instance which will be accessed by other classes as well
//        private static NamedPipeServerStream? _pipeServer;


//        //Create a method to set the _pipeServer attribute which can be used from the current class only
//        private static void SetPipeServer()
//        {
//            if (_pipeServer == null)
//            {
//                string pipeName = "ICMNamedPipeServer";
//                //Create the named pipe server
//                NamedPipeServerStream pipeServer = new NamedPipeServerStream(pipeName, PipeDirection.InOut, 1, PipeTransmissionMode.Message);
//                _pipeServer = pipeServer;
//            }
//        }
//        //Create a method to get the _pipeServer attribute which can be used from the any class
//        public static NamedPipeServerStream GetPipeServer
//        {
//            get
//            {
//                return _pipeServer;
//            }
//        }
//    }
//}
