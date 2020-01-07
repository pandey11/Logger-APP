using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using App.Interfaces;
using Microsoft.Extensions.FileProviders;
using System.Linq;

namespace App.Managers
{

    public class ConfigurationManager : IConfigurationManager
    {

        private readonly ConcurrentDictionary<Type, object> configurationCache = new ConcurrentDictionary<Type, object>();
        private IDictionary<string, string> Data = new Dictionary<string, string>();
        private readonly XmlDocument xmlDocument = new XmlDocument();
        public static ConfigurationManager Instance => new ConfigurationManager();

        private ConfigurationManager()
        {
            var provider = new PhysicalFileProvider(Path.GetDirectoryName(AppDomain.CurrentDomain.BaseDirectory));

            var xmlDocInfo = provider.GetFileInfo("Settings.xml").CreateReadStream();
            xmlDocument.Load(xmlDocInfo);
            foreach (XmlNode section in this.xmlDocument.GetElementsByTagName("Section"))
            {
                foreach (XmlNode node in section.ChildNodes)
                {
                    var namedAttribute = node.Attributes?["Name"].Value;
                    var valuedAttribute = node.Attributes?["Value"].Value;

                    this.Data.Add(section.Attributes["Name"].Value + namedAttribute, valuedAttribute);
                }
            }
            xmlDocInfo.Dispose();
        }

        public T Get<T>() where T : class, new()
        {
            return configurationCache.GetOrAdd(
                typeof(T),
                type =>
                {
                    string sectionName = typeof(T).ToString().Split('.').Last();
                    var propertyInfos = type.GetProperties();
                    var settings = new T();
                    foreach (var propertyInfo in propertyInfos)
                    {
                        propertyInfo.SetValue(settings, Data[sectionName + propertyInfo.Name]);
                    }
                    return settings;
                }) as T;
        }
    }
}