using UnityEngine;
using UnityEngine.InputSystem;

public class Clue : MonoBehaviour
{
    public GameObject textClue;           
    public GameObject textInteraction;
    public InputActionReference viewClueAction;

    public string textoPromptVer = "Presiona X para ver la pista";
    public string textoPromptOcultar = "Presiona X para ocultar";

    private bool jugadorCerca = false;
    private bool mostrando = false;

    void Start()
    {
        if (textClue != null) textClue.SetActive(false);
    }

    void Update()
    {
        if (jugadorCerca && viewClueAction.action.WasPressedThisFrame())
        {
            if (!mostrando)
                MostrarTexto();
            else
                OcultarTexto();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            ActualizarPrompt();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            if (textInteraction != null)
            { 
                textInteraction.SetActive(false);
            }
        }
    }

    void MostrarTexto()
    {
        mostrando = true;
        if (textClue != null) textClue.SetActive(true);
        ActualizarPrompt();
    }

    void OcultarTexto()
    {
        mostrando = false;
        if (textClue != null) textClue.SetActive(false);
        ActualizarPrompt();
    }

    void ActualizarPrompt()
    {
        if (textInteraction == null) return;

        if (jugadorCerca)
        {
            textInteraction.SetActive(true);
            TMPro.TMP_Text tmp = textInteraction.GetComponent<TMPro.TMP_Text>();
            if (tmp != null)
                tmp.text = mostrando ? textoPromptOcultar : textoPromptVer;
        }
        else
        {
            textInteraction.SetActive(false);
        }
    }
}