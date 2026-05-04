using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using Zenject;

public class WorldSpaceCharacterUI : MonoBehaviour
{
    [SerializeField] private Image _healthFill;

    [SerializeField] private Image _expRadialFill;
    [SerializeField] private TextMeshProUGUI _levelText;

    private ILevelable _levelSystem;
    private Character _character;

    [Inject]
    public void Construct(ILevelable levelSystem)
    {
        _levelSystem = levelSystem;
    }

    private void Awake()=>_character = GetComponentInParent<Character>();

    private void OnEnable()
    {
        _levelSystem.OnExpChanged += UpdateExp;
        _levelSystem.OnLevelUp += UpdateLevel;

        _character.OnHealthChanged += UpdateHealth;
    }

    private void OnDisable()
    {
        _levelSystem.OnExpChanged -= UpdateExp;
        _levelSystem.OnLevelUp -= UpdateLevel;

        _character.OnHealthChanged -= UpdateHealth;
    }

    private void UpdateHealth(int current, int max)
    {
        float target = (float)current / max;
        _healthFill.DOFillAmount(target, 0.3f);

        transform.DOShakePosition(0.2f, 0.05f);
    }

    private void UpdateExp(int current, int next)
    {
        float target = (float)current / next;
        _expRadialFill.DOFillAmount(target, 0.5f).SetEase(Ease.OutCubic);
    }

    private void UpdateLevel(int level)
    {
        _levelText.text = level.ToString();
        _levelText.transform.DOPunchScale(Vector3.one * 1.5f, 0.4f);
    }

    private void LateUpdate() => transform.rotation = Quaternion.identity;
}
