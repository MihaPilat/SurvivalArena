using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class HotbarSlotView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _selectionFrame;
    [SerializeField] private TextMeshProUGUI _keyText;

    [SerializeField] private RectTransform _iconContainer;
    [SerializeField] private float _selectPunchAmount = 0.2f;
    [SerializeField] private float _animationDuration = 0.3f;

    private Tween _pulseTween;

    public void SetIcon(Sprite sprite)
    {
        _icon.transform.DOKill();

        if (sprite == null)
        {
            _icon.enabled = false;
            return;
        }

        _icon.sprite = sprite;
        _icon.enabled = true;
        _icon.color = Color.white;

        StartCoroutine(AnimateIconRoutine());
    }

    public void SetSelection(bool isSelected)
    {
        _selectionFrame.transform.DOKill();
        _selectionFrame.DOKill();
        _iconContainer.DOKill();
        _pulseTween?.Kill();

        if (isSelected)
        {
            _selectionFrame.enabled = true;
            var color = _selectionFrame.color;
            color.a = 1f;
            _selectionFrame.color = color;

            _selectionFrame.transform.localScale = Vector3.one * 0.8f;
            _selectionFrame.transform.DOScale(Vector3.one, _animationDuration)
                .SetEase(Ease.OutBack)
                .SetUpdate(true);

            _pulseTween = _selectionFrame.DOFade(0.3f, 0.8f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(Ease.InOutSine)
                .SetUpdate(true);

            _iconContainer.DOPunchScale(Vector3.one * _selectPunchAmount, _animationDuration, 5, 1)
                .SetUpdate(true);
        }
        else
        {
            _selectionFrame.DOFade(0, _animationDuration)
            .SetUpdate(true)
            .OnComplete(() =>
            {
                if (_selectionFrame.color.a < 0.1f)
                    _selectionFrame.enabled = false;
            });

            _iconContainer.DOScale(Vector3.one, _animationDuration).SetUpdate(true);
        }
    }

    public void SetKeyText(string text) => _keyText.text = text;

    private System.Collections.IEnumerator AnimateIconRoutine()
    {
        _icon.transform.localScale = Vector3.zero;
        yield return new WaitForEndOfFrame();

        _icon.transform.DOScale(Vector3.one, _animationDuration)
            .SetEase(Ease.OutBack)
            .SetUpdate(true);
    }

    private void OnDestroy()
    {
        _pulseTween?.Kill();
        _selectionFrame.transform.DOKill();
    }
}