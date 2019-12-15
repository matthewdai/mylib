using IronPython.Hosting;
using Microsoft.Scripting.Hosting;
using System;
using System.Collections.Generic;
using System.IO;

namespace CallPythonFromCSharp
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = PatchParameter("dsf", 2);

            Console.WriteLine("Python result: " + s.ToString());
        }

        public static int PatchParameter(string parameter, int serviceid)
        {
            var engine = Python.CreateEngine(); // Extract Python language engine from their grasp
            var scope = engine.CreateScope(); // Introduce Python namespace (scope)
            var d = new Dictionary<string, object>
            {
                { "serviceid", serviceid},
                { "parameter", parameter},
                { "fp", 5 }
            }; // Add some sample parameters. Notice that there is no need in specifically setting the object type, interpreter will do that part for us in the script properly with high probability

            scope.SetVariable("params", d); // This will be the name of the dictionary in python script, initialized with previously created .NET Dictionary
            //ScriptSource source = engine.CreateScriptSourceFromFile("PATH_TO_PYTHON_SCRIPT_FILE"); // Load the script
            //ScriptSource source = engine.CreateScriptSourceFromFile(@"C:\MD\github\mylib\Samples\CallPythonFromCSharp\Scripts\Simple.py"); // Load the script

            var script = File.ReadAllText("../../../Scripts/Simple.py");
            ScriptSource source = engine.CreateScriptSourceFromString(script); // Load the script
            object result = source.Execute(scope);

            //parameter = scope.GetVariable<string>("parameter"); // To get the finally set variable 'parameter' from the python script
            var simpleResult = scope.GetVariable<int>("result");

            return simpleResult;
        }
    }
}
