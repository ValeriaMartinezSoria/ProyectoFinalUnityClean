using UnityEngine;
using UnityEngine.AI;

public class NpcHurried : NpcBase
{
    private float radiusMove = 15f;
    private float speedRunning = 5f;        
    private float speedPhoneWalking = 1.5f; 

    private float timeRunningMin = 8f;      
    private float timeRunningMax = 15f;
    private float timePhoneTalkingMin = 5f; 
    private float timePhoneTalkingMax = 10f;

    protected NavMeshAgent agent;
    private bool phoneTalking = false;
    private float stateTimer = 0f;

    protected override void Start()
    {
        base.Start();
        agent = GetComponent<NavMeshAgent>();
        StartRunning();
    }

    void Update()
    {
        if (animator != null)
        {
            animator.SetFloat("Speed", agent.velocity.magnitude);
        }
        if (!agent.pathPending && agent.remainingDistance <= agent.stoppingDistance)
        {
            ChooseNewDestination();
        }
        stateTimer -= Time.deltaTime;
        if (stateTimer <= 0f)
        {
            if (phoneTalking)
            {
                StartRunning();
            }
            else
            {
                StartPhoneTalking();
            }
        }
    }

    void StartRunning()
    {
        phoneTalking = false;
        stateTimer = Random.Range(timeRunningMin, timeRunningMax);

        if (agent != null)
        {
            agent.speed = speedRunning;
        }
        if (animator != null)
        {
            animator.SetBool("IsPhoneTalking", false);
        }

        ChooseNewDestination();
        Debug.Log(nombreNPC + " empieza a correr");
    }

    void StartPhoneTalking()
    {
        phoneTalking = true;
        stateTimer = Random.Range(timePhoneTalkingMin, timePhoneTalkingMax);

        if (agent != null)
        {
            agent.speed = speedPhoneWalking;
        }
        if (animator != null)
        {
            animator.SetBool("IsPhoneTalking", true);
        }

        if (audioSource != null && !audioSource.isPlaying)
        {
            audioSource.Play();
        }

        Debug.Log(nombreNPC + " empieza a hablar por teléfono");
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
        {
            return hit.position;
        }
        return transform.position;
    }

    public override void OnInteract()
    {
        Debug.Log(nombreNPC + ": estoy apurado, no puedo hablar");
    }
}