using ExcelDna.Integration;
using System.Threading;

namespace TMTech.FeedersModel
{
    public static class FeedersModelFunctions
    {
        [ExcelFunction(Description = "My first .NET function")]
        public static string SayHello(string name)
        {
            return "Hello " + name;
        }


        [ExcelFunction(Description = "Add two numbers")]
        public static double AddThem(double number1, double number2)
        {
            return number1 + number2 * 2;
        }


        [ExcelFunction(Description = "Feeder's Model Add function")]
        public static double FM_AddThem(double number1, double number2)
        {
            var ddd = new FeedersModelRibbon();
            return number1 + number2;
        }


        public static void AAA_RegisterSlave()
        {
            Thread.Sleep(1000);
        }

        public static void AAA_UnregisterSlave()
        {
            Thread.Sleep(1000);
        }


    }

}
