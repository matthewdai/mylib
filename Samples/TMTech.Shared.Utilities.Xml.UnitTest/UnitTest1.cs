using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;
using System.Xml;

namespace TMTech.Shared.Utilities.Xml.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        private A mData = new A()
        {
            ID = 999,
            Name = "test name",
            Enabled = true,
            Data = new D()
            {
                Desc = "sdf sd",
                Amount = 123.45,
            },

        };



        [TestMethod]
        public void Test_ToXml()
        {

            string gg = WriteHelper.CreateXmlText(mData);

            //Console.WriteLine(gg);
            Trace.WriteLine(gg);
            //System.Diagnostics.Debug.WriteLine(gg);
        }



        [TestMethod]
        public void Test_FromXml()
        {
            // get xml string 
            string xmlString = WriteHelper.CreateXmlText(mData);

            var xml = new XmlDocument();
            xml.LoadXml(xmlString);

            var objBuilder = new ObjectBuilder();
            objBuilder.RegisterAssembly(this.GetType().Assembly);

            // back to object
            object obj = objBuilder.CreateObjectFromXml(xml.DocumentElement);

            Assert.AreEqual(obj != null, true);

            var a = obj as A;

            Assert.AreEqual(a != null, true);
            Assert.AreEqual(a.ID, 999);
            Assert.AreEqual(a.Data != null, true);
            Assert.AreEqual(a.Data.Amount, 123.45);
            Assert.AreEqual(a.Enabled, true);

        }

    }




    public class A
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public D Data { get; set; }
        public bool Enabled { get; set; }

    }


    public class D
    {
        public string Desc { get; set; }

        public double Amount { get; set; }
    }

}
