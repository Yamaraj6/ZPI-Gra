using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Assets.DataManager.Scripts.Models;
namespace ZPIGame.Assets.DataManager.Scripts.AntiCheat
{
    public interface IAntiCheatLogic
    {
        bool CheckIfCheated(Player serverPlayer, Player localPlayer);
    }
    public class AntiCheatLogic : IAntiCheatLogic
    {
        public bool CheckIfCheated(Player serverPlayer, Player localPlayer)
        {
            return true; //TODO: IMPLEMENT ANTICHEAT LOGIC!!!
        }
    }

}