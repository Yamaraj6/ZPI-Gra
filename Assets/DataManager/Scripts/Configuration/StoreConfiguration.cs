using Assets.DataManager.Scripts.Configuration;

namespace ZPIGame.Assets.DataManager.Scripts.Configuration
{
    public interface IStoreConfiguration
    {
        int MaxCardPower { get; }
        int MaxCardManaCost { get; }
        int MaxCardCastingTime { get; }
    }
    public class StoreConfiguration : IStoreConfiguration
    {
        public MyConfig Config;

        public StoreConfiguration(MyConfig myConfig)
        {
            this.Config = myConfig;
        }

        public int MaxCardPower => Config.Configuration.GetConfiguration("StoreConfiguration", "MaxCardPower", typeof(int)); //todo temprtary adress config
        public int MaxCardManaCost => Config.Configuration.GetConfiguration("StoreConfiguration", "MaxCardManaCost", typeof(int)); //todo temprtary adress config
        public int MaxCardCastingTime => Config.Configuration.GetConfiguration("StoreConfiguration", "MaxCardCastingTime", typeof(int)); //todo temprtary adress config

    }
}