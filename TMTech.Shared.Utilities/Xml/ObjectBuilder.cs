using System;
using System.Collections.Generic;
using System.Reflection;
using System.Xml;

namespace TMTech.Shared.Utilities.Xml
{

    /// <summary>
    /// An implementation of IDataRepository interface for testing purpose.
    /// </summary>
    public class ObjectBuilder
    {

        /// <summary>
        /// Holds the registered assemblies, 
        /// </summary>
        private List<Assembly> mRegisteredAssemblies;



        /// <summary>
        /// Constructor
        /// </summary>
        public ObjectBuilder()
        {
            mRegisteredAssemblies = new List<Assembly>();
        }



        /// <summary>
        /// REgister assembly
        /// </summary>
        /// <param name="assembl"></param>
        public void RegisterAssembly(Assembly assembly)
        {
            mRegisteredAssemblies.Add(assembly);
        }



        /// <summary>
        /// Gets an instance object from xml node.
        /// </summary>
        /// <param name="objectNode"></param>
        /// <returns></returns>
        public object CreateObjectFromXml(XmlNode xObject)
        {
            if (xObject == null) return null;

            // get object type name
            var typeName = WriteHelper.GetAttributeValue(xObject, "type");

            // get type from the assembly
            var t = GetType(typeName);

            if (t == null) return null;

            // create type instance
            object obj = Activator.CreateInstance(t);

            if (obj != null)
            {
                // get properties
                foreach (XmlNode xProperty in xObject.SelectNodes("property"))
                {
                    try
                    {
                        GetProperty(obj, xProperty);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message + "; Failed to create property " + xProperty.OuterXml);
                    }

                }
            }

            return obj;

        }


        #region private methods


        /// <summary>
        /// Get type from the registered assembly
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        private Type GetType(string name)
        {
            foreach (var assembly in mRegisteredAssemblies)
            {
                var t = assembly.GetType(name);

                if (t != null) return t;
            }

            return null;
        }




        /// <summary>
        /// Get  property value from xml node.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="xProperty"></param>
        public void GetProperty(Object obj, XmlNode xProperty)
        {
            string propertyName = WriteHelper.GetAttribute(xProperty, "name");
            PropertyInfo propertyInfo = obj.GetType().GetProperty(propertyName);
            if (propertyInfo == null || !propertyInfo.CanWrite) return;


            XmlNode xValue = xProperty.SelectSingleNode("value");
            if (xValue == null) return;

            Type t = propertyInfo.PropertyType;

            if (t == typeof(string))
            {
                propertyInfo.SetValue(obj, xValue.InnerText);
                return;
            }
            else if (t == typeof(double) || t == typeof(double?))
            {
                propertyInfo.SetValue(obj, WriteHelper.GetDouble(xValue.InnerText));
                return;
            }
            else if (t == typeof(int) || t == typeof(int?))
            {
                propertyInfo.SetValue(obj, WriteHelper.GetInt(xValue.InnerText));
                return;
            }
            else if (t == typeof(long) || t == typeof(long?))
            {
                propertyInfo.SetValue(obj, WriteHelper.GetLong(xValue.InnerText));
                return;
            }
            else if (t == typeof(DateTime) || t == typeof(DateTime?))
            {
                propertyInfo.SetValue(obj, WriteHelper.GetDate(xValue.InnerText));
            }
            else if (t == typeof(bool) || t == typeof(bool?))
            {
                propertyInfo.SetValue(obj, WriteHelper.GetBool(xValue.InnerText));
            }
            else if (t.IsEnum)
            {
                propertyInfo.SetValue(obj, WriteHelper.GetEnumValue(propertyInfo.PropertyType, xValue.InnerText));
                return;
            }
            else if (t.IsClass)
            {
                XmlNode xObject = xValue.SelectSingleNode("object");
                if (xObject != null)
                    propertyInfo.SetValue(obj, CreateObjectFromXml(xObject));
            }
            else
            {
                throw new Exception("not supported value type " + propertyInfo.PropertyType.Name);
            }

        }
        #endregion
    }
}
