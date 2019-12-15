using System;
using System.Diagnostics;
using Python.Runtime;

namespace TMTech.Shared.CallPython
{
    public class PythonRunner
    {
        public static int GetSimplaeInt()
        {
            using (Py.GIL())
            {
                dynamic os = Py.Import("os");
                dynamic dir = os.listdir();
                var result = dir;
            }

            return 100;
        }
    }
}
