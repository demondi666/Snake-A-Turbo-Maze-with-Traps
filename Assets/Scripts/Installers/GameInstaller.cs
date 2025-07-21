using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    public override void InstallBindings()
    {
        BindFactory();
    }

    private void BindFactory()
    {
        Container.Bind<EffectorFactory>().AsSingle();
        Container.Bind<SnakeFactory>().AsSingle();
    }
}
