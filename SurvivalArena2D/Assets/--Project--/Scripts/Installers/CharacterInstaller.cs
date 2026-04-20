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

    private Character _character;
    public override void InstallBindings()
    {
        BindConfig();
        BindInstance();
        BindWeaponController();
    }

    private void BindWeaponController()
    {
        var weaponController = _character.GetComponentInChildren<WeaponController>();

        Container.Bind<IWeaponController>()
            .FromInstance(weaponController)
            .AsSingle();
    }

    private void BindInstance()
    {
        _character = Container.InstantiatePrefabForComponent<Character>(_characterPrefab, _characterSpawnPoint.position, Quaternion.identity, null);
        Container.BindInterfacesAndSelfTo<Character>().FromInstance(_character).AsSingle().NonLazy();
        Container.BindInterfacesTo<CharacterMovement>().AsSingle().NonLazy();
    }


    private void BindConfig()
    {
        Container.Bind<CharacterStatsConfig>().FromInstance(_characterStatsConfig).AsSingle().NonLazy();
    }
}
