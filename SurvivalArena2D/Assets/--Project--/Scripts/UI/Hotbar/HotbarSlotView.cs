using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HotbarSlotView : MonoBehaviour
{
    [SerializeField] private Image _icon;
    [SerializeField] private Image _selectionFrame;
    [SerializeField] private TextMeshProUGUI _keyText;

    public void SetIcon(Sprite sprite)
    {
        if (sprite == null)
        {
            _icon.enabled = false;
        }
        else
        {
            _icon.sprite = sprite;
            _icon.enabled = true;
        }
    }

    public void SetSelection(bool isSelected)
    {
        _selectionFrame.enabled = isSelected;
    }

    public void SetKeyText(string text) => _keyText.text = text;
}