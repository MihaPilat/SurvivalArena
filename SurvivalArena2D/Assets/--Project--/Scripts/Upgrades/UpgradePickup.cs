using UnityEngine;

public class UpgradePickup : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _iconRenderer;

    private UpgradeData _data;

    public void Setup(UpgradeData newData)
    {
        _data = newData;
        if (_iconRenderer != null && _data != null)
        {
            _iconRenderer.sprite = _data.Icon;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Character character))
        {
            _data.Apply(character);
            Destroy(gameObject);
        }
    }
}
