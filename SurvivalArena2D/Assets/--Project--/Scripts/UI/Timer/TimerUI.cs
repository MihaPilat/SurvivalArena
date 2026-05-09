using UnityEngine;
using TMPro;
using Zenject;
using DG.Tweening;

public class TimerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _timeText;

    private TimerService _timerService;
    private int _lastSecond;

    [Inject]
    private void Construct(TimerService timerService) => _timerService = timerService;

    private void Update()
    {
        _timeText.text = _timerService.GetFormattedTime();

        int currentSecond = Mathf.FloorToInt(_timerService.CurrentTime % 60);
        if (currentSecond != _lastSecond)
        {
            _lastSecond = currentSecond;
            AnimateTick();
        }
    }

    private void AnimateTick()
    {
        _timeText.transform.DOPunchScale(Vector3.one * 0.1f, 0.2f);
    }
}
