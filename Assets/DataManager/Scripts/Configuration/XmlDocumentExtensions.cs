using System;
using System.Xml;

namespace Assets.DataManager.Scripts.Configuration
{
    public static class XmlDocumentExtensions
    {

        public static dynamic GetConfiguration(this XmlDocument xml, string nodeName, string tagName, Type type)
        {
            var xmlData = xml.SelectSingleNode($"//{nodeName}/{tagName}")?.InnerText;
            return Convert.ChangeType(xmlData, type);
        }
    }
}
