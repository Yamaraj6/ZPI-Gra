using Assets.DataManager.Scripts.Models;

namespace Assets.Scripts.Containers
{
    public interface IPlayerContainer
    {
        Player Player {get; set;}
        void SavePlayer();


    }
}