using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class CharacterInstaller : MonoInstaller
{
    [SerializeField] private Character _characterPrefab;

    [SerializeField] private Transform _characterSpawnPoint;
    [SerializeField] private CharacterStatsConfig _characterStatsConfig;

    public override void InstallBindings()
    {
        BindConfig();
        BindInstance();
    }

    private void BindInstance()
    {
        Character character = Container.InstantiatePrefabForComponent<Character>(_characterPrefab, _characterSpawnPoint.position, Quaternion.identity, null);
        Container.BindInterfacesAndSelfTo<Character>().FromInstance(character).AsSingle().NonLazy();
        Container.BindInterfacesTo<CharacterMovement>().AsSingle().NonLazy();
    }


    private void BindConfig()
    {
        Container.Bind<CharacterStatsConfig>().FromInstance(_characterStatsConfig).AsSingle().NonLazy();
    }
}
