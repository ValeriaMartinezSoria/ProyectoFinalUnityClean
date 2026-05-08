using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Helper : NpcBase
{
    public string mensaje = "Hola estudiante, tu misión es buscar pistas para salir del aula";
    public string interaction = "Presiona E para hablar";
    public GameObject textInteraction;
    public GameObject panelDialog;
    public TMP_Text textDialog;
    public GameObject talkHelperWarning;

    public InputActionReference interactAction;

    private float velocidadRotacion = 5f;
    private bool talking = false;

    void OnEnable()
    {
        if (interactAction != null)
        {
            interactAction.action.Enable();
        }
    }

    void OnDisable()
    {
        if (interactAction != null)
        {
            interactAction.action.Disable();
        }
    }

    void Update()
    {
        if (playerNear && interactAction != null && interactAction.action.WasPressedThisFrame())
        {
            OnInteract();
        }
        if (talking && player != null)
        {
            LookPlayer();
        }
    }

    protected override void OnPlayerEnter()
    {
        base.OnPlayerEnter();

        TMP_Text tmp = textInteraction.GetComponent<TMP_Text>();
        if (tmp != null)
        {
            tmp.text = interaction;
        }
        textInteraction.SetActive(true);

        if (talkHelperWarning != null)
        {
            talkHelperWarning.SetActive(true);
        }
    }

    protected override void OnPlayerExit()
    {
        base.OnPlayerExit();

        textInteraction.SetActive(false);

        if (talkHelperWarning != null)
        {
            talkHelperWarning.SetActive(false);
        }

        if (talking)
        {
            FinishTalking();
        }
    }

    public override void OnInteract()
    {
        if (!talking)
        {
            StartTalking();
        }
        else
        {
            FinishTalking();
        }
    }

    void StartTalking()
    {
        talking = true;
        textInteraction.SetActive(false);
        panelDialog.SetActive(true);
        textDialog.text = mensaje;

        if (animator != null)
        {
            animator.SetBool("IsTalking", true);
        }
        if (audioSource != null)
        {
            audioSource.Play();
        }
        if (talkHelperWarning != null)
        {
            talkHelperWarning.SetActive(false);
        }
    }

    void FinishTalking()
    {
        talking = false;
        panelDialog.SetActive(false);
        if (playerNear)
        {
            textInteraction.SetActive(true);
        }
        if (animator != null)
        {
            animator.SetBool("IsTalking", false);
        }
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }

    void LookPlayer()
    {
        Vector3 direccion = player.position - transform.position;
        direccion.y = 0f;
        if (direccion.sqrMagnitude > 0.001f)
        {
            Quaternion rotacionObjetivo = Quaternion.LookRotation(direccion);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo,
                                                   velocidadRotacion * Time.deltaTime);
        }
    }
}