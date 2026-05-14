using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _acceleration = 0.2f;

    private ExperienceOrb _originPrefab;
    private PoolFactory _factory;
    private int _amount;

    private Transform _target;
    private bool _isCollected = false;

    public void Init(int amount, ExperienceOrb prefab, PoolFactory factory)
    {
        _amount = amount;
        _originPrefab = prefab;
        _factory = factory;
        _isCollected = false;
        _target = null;
    }

    private void Update()
    {
        if(_target!=null&& !_isCollected)
        {
            _speed += _acceleration * Time.deltaTime;

            transform.position = Vector3.MoveTowards(
                transform.position,
                _target.position,
                _speed * Time.deltaTime
            );
        }
    }

    public void StartFollowing(Transform target)
    {
        _target = target;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isCollected) return;

        if (other.TryGetComponent<Character>(out var player))
        {
            _isCollected = true;
            player.AddExperience(_amount);
            Collect();
        }
    }
    private void Collect()
    {
        _factory.Reclaim(this, _originPrefab);
    }
}
