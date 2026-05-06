using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    private bool _isCollected = false;
    private int _expAmount;

    public void Init(int amount) => _expAmount = amount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (_isCollected) return;

        if (other.TryGetComponent<Character>(out var player))
        {
            _isCollected = true;
            player.AddExperience(_expAmount);
            Destroy(gameObject);
        }
    }
}
