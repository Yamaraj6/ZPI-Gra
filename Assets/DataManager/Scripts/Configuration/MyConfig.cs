using System.IO;
using System.Xml;
using UnityEngine;

namespace Assets.DataManager.Scripts.Configuration
{
    public class MyConfig
    {
        private const string ConfigFileName = "appconfig";
        private readonly string _path = Application.persistentDataPath + @"/appconfig.xml";

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
            if (!File.Exists(_path))
            {
                var xmlAsset = Resources.Load(ConfigFileName) as TextAsset;
                if (xmlAsset != null)
                {
                    var content = xmlAsset.text;
                    File.WriteAllText(_path, content);
                }
            }

            Configuration = new XmlDocument();
            Configuration.Load(_path);

        }
    }
}
