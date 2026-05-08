using UnityEngine;
using UnityEngine.AI;

public class NpcDance : NpcBase
{
    private float radiusMove = 8f;
    private float speedSlowWalk = 1.2f;

    private float timeWalkingMin = 5f;      
    private float timeWalkingMax = 10f;
    private float timeWarmingUp = 3f;       
    private float timeDancingMin = 6f;      
    private float timeDancingMax = 12f;

    protected NavMeshAgent agent;

    private enum State { Walking, WarmingUp, Dancing }
    private State currentState;
    private float stateTimer = 0f;

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        agent.speed = speedSlowWalk;
        StartWalking();
    }

    void Update()
    {
        if (animator != null)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }

        switch (currentState)
        {
            case State.Walking:
                UpdateWalking();
                break;
            case State.WarmingUp:
                UpdateWarmingUp();
                break;
            case State.Dancing:
                UpdateDancing();
                break;
        }
    }

    void StartWalking()
    {
        currentState = State.Walking;
        stateTimer = Random.Range(timeWalkingMin, timeWalkingMax);

        if (agent != null)
        {
            agent.isStopped = false;
        }
        if (animator != null)
        {
            animator.SetBool("IsWarmingUp", false);
            animator.SetBool("IsDancing", false);
        }

        ChooseNewDestination();
        Debug.Log(nombreNPC + " empieza a caminar");
    }

    void UpdateWalking()
    {
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            ChooseNewDestination();
        }

        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0f)
        {
            StartWarmingUp();
        }
    }

    void StartWarmingUp()
    {
        currentState = State.WarmingUp;
        stateTimer = timeWarmingUp;

        if (agent != null) agent.isStopped = true;
        if (animator != null)
        {
            animator.SetBool("IsWarmingUp", true);
            animator.SetBool("IsDancing", false);
        }

        Debug.Log(nombreNPC + " se está estirando");
    }

    void UpdateWarmingUp()
    {
        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0f)
            StartDancing();
    }

    void StartDancing()
    {
        currentState = State.Dancing;
        stateTimer = Random.Range(timeDancingMin, timeDancingMax);

        if (animator != null)
        {
            animator.SetBool("IsWarmingUp", false);
            animator.SetBool("IsDancing", true);
        }

        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        Debug.Log(nombreNPC + " empieza a bailar");
    }

    void UpdateDancing()
    {
        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0f)
        {
            if (audioSource != null)
            {
                audioSource.Stop();
            }
            StartWalking();
        }
    }


    void ChooseNewDestination()
    {
        Vector3 punto = ObtenerPuntoAleatorio(transform.position, radiusMove);
        agent.SetDestination(punto);
    }

    Vector3 ObtenerPuntoAleatorio(Vector3 centro, float radio)
    {
        Vector3 dir = Random.insideUnitSphere * radio;
        dir += centro;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(dir, out hit, radio, NavMesh.AllAreas))
            return hit.position;

        return transform.position;
    }

    public override void OnInteract()
    {
        Debug.Log(nombreNPC + ": ¡baila conmigo!");
    }
}