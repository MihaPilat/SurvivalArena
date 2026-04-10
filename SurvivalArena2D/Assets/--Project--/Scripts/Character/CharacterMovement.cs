using UnityEngine;
using Zenject;
public class CharacterMovement: IFixedTickable
{

    private IInput _input;
    private Transform _transform;
    private Rigidbody2D _rb;
    private float Speed { get; set; }

    

    [Inject]
    private void Construct(IInput  input, Character character)
    {
        _input = input;
        Speed = character.Speed;
        _transform = character.transform;
        _rb = character.GetComponent<Rigidbody2D>();
    }
    public void FixedTick()
    {
        Vector2 move = _input.Move.normalized;
        Vector2 target = _rb.position + move * Speed * Time.fixedDeltaTime;
        _rb.MovePosition(target);
    }
}
