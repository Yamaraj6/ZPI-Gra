using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Assets.Scripts.Configuration
{
    public static class XmlDocumentExtensions
    {

        public static dynamic GetConfiguration(this XmlDocument xml, string nodeName, string tagName, Type type)
        {
            var xmlData = xml.SelectSingleNode($"//{nodeName}/{tagName}").InnerText;
            return Convert.ChangeType(xmlData, type);
        }
    }
}
