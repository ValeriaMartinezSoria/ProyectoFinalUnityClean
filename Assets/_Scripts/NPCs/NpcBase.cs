using UnityEngine;

public abstract class NpcBase : MonoBehaviour
{
    public string nombreNPC = "NPC";
    public Animator animator;
    public AudioSource audioSource;

    protected bool playerNear = false;
    protected Transform player;

    protected virtual void Start()
    {
        if (animator == null)
            animator = GetComponent<Animator>();
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = true;
            player = other.transform;
            OnPlayerEnter();
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNear = false;
            player = null;
            OnPlayerExit();
        }
    }

    protected virtual void OnPlayerEnter()
    {
        Debug.Log(nombreNPC + ": jugador se acerco");
    }

    protected virtual void OnPlayerExit()
    {
        Debug.Log(nombreNPC + ": jugador se alejo");
    }

    public abstract void OnInteract();
}