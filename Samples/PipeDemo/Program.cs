using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PipeDemo
{
    public class PipeClient
    {
        const string SUBMISSION_ID = "{C6E41045-5CA6-4287-9BEF-3F98FFCFCC63}";

        private static int numClients = 1;

        public static void Main(string[] Args)
        {
            if (Args.Length > 0)
            {
                if (Args[0] == "spawnclient")
                {
                    var pipeClient = new NamedPipeClientStream(".", "testpipe", PipeDirection.InOut, PipeOptions.None, TokenImpersonationLevel.Impersonation);

                    Console.WriteLine("Connecting to server ...\n");
                    pipeClient.Connect();

                    var ss = new StreamString(pipeClient);

                    // validate the server's signature string
                    if (ss.ReadString() == "I am the one true server!")
                    {
                        // the client security token is sent with the first write.
                        // send the name of the file whose contents are returned by the server
                        ss.WriteString(SUBMISSION_ID);

                        // print the file to the screen
                        Console.Write("Submission id was sent to server.");
                    }
                    else
                    {
                        Console.WriteLine("Server could not be verified.");
                    }

                    pipeClient.Close();
                    // give client process some time to display results before exiting.
                    Thread.Sleep(4000);
                }
            }
            else
            {
                Console.WriteLine("\n*** Named pipe client stream with impersonation example ***\n"); 
                StartClients();
            }
        }



        // Helper function to create pipe client processes
        private static void StartClients()
        {
            int i;
            string currentProcessName = Environment.CommandLine;
            Process[] plist = new Process[numClients];

            Console.WriteLine("Spawning client processes...\n");

            if (currentProcessName.Contains(Environment.CurrentDirectory))
            {
                currentProcessName = currentProcessName.Replace(Environment.CurrentDirectory, String.Empty);
            }

            // Remove extra characters when launched from Visual Studio
            currentProcessName = currentProcessName.Replace("\\", String.Empty);
            currentProcessName = currentProcessName.Replace("\"", String.Empty);

            for (i = 0; i < numClients; i++)
            {
                // Start 'this' program but spawn a named pipe client.
                plist[i] = Process.Start(currentProcessName, "spawnclient");

                Thread.Sleep(250);
            }

            while (i > 0)
            {
                for (int j = 0; j < numClients; j++)
                {
                    if (plist[j] != null)
                    {
                        if (plist[j].HasExited)
                        {
                            Console.WriteLine("Client process[{0}] has exited.", plist[j].Id);
                            plist[j] = null;
                            i--;    // decrement the process watch count
                        }
                        else
                        {
                            Thread.Sleep(250);
                        }
                    }
                }
            }
            Console.WriteLine("\nClient processes finished, exiting.");

            Thread.Sleep(5000);
        }
    }


    // Defines the data protocol for reading and writing strings on our stream
    public class StreamString
    {
        private Stream ioStream;
        private UnicodeEncoding streamEncoding;

        public StreamString(Stream ioStream)
        {
            this.ioStream = ioStream;
            streamEncoding = new UnicodeEncoding();
        }

        public string ReadString()
        {
            int len;
            len = ioStream.ReadByte() * 256;
            len += ioStream.ReadByte();
            byte[] inBuffer = new byte[len];
            ioStream.Read(inBuffer, 0, len);

            return streamEncoding.GetString(inBuffer);
        }

        public int WriteString(string outString)
        {
            byte[] outBuffer = streamEncoding.GetBytes(outString);
            int len = outBuffer.Length;
            if (len > UInt16.MaxValue)
            {
                len = (int)UInt16.MaxValue;
            }
            ioStream.WriteByte((byte)(len / 256));
            ioStream.WriteByte((byte)(len & 255));
            ioStream.Write(outBuffer, 0, len);
            ioStream.Flush();

            return outBuffer.Length + 2;
        }
    }



    
}
