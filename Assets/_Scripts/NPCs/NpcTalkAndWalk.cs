using UnityEngine;
using UnityEngine.AI;

public class NpcTalkAndWalk : NpcBase
{
    private float radiusMove = 10f;
    private float awaitTimeMin = 2f;
    private float awaitTimeMax = 5f;
    private float radioDeteccionNPC = 1f;
    private float duracionConversacion = 15f;
    private float waitTimeAfterTalk = 5f;

    protected NavMeshAgent agent;

    private float awaitTime = 0f;
    private bool wait = false;

    private bool talking = false;
    private float timeTalking = 0f;
    private float cooldownAfterTalk = 0f;

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        StartMoving();
    }

    protected virtual void Update()
    {
        if (talking)
        {
            timeTalking -= Time.deltaTime;
            if (timeTalking <= 0f)
                FinishTalking();
            return;
        }

        if (cooldownAfterTalk > 0f)
            cooldownAfterTalk -= Time.deltaTime;

        UpdateMovement();

        if (cooldownAfterTalk <= 0f)
            LookNpc();
    }

    void UpdateMovement()
    {
        if (animator != null)
            animator.SetFloat("Speed", agent.velocity.magnitude);

        if (wait)
        {
            awaitTime -= Time.deltaTime;
            if (awaitTime <= 0f)
            {
                wait = false;
                StartMoving();
            }
            return;
        }

        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            wait = true;
            awaitTime = Random.Range(awaitTimeMin, awaitTimeMax);
        }
    }

    protected void StartMoving()
    {
        Vector3 puntoAleatorio = ObtenerPuntoAleatorio(transform.position, radiusMove);
        agent.SetDestination(puntoAleatorio);
    }

    Vector3 ObtenerPuntoAleatorio(Vector3 centro, float radio)
    {
        Vector3 direccionAleatoria = Random.insideUnitSphere * radio;
        direccionAleatoria += centro;

        NavMeshHit hit;

        if (NavMesh.SamplePosition(direccionAleatoria, out hit, radio, NavMesh.AllAreas))
        {
            return hit.position;
        }

        return transform.position;
    }

    void LookNpc()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, radioDeteccionNPC);
        foreach (Collider c in colliders)
        {
            if (c.gameObject == gameObject) continue;

            NpcTalkAndWalk other = c.GetComponent<NpcTalkAndWalk>();
            if (other != null && !other.IsTalking() && !other.IsInCooldown())
            {
                StartTalking(other);
                other.StartTalking(this);
                return;
            }
        }
    }

    public void StartTalking(NpcTalkAndWalk anotherNpc)
    {
        talking = true;
        timeTalking = duracionConversacion;
        if (agent != null)
        {
            agent.isStopped = true;
        }

        Vector3 dir = anotherNpc.transform.position - transform.position;
        dir.y = 0f;

        if (dir.sqrMagnitude > 0.001f)
        {
            transform.rotation = Quaternion.LookRotation(dir);
        }
        if (animator != null)
        {
            animator.SetFloat("Speed", 0f);
            animator.SetBool("IsTalking", true);
        }

        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }
        Debug.Log(nombreNPC + " conversa con " + anotherNpc.nombreNPC);
    }

    void FinishTalking()
    {
        talking = false;
        cooldownAfterTalk = waitTimeAfterTalk;

        if (agent != null)
        {
            agent.isStopped = false;
        }
        if (animator != null)
        {
            animator.SetBool("IsTalking", false);
        }
        if (audioSource != null)
        {
            audioSource.Stop();
        }

        StartMoving();
    }

    public bool IsTalking()
    {
        return talking;
    }

    public bool IsInCooldown()
    {
        return cooldownAfterTalk > 0f;
    }

    public override void OnInteract()
    {
        Debug.Log(nombreNPC + " está ocupado");
    }
}