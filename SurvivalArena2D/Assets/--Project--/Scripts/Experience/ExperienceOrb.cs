using UnityEngine;

public class ExperienceOrb : MonoBehaviour
{
    private int _expAmount;

    public void Init(int amount) => _expAmount = amount;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Character>(out var player))
        {
            player.AddExperience(_expAmount);
            Destroy(gameObject);
        }
    }
}
