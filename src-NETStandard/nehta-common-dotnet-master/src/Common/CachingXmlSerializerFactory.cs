using System;
using System.Collections.Generic;
using System.Globalization;
using System.Xml.Serialization;

namespace Nehta.VendorLibrary.Common
{
    // Replaced Serializer with this cached one which protects memory leakage - code from here:
    // http://dotnetcodebox.blogspot.com.au/2013/01/xmlserializer-class-may-result-in.html
    // Microsoft highlighted the issue here:
    // https://blogs.msdn.microsoft.com/tess/2006/02/15/net-memory-leak-xmlserializing-your-way-to-a-memory-leak/

    /// <summary>
    /// Replaced Serializer with this cached one which protects memory leakage
    /// </summary>
    public static class CachingXmlSerializerFactory
    {
        private static readonly Dictionary<string, XmlSerializer> Cache = new Dictionary<string, XmlSerializer>();

        private static readonly object SyncRoot = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public static XmlSerializer Create(Type type, XmlRootAttribute root)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (root == null) throw new ArgumentNullException("root");

            //Added namespace into key to address incorrect namespace returned my HI Service
            //var key = String.Format(CultureInfo.InvariantCulture, "{0}:{1}", type, root.ElementName);
            var key = String.Format(CultureInfo.InvariantCulture, "{0}:{1}:{2}", type, root.Namespace, root.ElementName);

            lock (SyncRoot)
            {
                if (!Cache.ContainsKey(key))
                {
                    Cache.Add(key, new XmlSerializer(type, root));
                }
            }

            return Cache[key];
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="root"></param>
        /// <returns></returns>
        public static XmlSerializer Create<T>(XmlRootAttribute root)
        {
            return Create(typeof(T), root);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static XmlSerializer Create<T>()
        {
            return Create(typeof(T));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defaultNamespace"></param>
        /// <returns></returns>
        public static XmlSerializer Create<T>(string defaultNamespace)
        {
            return Create(typeof(T), defaultNamespace);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static XmlSerializer Create(Type type)
        {
            return new XmlSerializer(type);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="defaultNamespace"></param>
        /// <returns></returns>
        public static XmlSerializer Create(Type type, string defaultNamespace)
        {
            return new XmlSerializer(type, defaultNamespace);
        }
    }
}
