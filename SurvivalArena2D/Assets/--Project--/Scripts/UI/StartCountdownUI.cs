using TMPro;
using UnityEngine;
using Zenject;

public class StartCountdownUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _countdownText;

    private EnemySpawner _spawner;

    [Inject]
    private void Construct(EnemySpawner enemySpawner)
    {
        _spawner = enemySpawner;
    }

    private void OnEnable()
    {
        _spawner.OnPreWaveCountdown += UpdateCountdownText;
        _spawner.OnWaveStarted += HideUI;
    }

    private void OnDisable()
    {
        _spawner.OnPreWaveCountdown -= UpdateCountdownText;
        _spawner.OnWaveStarted -= HideUI;
    }

    private void UpdateCountdownText(float timeLeft)
    {
        if (timeLeft > 3.5f)
        {
            _countdownText.text = "ОНИ БЛИЗКО...";
            _countdownText.color = Color.white;
        }
        else if (timeLeft > 1.5f)
        {
            _countdownText.text = "ПРИГОТОВЬСЯ...";
            _countdownText.color = Color.yellow;
        }
        else if (timeLeft > 0f)
        {
            _countdownText.text = "ВЫЖИВИ!";
            _countdownText.color = Color.red;
            _countdownText.transform.localScale = Vector3.one * (1f + (1f - timeLeft));
        }
    }

    private void HideUI(int wave)
    {
        gameObject.SetActive(false);
    }
}
