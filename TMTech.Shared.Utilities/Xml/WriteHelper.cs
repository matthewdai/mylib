using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace TMTech.Shared.Utilities.Xml
{


    /// <summary>
    /// An implementation of IDataRepository interface for testing purpose.
    /// </summary>
    public static class WriteHelper
    {


        /// <summary>
        /// Create xml string for object data
        /// </summary>
        /// <param name="dataObject"></param>
        /// <returns></returns>
        public static string CreateXmlText(object dataObject)
        {
            var stringWriter = new StringWriter();
            var xw = new XmlTextWriter(stringWriter);

            CreateXmlText(dataObject, xw);

            return stringWriter.ToString();
        }



        /// <summary>
        /// Create xml string for object data
        /// </summary>
        /// <param name="dataObject"></param>
        /// <returns></returns>
        public static void CreateXmlText(object dataObject, XmlTextWriter xw)
        {
            xw.WriteStartElement("object");
            xw.WriteAttributeString("type", dataObject.GetType().ToString());

            foreach (var pInfo in dataObject.GetType().GetProperties().Where(x => x.CanWrite))
            {
                WriteProperty(dataObject, pInfo, xw);
            }

            xw.WriteEndElement();               // object

        }



        public static void WriteProperty(object dataObject, string propertyName, XmlTextWriter xw)
        {
            // get property info 
            var pInfo = dataObject.GetType().GetProperty(propertyName);

            if (pInfo == null) return;

            WriteProperty(dataObject, pInfo, xw);
        }



        public static void WriteProperty(object dataObject, PropertyInfo pInfo, XmlTextWriter xw)
        {
            // only write writable properties
            if (!pInfo.CanWrite) return;

            // get value
            object pValue = pInfo.GetValue(dataObject);
            if (pValue == null) return;


            // start to write text

            xw.WriteStartElement("property");
            xw.WriteAttributeString("name", pInfo.Name);

            xw.WriteStartElement("value");

            Type t = pInfo.PropertyType;
            if (t == typeof(string) || t == typeof(double) || t == typeof(int) || t == typeof(bool) || t == typeof(long) || t == typeof(decimal) ||
                t == typeof(double?) || t == typeof(int?) || t == typeof(bool?) || t == typeof(long?) || t == typeof(decimal?))
            {
                xw.WriteString(pValue.ToString());
            }
            else if (pInfo.PropertyType.IsEnum)
            {
                xw.WriteString(string.Format("{0}", (int)pValue));
            }
            else if (t == typeof(DateTime))
            {
                // xw.WriteString(((DateTime)pValue).ToString("yyyyMMdd")); // String.Format("{0:YYYYMMDD}", Convert.ToString(((DateTime)value)));
                xw.WriteString(String.Format("{0:yyyyMMdd}", pValue));
            }
            else if (t == typeof(int[]))
            {
                xw.WriteString(ConvertArrayToString(pValue as int[]));
            }
            else if (t == typeof(double[]))
            {
                xw.WriteString(ConvertArrayToString(pValue as double[]));
            }
            else if (t == typeof(string[]))
            {
                WriteStringArray(pValue as string[], xw);
            }
            else if (t.IsClass)
            {
                CreateXmlText(pValue, xw);
            }
            else
            {
                throw new Exception("not supported value type " + t.Name);
            }

            xw.WriteEndElement();       // value

            xw.WriteEndElement();       // property

        }


        /// <summary>
        /// Write string array to xml
        /// </summary>
        /// <param name="values"></param>
        /// <param name="xw"></param>
        private static void WriteStringArray(string[] values, XmlTextWriter xw)
        {
            foreach (var value in values)
            {
                xw.WriteStartElement("row");
                xw.WriteString(value);
                xw.WriteEndElement();
            }
        }


        /// <summary>
        /// Convert Integer Array to base 64 string
        /// </summary>
        /// <param name="arrayData"></param>
        /// <returns></returns>
        public static string ConvertArrayToString(int[] arrayData)
        {
            // first convert array to byte
            byte[] bytes = new byte[arrayData.Length * sizeof(int)];
            System.Buffer.BlockCopy(arrayData, 0, bytes, 0, bytes.Length);
            string base64String = System.Convert.ToBase64String(bytes);
            return base64String;
        }

        /// Convert double array data to base 64 string
        /// </summary>
        /// <param name="arrayData"></param>
        /// <returns></returns>
        public static string ConvertArrayToString(double[] arrayData)
        {
            // first convert array to byte
            byte[] bytes = new byte[arrayData.Length * sizeof(double)];
            System.Buffer.BlockCopy(arrayData, 0, bytes, 0, bytes.Length);
            string base64String = System.Convert.ToBase64String(bytes);
            return base64String;
        }


        /// <summary>
        /// Gets attribute value
        /// </summary>
        /// <param name="xnode"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string GetAttributeValue(XmlNode xnode, string name)
        {
            var att = xnode.Attributes[name];
            if (att == null)
                return null;
            else
                return att.Value;
        }



        public static string GetAttribute(XmlNode xnode, string name)
        {
            var att = xnode.Attributes[name];
            if (att == null)
                return null;
            else
                return att.Value;
        }


        public static double GetDouble(object value)
        {
            double d;
            if (double.TryParse(value.ToString(), out d))
            {
                return d;
            }
            return 0;
        }


        public static int GetInt(object value)
        {
            int d;
            if (int.TryParse(value.ToString(), out d))
            {
                return d;
            }
            return 0;
        }


        public static long GetLong(object value)
        {
            long d;
            if (long.TryParse(value.ToString(), out d))
            {
                return d;
            }
            return 0;
        }


        /// <summary>
        /// Read string array from xml object
        /// </summary>
        /// <param name="xValue"></param>
        /// <returns></returns>
        public static string[] GetStringArray(XmlNode xValue)
        {
            string[] data = null;
            var rows = xValue.SelectNodes("row");

            if (rows.Count > 0)
            {
                data = new string[rows.Count];

                for (int i = 0; i < rows.Count; i++)
                {
                    data[i] = rows[i].InnerText;
                }
            }
            return data;
        }


        public static DateTime GetDate(string value)
        {
            DateTime theDate;
            if (DateTime.TryParseExact(value, "yyyyMMdd", CultureInfo.InvariantCulture, DateTimeStyles.None, out theDate))
                return theDate;
            else
                return DateTime.Today;
        }


        public static bool GetBool(string value)
        {
            bool result;
            if (bool.TryParse(value, out result)) ;

            return result;
        }


        public static object GetEnumValue(Type type, string value)
        {
            // check to see if the value is integer
            int intValue;
            if (int.TryParse(value, out intValue))
            {
                // integer value
                if (Enum.IsDefined(type, intValue))
                    return intValue;

            }
            else {
                // string value
                if (Enum.IsDefined(type, value))
                    return Enum.Parse(type, value);
            }

            return null;
        }

    }
}
