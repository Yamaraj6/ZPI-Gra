using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Configuration
{
    public interface ICharacterAnimationControllerConfigurationProvider
    {
        float Speed { get; }
        float JumpingSpeed { get; }
        float RotatingSpeed { get; }
    }

    public class CharacterAnimationControllerConfigurationProvider : ICharacterAnimationControllerConfigurationProvider
    {
        public MyConfig Config;

        public CharacterAnimationControllerConfigurationProvider(MyConfig myConfig)
        {
            this.Config = myConfig;
        }

        public float Speed
        {
            get
            {
                return Config.Configuration.GetConfiguration("CharacterAnimationController", "Speed", typeof(float));
            }
        }

        public float JumpingSpeed
        {
            get
            {
                return Config.Configuration.GetConfiguration("CharacterAnimationController", "JumpingSpeed", typeof(float));
            }
        }

        public float RotatingSpeed
        {
            get
            {
                return Config.Configuration.GetConfiguration("CharacterAnimationController", "RotatingSpeed", typeof(float));
            }
        }
    }
}
