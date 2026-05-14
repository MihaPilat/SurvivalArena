using System.Collections.Generic;
using UnityEngine;

public class DamageHitbox : MonoBehaviour
{
    [SerializeField] private LayerMask _targetLayer;

    private int _damage;
    private Collider2D _collider;

    private HashSet<IDamageable> _hitTargets;

    private ContactFilter2D _filter;
    private Collider2D[] _results = new Collider2D[30];
    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
        _collider.enabled = false;

        _filter = new ContactFilter2D();
        _filter.useTriggers = true;
    }
    public void Enable(int currentDamage)
    {
        _damage = currentDamage;
        _hitTargets = new HashSet<IDamageable>();
        _collider.enabled = true;

        int count = _collider.OverlapCollider(_filter, _results);

        for (int i = 0; i < count; i++)
        {
            TryDamage(_results[i]);
        }
    }
    public void Disable()
    {
        _collider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        TryDamage(other);
    }

    private void TryDamage(Collider2D other)
    {
        if (other == null)
            return;

        if (((1 << other.gameObject.layer) & _targetLayer) == 0)
            return;

        var damageable = other.GetComponent<IDamageable>();

        if (damageable == null)
            return;

        if (_hitTargets.Contains(damageable))
            return;

        damageable.TakeDamage(_damage);
        _hitTargets.Add(damageable);
    }
}
