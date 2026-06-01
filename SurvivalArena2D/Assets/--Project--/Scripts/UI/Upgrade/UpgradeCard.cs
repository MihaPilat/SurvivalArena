using DG.Tweening;
using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeCard : MonoBehaviour
{
    public event Action OnClicked;

    [SerializeField] private TextMeshProUGUI _titleText;
    [SerializeField] private TextMeshProUGUI _descriptionText;
    [SerializeField] private Image _iconImage;
    [SerializeField] private Button _selectButton;

    private UpgradeData _data;
    private Action<UpgradeData> _onSelected;

    public UpgradeData CurrentUpgrade => _data;

    public void Setup(UpgradeData data, Action<UpgradeData> onSelected)
    {
        _data = data;
        _onSelected = onSelected;

        _titleText.text = data.Title;
        _descriptionText.text = data.Description;
        _iconImage.sprite = data.Icon;

        _selectButton.onClick.RemoveAllListeners();
        _selectButton.onClick.AddListener(HandleClick);

        transform.localScale = Vector3.zero;
        transform.DOScale(Vector3.one, 0.4f).SetEase(Ease.OutBack).SetUpdate(true);
    }

    private void HandleClick()
    {
        OnClicked?.Invoke();

        _onSelected?.Invoke(_data);
    }
}
