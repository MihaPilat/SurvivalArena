using UnityEngine;

public class CharacterView : MonoBehaviour
{
    [SerializeField] private Character _character;
    private void Update()
    {
        Flip();
    }

    private void Flip()
    {
        float dirX = _character.AimDirection.x;

        if (dirX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (dirX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
