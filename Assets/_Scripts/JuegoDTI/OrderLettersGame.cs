using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class OrderLettersGame : MonoBehaviour
{
    public string pregunta = "¿Cuál es el nombre de nuestra escuela?";
    private string[] ordenCorrecto = { "D", "T", "I" };

    public string mensajeGanador = "Perfecto! \n Ahora ve al letrero de UPB";
    private float duracionMensajeVictoria = 5f;

    public GameObject panelPregunta;
    public TMP_Text textoPregunta;
    public TMP_Text textoProgreso;

    public AudioSource audioSource;
    public AudioClip sonidoCorrecto;
    public AudioClip sonidoIncorrecto;
    public AudioClip sonidoVictoria;

    public Camera mainCamera;
    private float clickDistance = 100f;

    private int indiceActual = 0;
    private bool juegoCompleto = false;
    private bool jugando = false;

    void Start()
    {
        if (panelPregunta != null) panelPregunta.SetActive(false);
        ResetProgreso();

        if (mainCamera == null) mainCamera = Camera.main;
    }

    void Update()
    {
        if (!jugando || juegoCompleto) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
            DetectarClickEnLetra();
    }

    void DetectarClickEnLetra()
    {
        if (mainCamera == null) return;

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, clickDistance, ~0, QueryTriggerInteraction.Ignore))
        {

            LetterButton letter = hit.collider.GetComponent<LetterButton>();
            if (letter == null) letter = hit.collider.GetComponentInParent<LetterButton>();

            if (letter != null)
            {
                OnLetterClicked(letter.letra);
            }
        }
    }

    public void IniciarJuego()
    {
        if (juegoCompleto) return;

        jugando = true;
        if (panelPregunta != null) panelPregunta.SetActive(true);
        if (textoPregunta != null) textoPregunta.text = pregunta;

        ResetProgreso();
    }

    public void TerminarJuego()
    {
        if (juegoCompleto) return;

        jugando = false;
        if (panelPregunta != null) panelPregunta.SetActive(false);
    }

    public void OnLetterClicked(string letra)
    {
        if (juegoCompleto) return;

        if (letra == ordenCorrecto[indiceActual])
        {
            indiceActual++;
            ActualizarProgreso();

            if (audioSource != null && sonidoCorrecto != null)
                audioSource.PlayOneShot(sonidoCorrecto);

            if (indiceActual >= ordenCorrecto.Length)
                Ganar();
        }
        else
        {
            indiceActual = 0;
            ActualizarProgreso();

            if (audioSource != null && sonidoIncorrecto != null)
                audioSource.PlayOneShot(sonidoIncorrecto);
        }
    }

    void ActualizarProgreso()
    {
        if (textoProgreso == null) return;

        string progreso = "";
        for (int i = 0; i < ordenCorrecto.Length; i++)
        {
            if (i < indiceActual)
                progreso += ordenCorrecto[i] + " ";
            else
                progreso += "_ ";
        }
        textoProgreso.text = progreso.Trim();
    }

    void ResetProgreso()
    {
        indiceActual = 0;
        ActualizarProgreso();
    }

    void Ganar()
    {
        juegoCompleto = true;
        jugando = false;

        if (audioSource != null && sonidoVictoria != null)
        {
            audioSource.PlayOneShot(sonidoVictoria);
        }

        if (textoPregunta != null) textoPregunta.text = mensajeGanador;
        if (textoProgreso != null) textoProgreso.text = "";  

        Invoke(nameof(CerrarPanelVictoria), duracionMensajeVictoria);
    }

    void CerrarPanelVictoria()
    {
        if (panelPregunta != null) panelPregunta.SetActive(false);
    }

    public bool EstaCompleto()
    {
        return juegoCompleto;
    }
}