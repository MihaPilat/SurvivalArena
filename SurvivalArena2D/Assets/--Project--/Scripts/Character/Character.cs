using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Character : MonoBehaviour, IEnemyTarget
{
    private int _maxHelth;
    private int _healht;

    public float Speed { get; private set; }

    public Vector3 Position => transform.position;

    [Inject]
    private void Construct(CharacterStatsConfig characterStatsConfig)
    {
        _healht = _maxHelth = characterStatsConfig.MaxHealth;
        Speed = characterStatsConfig.Speed;
    }


    public void TakeDamage()
    {
        Debug.Log("Im take damage");
    }
}
