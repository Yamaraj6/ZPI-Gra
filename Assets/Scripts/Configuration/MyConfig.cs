using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Configuration
{
    public class MyConfig
    {
        private string configFileName = "appconfig";
        private string path = Application.persistentDataPath + @"/appconfig.xml";

        public XmlDocument Configuration { get; private set; }

        public MyConfig()
        {
          //  configFileName = configFileName;
         //   path = $"{Application.persistentDataPath}{configFileName}.xml";

            LoadConfigurationFile();
        }

    /*    [Inject]
        public MyConfig(string fileName) : base()
        {
            configFileName = fileName;
            path = $"{Application.persistentDataPath}{fileName}.xml";
        }*/

        private void LoadConfigurationFile()
        {
            if (!File.Exists(path))
            {
                var xmlAsset = Resources.Load(configFileName) as TextAsset;
                var content = xmlAsset.text;
                File.WriteAllText(path, content);
            }

            Configuration = new XmlDocument();
            Configuration.Load(path);

        }
    }
}
