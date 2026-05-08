using UnityEngine;
using UnityEngine.InputSystem;

public class OrderLettersTrigger : MonoBehaviour
{
    public string textoPrompt = "Presiona E para resolver el acertijo";
    public GameObject promptInteraccion;
    public OrderLettersGame game;
    public InputActionReference interactAction;

    private bool playerNear = false;
    private bool jugando = false;

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
            
            if (game == null)
            {
                return;
            }

            if (game.EstaCompleto())
            {
                return;
            }

            if (!jugando)
            {
                IniciarJuego();
            }
            else
            {
                TerminarJuego();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            if (game == null)
            {
                return;
            }

            if (game.EstaCompleto())
            {
                return;
            }

            playerNear = true;

            if (promptInteraccion != null)
            {
                TMPro.TMP_Text tmp = promptInteraccion.GetComponent<TMPro.TMP_Text>();
                if (tmp != null)
                {
                    tmp.text = textoPrompt;
                }
                promptInteraccion.SetActive(true);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            playerNear = false;

            if (promptInteraccion != null)
            {
                promptInteraccion.SetActive(false);
            }

            if (jugando)
            {
                TerminarJuego();
            }
        }
    }

    void IniciarJuego()
    {
        jugando = true;
        if (promptInteraccion != null)
        {
            promptInteraccion.SetActive(false);
        }

        game.IniciarJuego();
    }

    void TerminarJuego()
    {
        jugando = false;
        game.TerminarJuego();
        if (playerNear && promptInteraccion != null)
        {
            promptInteraccion.SetActive(true);
        }
    }
}