using UnityEngine;

namespace ZPIGame.Assets.DataManager.Scripts.FirstLaunch
{
    public interface IFirstLaunchChecker
    {
        bool IsFirstLaunch { get; set; }
        void SetFirstLaunchToTrue();
    }
    public class FirstLaunchChecker : IFirstLaunchChecker
    {
        public bool IsFirstLaunch { get; set; }
        private readonly string _key = "firstlaunch";
        public FirstLaunchChecker ()
        {
            IsFirstLaunch = PlayerPrefs.GetInt(_key, 0) == 1 ?  true : false;     
        }

        public void SetFirstLaunchToTrue()
        {
            PlayerPrefs.SetInt(_key, 1);
        }

    }
}