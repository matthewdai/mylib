using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MutexDemo
{
    public class Shared
    {
        // Pipe server class to listen request for openning submissions.
        public static PipeServer _PipeServer;

        public static MainWindow _MainWindow;
    }
}
