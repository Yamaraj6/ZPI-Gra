using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ContainerInstaller : MonoInstaller {

    public static DiContainer diContainer;

    public override void InstallBindings()
    {
        diContainer = new DiContainer();

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
    }
}
