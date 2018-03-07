using Assets.Scripts.Containers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.GameManagers
{
    public class GameLaunchManager : MonoBehaviour
    {
        private IPlayerContainer playerContainer;

        public void Awake()
        {
            this.playerContainer = ContainerInstaller.diContainer.Resolve<IPlayerContainer>();

            Debug.Log(playerContainer.Player.Name);
        }


        public void OnApplicationQuit()
        {
            playerContainer.SavePlayer();
        }
    }
}
