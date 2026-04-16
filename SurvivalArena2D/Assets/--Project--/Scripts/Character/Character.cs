using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Character : MonoBehaviour, IEnemyTarget
{
    private int _maxHelth;
    private int _healht;

    private IWeapon _currentWeapon;

    private IMouseInput _mouseInput;

    public Vector2 MoveDirection { get; private set; }
    public Vector2 AimDirection { get; private set; }

    public float Speed { get; private set; }

    public Vector3 Position => transform.position;

    [Inject]
    private void Construct(CharacterStatsConfig characterStatsConfig, IMouseInput mouseInput)
    {
        _healht = _maxHelth = characterStatsConfig.MaxHealth;
        Speed = characterStatsConfig.Speed;
        _mouseInput = mouseInput;
    }

    private void Update()
    {
        UpdateAimDirection();
    }

    public void SetWeapon(IWeapon weapon)
    {
        _currentWeapon = weapon;
    }

    public void Attack()
    {
        _currentWeapon?.Attack(transform.position, AimDirection);
    }
    public void TakeDamage(int damage)
    {
        Debug.Log("Im take damage");
    }
    public void SetMoveDirection(Vector2 direction)
    {
        MoveDirection = direction;
    }
    private void UpdateAimDirection()
    {
        Vector2 dir = _mouseInput.MouseWorldPosition - (Vector2)transform.position;
        AimDirection = dir.normalized;
    }
}
