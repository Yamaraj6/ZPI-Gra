using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.DataManager.Scripts.Configuration
{
    public interface IServiceConnectorConfiguration
    {
        string ServiceUrl { get; }
    }
    public class ServiceConnectorConfiguration : IServiceConnectorConfiguration
    {
        public MyConfig Config;

        public ServiceConnectorConfiguration(MyConfig myConfig)
        {
            this.Config = myConfig;
        }

        public string ServiceUrl => Config.Configuration.GetConfiguration("CharacterAnimationController", "ServiceUrl", typeof(string)); //todo temprtary adress config
    }
}
