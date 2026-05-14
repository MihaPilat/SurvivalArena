using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemyTarget : IDamageable
{
    Vector3 Position { get; }
}
