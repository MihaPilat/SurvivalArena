using UnityEngine;

public class UpgradePickup : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _frameRenderer;
    [SerializeField] private SpriteRenderer _iconRenderer;
    [SerializeField] private float _maxIconSize = 0.9f;

    private UpgradeData _data;

    public void Setup(UpgradeData newData)
    {
        _data = newData;
        if (_iconRenderer != null && _data != null)
        {
            _iconRenderer.sprite = _data.Icon;

            _iconRenderer.transform.localScale = Vector3.one;

            Bounds bounds = _iconRenderer.sprite.bounds;
            float maxDimension = Mathf.Max(bounds.size.x, bounds.size.y);

            float scale = _maxIconSize / maxDimension;
            _iconRenderer.transform.localScale = new Vector3(scale, scale, 1);
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
