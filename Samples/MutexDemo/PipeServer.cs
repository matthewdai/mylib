using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MutexDemo
{
    public class PipeServer
    {
        //private Thread _ServerThread;
        private NamedPipeServerStream _NamedPipeServerStream = null;

        private BackgroundWorker _BackgroundWorker;


        /// <summary>
        /// Constructor
        /// </summary>
        public PipeServer()
        {
            _BackgroundWorker = new BackgroundWorker();
            _BackgroundWorker.DoWork += _BackgroundWorker_DoWork;
            _BackgroundWorker.ProgressChanged += _BackgroundWorker_ProgressChanged;
            _BackgroundWorker.WorkerReportsProgress = true;
            _BackgroundWorker.RunWorkerAsync();

        }



        private void _BackgroundWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            var sid = e.UserState;
            if (sid != null) Shared._MainWindow.OpenSubmission(sid.ToString());
        }



        /// <summary>
        /// Background process for listening pipe clients
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _BackgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            _NamedPipeServerStream = new NamedPipeServerStream("testpipe", PipeDirection.InOut, 1, PipeTransmissionMode.Message, PipeOptions.Asynchronous);

            do
            {
                _NamedPipeServerStream.WaitForConnection();
                ProcessRequest();
                _NamedPipeServerStream.Disconnect();
            } while (true);    // loop until the application is shutdown.

        }



        /// <summary>
        /// Process the request from pipe client.
        /// </summary>
        private void ProcessRequest()
        {
            try
            {
                // Read the request from the client. Once the client has
                // written to the pipe its security token will be available.

                StreamString ss = new StreamString(_NamedPipeServerStream);

                // Verify our identity to the connected client using a
                // string that the client anticipates.


                ss.WriteString("I am the one true server!");
                string submissionID = ss.ReadString();

                Debug.WriteLine("submission ID: " + submissionID);
                _BackgroundWorker.ReportProgress(0, submissionID);


                // Read in the contents of the file while impersonating the client.
                //ReadFileToStream fileReader = new ReadFileToStream(ss, submissionID);

                // Display the name of the user we are impersonating.
                //Console.WriteLine("Reading file: {0} on thread[{1}] as user: {2}.", filename, threadId, pipeServer.GetImpersonationUserName());
                //pipeServer.RunAsClient(fileReader.Start);
      
            }
            // Catch the IOException that is raised if the pipe is broken
            // or disconnected.
            catch (IOException e)
            {
                Debug.WriteLine("ERROR: {0}", e.Message);
            }
            
        }

        
    }
}
