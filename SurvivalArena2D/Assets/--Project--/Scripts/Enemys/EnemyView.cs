using System.Collections;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Animator))]
public class EnemyView : MonoBehaviour
{
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
            _enemyEntity.OnDied += PlayDeathAnimation;
            _enemyEntity.OnHit += PlayHitEffect;
        }
    }

    private void OnDisable()
    {
        if (_enemyEntity != null)
        {
            _enemyEntity.OnDied -= PlayDeathAnimation;
            _enemyEntity.OnHit -= PlayHitEffect;
        }
        if (_hitEffectCoroutine != null)
            StopCoroutine(_hitEffectCoroutine);
    }
    void Update()
    {
        if (_agent == null) return;

        float moveX = _agent.desiredVelocity.x;

        if (Mathf.Abs(moveX) > 0.01f)
        {
            Flip(moveX);
        }
    }
    private void Flip(float xVelocity)
    {
        float direction = xVelocity > 0 ? 1f : -1f;

        Vector3 currentScale = transform.localScale;
        currentScale.x = _initialScaleX * direction;
        transform.localScale = currentScale;
    }
    private void PlayDeathAnimation()
    {
        Debug.Log("View: Вижу смерть сущности, играю анимацию.");
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
