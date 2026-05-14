using UnityEngine;

public class ExperienceCollector : MonoBehaviour
{
    [SerializeField] private CircleCollider2D _collectionTrigger;
    public void UpdateRadius(float value)
    {
        _collectionTrigger.radius = value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out ExperienceOrb orb))
        {
            orb.StartFollowing(transform.parent);
        }
    }
}
