using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Monster : MonoBehaviour
{
    private NavMeshAgent agent;

    public NavMeshAgent MonsterAgent 
    {
        get { return agent; }
        set { agent = value; }
    }

    private Animator animator;

    public Animator MonsterAnimator
    {
        get { return animator; }
        set { animator = value; }
    }

    private float _animationBlend;
    private bool isDead;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    private void OnEnable()
    {
        isDead = false;
    }

    private void Start()
    {
        //agent.SetDestination(transform.position);
        StartCoroutine(RandomPatrol());
    }

    private void Update()
    {       
        if(agent.velocity != Vector3.zero)
        {
            _animationBlend = Mathf.Lerp(_animationBlend, agent.speed, Time.deltaTime);
            if (_animationBlend < 0.01f) _animationBlend = 0f;
            animator.SetFloat("Speed", _animationBlend);
        }
    }

    IEnumerator RandomPatrol()
    {
        while (!isDead)
        {
            yield return new WaitForSeconds(0.2f);

            if (agent.remainingDistance <= agent.stoppingDistance)
            {
                yield return new WaitForSeconds(2f);

                Vector3 point;
                while (!GetRandomPos(transform.position, 15f, out point)) { }                                
                agent.SetDestination(point);
                Debug.Log(point);
            }
        }

        yield break;
    }

    private bool GetRandomPos(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPos  = center + Random.insideUnitSphere * range;
        if(NavMesh.SamplePosition(randomPos, out NavMeshHit hit, 1f, NavMesh.AllAreas))
        {
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }
}
