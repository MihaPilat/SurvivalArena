using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class EnemyView : MonoBehaviour
{
    private const string IsDead= "IsDead";
    private const string IsIdle = "IsIdle";
    private const string IsMoving = "IsMoving";
    private const string AttackTrigger = "Attack";

    [SerializeField] private Color _hitColor = new Color(1f, 0f, 0f, 0.7f);
    [SerializeField] private float _hitDuration = 0.5f;

    private Animator _animator;
    private SpriteRenderer _spriteRenderer;
    private NavMeshAgent _agent;
    private EnemyEntity _enemyEntity;

    private float _initialScaleX;
    private Color _originalColor;
    private Coroutine _hitEffectCoroutine;


    void Awake()
    {
        _animator = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemyEntity = GetComponentInParent<EnemyEntity>();
        _agent = GetComponentInParent<NavMeshAgent>();

        _initialScaleX = transform.localScale.x;
        _originalColor = _spriteRenderer.color;
    }
    private void OnEnable()
    {
        if (_enemyEntity != null)
        {
            _enemyEntity.OnDied += StopHitEffect;
            _enemyEntity.OnHit += PlayHitEffect;
            _enemyEntity.OnAttack += PlayAttackAnimation;
        }
        _spriteRenderer.color = _originalColor;
    }

    private void OnDisable()
    {
        if (_enemyEntity != null)
        {
            _enemyEntity.OnDied -= StopHitEffect;
            _enemyEntity.OnHit -= PlayHitEffect;
            _enemyEntity.OnAttack -= PlayAttackAnimation;
        }
        if (_hitEffectCoroutine != null)
            StopCoroutine(_hitEffectCoroutine);

    }
    void Update()
    {
        if (_enemyEntity == null || _enemyEntity.IsDie)
            return;

        bool isIdle = _agent != null && _agent.enabled && _agent.desiredVelocity.sqrMagnitude > 0.01f;

        if (isIdle)
        {
            Flip(_agent.desiredVelocity.x);
        }
        else if (_enemyEntity != null && _enemyEntity.Target != null)
        {
            float directionToTarget = _enemyEntity.Target.position.x - transform.position.x;
            if (Mathf.Abs(directionToTarget) > 0.1f)
            {
                Flip(directionToTarget);
            }
        }
    }

    public void StartIdling() => _animator.SetBool(IsIdle, true);
    public void StopIdling() => _animator.SetBool(IsIdle, false);

    public void StartMoving() => _animator.SetBool(IsMoving, true);
    public void StopMoving() => _animator.SetBool(IsMoving, false);

    public void StartDead() => _animator.SetBool(IsDead, true);
    public void StopDead() => _animator.SetBool(IsDead, false);

    public void OnAttackAnimationEvent()
    {
        if (_enemyEntity != null)
        {
            _enemyEntity.PerformRangeAttack();
        }
    }

    private void Flip(float xVelocity)
    {
        float direction = xVelocity > 0 ? 1f : -1f;

        Vector3 currentScale = transform.localScale;
        currentScale.x = _initialScaleX * direction;
        transform.localScale = currentScale;
    }

    private void PlayAttackAnimation()
    {
        _animator.SetTrigger(AttackTrigger);
    }

    private void StopHitEffect()
    {
        if (_hitEffectCoroutine != null)
        {
            StopCoroutine(_hitEffectCoroutine);
            _spriteRenderer.color = _originalColor;
        }
    }
    private void PlayHitEffect()
    {
        if (_hitEffectCoroutine != null)
            StopCoroutine(_hitEffectCoroutine);

        _hitEffectCoroutine = StartCoroutine(HitFlashRoutine());
    }
    private IEnumerator HitFlashRoutine()
    {
        _spriteRenderer.color = _hitColor;

        yield return new WaitForSeconds(_hitDuration);

        _spriteRenderer.color = _originalColor;
        _hitEffectCoroutine = null;
    }
}
