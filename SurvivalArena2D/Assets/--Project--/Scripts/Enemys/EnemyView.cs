using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyView : MonoBehaviour
{
    private NavMeshAgent _agent;
    private float _initialScaleX;

    void Awake()
    {
        _agent = GetComponentInParent<NavMeshAgent>();
        _initialScaleX = transform.localScale.x;
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
}
