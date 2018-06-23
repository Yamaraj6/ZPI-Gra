using Assets.DataManager.Scripts.Api;
using Assets.DataManager.Scripts.Configuration;
using Assets.DataManager.Scripts.Containers;
using Assets.Scripts.Containers;
using Zenject;
using ZPIGame.Assets.DataManager.Scripts.AntiCheat;
using ZPIGame.Assets.DataManager.Scripts.FirstLaunch;


public class ContainerInstaller : MonoInstaller
{

    public static DiContainer DiContainer;

    public override void InstallBindings()
    {
        DiContainer = new DiContainer();

        //tak uzywac dla zwyklych klas i przed konstruktorem klasy [Inject]
        //diContainer.Bind<ILanguage>()
        //    .To<Language>()
        //    .AsSingle(); 
        //
        //tak dla monobehviour i przed konstruktorem klasy [Inject]
        //        diContainer.Bind<FacebookPlayerRepository>()
        //          .FromNewComponentOn(this.gameObject)
        //          .AsSingle();
        //
        //zeby wyciagnac z kontenera (lepiej wstrzyknac jak wyzej niz wyciagac z kotenera! ale nie zawsze sie da)
        //ContainerInstaller.diContainer.Resolve<IPlayerContainer>();



        DiContainer.Bind<IPlayerContainer>().To<PlayerContainer>().AsSingle();
        DiContainer.Bind<MyConfig>().AsSingle(new MyConfig());
        DiContainer.Bind<ICharacterAnimationControllerConfigurationProvider>()
            .To<CharacterAnimationControllerConfigurationProvider>().AsSingle();
        DiContainer.Bind<IFirstLaunchChecker>().To<FirstLaunchChecker>().AsSingle();
        DiContainer.Bind<IServiceConnector>().To<ServiceConnector>().AsSingle();
        DiContainer.Bind<IServiceConnectorConfiguration>().To<ServiceConnectorConfiguration>().AsSingle();
        DiContainer.Bind<IPlayerService>().To<PlayerService>().AsSingle();
        DiContainer.Bind<ICardService>().To<CardService>().AsSingle();
        DiContainer.Bind<ICardContainer>().To<CardContainer>().AsSingle();
        DiContainer.Bind<IAntiCheatLogic>().To<AntiCheatLogic>().AsSingle();

    }
}

