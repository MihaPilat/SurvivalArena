using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(RectTransform))]
public class IndicatorUI : MonoBehaviour
{
    [SerializeField] private Image _iconImage;
    [SerializeField] private RectTransform _arrowTransform;
    [SerializeField] private float _margin = 50f;

    private Transform _target;
    private Camera _mainCam;
    private RectTransform _rectTransform;

    [Inject]
    private void Construct(Camera mainCam)
    {
        _mainCam = mainCam;
    }

    public void Setup(Transform target, Sprite icon)
    {
        _target = target;
        _iconImage.sprite = icon;
        _rectTransform = GetComponent<RectTransform>();

        ToggleVisuals(false);

        if (_target != null && _mainCam != null)
        {
            Vector3 screenPos = _mainCam.WorldToScreenPoint(_target.position);
            UpdatePosition(screenPos);
        }
    }

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 screenPos = _mainCam.WorldToScreenPoint(_target.position);

        bool isOffScreen = screenPos.z < 0 ||
                           screenPos.x <= 0 || screenPos.x >= Screen.width ||
                           screenPos.y <= 0 || screenPos.y >= Screen.height;

        ToggleVisuals(isOffScreen);

        if (isOffScreen)
        {
            UpdatePosition(screenPos);
        }


    }

    private void ToggleVisuals(bool show)
    {
        if (_iconImage != null) _iconImage.gameObject.SetActive(show);
        if (_arrowTransform != null) _arrowTransform.gameObject.SetActive(show);
    }

    private void UpdatePosition(Vector3 screenPos)
    {
        if (screenPos.z < 0) screenPos *= -1;

        Vector2 canvasCenter = new Vector2(Screen.width, Screen.height) * 0.5f;
        Vector2 centeredPos = (Vector2)screenPos - canvasCenter;

        float halfWidth = _rectTransform.rect.width * 0.5f;
        float halfHeight = _rectTransform.rect.height * 0.5f;

        float xLimit = canvasCenter.x - (_margin + halfWidth);
        float yLimit = canvasCenter.y - (_margin + halfHeight);

        float x = Mathf.Clamp(centeredPos.x, -xLimit, xLimit);
        float y = Mathf.Clamp(centeredPos.y, -yLimit, yLimit);

        _rectTransform.anchoredPosition = new Vector2(x, y);

        if (_arrowTransform != null)
        {
            float angle = Mathf.Atan2(centeredPos.y, centeredPos.x);
            _arrowTransform.rotation = Quaternion.Euler(0, 0, angle * Mathf.Rad2Deg);
        }
    }
}
