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

    public static class CachingXmlSerializerFactory
    {
        private static readonly Dictionary<string, XmlSerializer> Cache = new Dictionary<string, XmlSerializer>();

        private static readonly object SyncRoot = new object();

        public static XmlSerializer Create(Type type, XmlRootAttribute root)
        {
            if (type == null) throw new ArgumentNullException("type");
            if (root == null) throw new ArgumentNullException("root");

            var key = String.Format(CultureInfo.InvariantCulture, "{0}:{1}", type, root.ElementName);

            lock (SyncRoot)
            {
                if (!Cache.ContainsKey(key))
                {
                    Cache.Add(key, new XmlSerializer(type, root));
                }
            }

            return Cache[key];
        }

        public static XmlSerializer Create<T>(XmlRootAttribute root)
        {
            return Create(typeof(T), root);
        }

        public static XmlSerializer Create<T>()
        {
            return Create(typeof(T));
        }

        public static XmlSerializer Create<T>(string defaultNamespace)
        {
            return Create(typeof(T), defaultNamespace);
        }

        public static XmlSerializer Create(Type type)
        {
            return new XmlSerializer(type);
        }

        public static XmlSerializer Create(Type type, string defaultNamespace)
        {
            return new XmlSerializer(type, defaultNamespace);
        }
    }
}
