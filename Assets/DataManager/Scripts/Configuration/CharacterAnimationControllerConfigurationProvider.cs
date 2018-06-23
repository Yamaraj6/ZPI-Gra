namespace Assets.DataManager.Scripts.Configuration
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

        public float Speed => Config.Configuration.GetConfiguration("CharacterAnimationController", "Speed", typeof(float));

        public float JumpingSpeed => Config.Configuration.GetConfiguration("CharacterAnimationController", "JumpingSpeed", typeof(float));

        public float RotatingSpeed => Config.Configuration.GetConfiguration("CharacterAnimationController", "RotatingSpeed", typeof(float));
    }
}
