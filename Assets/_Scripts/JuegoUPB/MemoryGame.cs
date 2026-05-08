using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using System.Collections;
using System.Collections.Generic;

public class MemoryGame : MonoBehaviour
{
    private int totalNiveles = 3;
    private int letrasIniciales = 2;
    private float tiempoEntreLetras = 0.7f;
    private float duracionBrillo = 0.5f;    

    public MemoryLetter[] letras;  

    public GameObject panelJuego;
    public TMP_Text textoEstado;

    public string mensajeRepetir = "¡Repite la secuencia!";
    public string mensajeAcierto = "¡Correcto!";
    public string mensajeFallo = "Fallaste. Reiniciando...";
    public string mensajeVictoria = "Ahora ve a la sala dee descanso";
    private float duracionMensajeVictoria = 5f;

    public AudioClip sonidoBrillo;
    public AudioClip sonidoCorrecto;
    public AudioClip sonidoFallo;
    public AudioClip sonidoVictoria;

    [Header("Al Ganar")]
    public GameObject objetoADesaparecer;

    public Camera mainCamera;
    private float clickDistance = 100f;

    private List<int> secuenciaActual = new List<int>();  
    private int indiceJugador = 0;
    private int nivelActual = 0;
    private bool jugando = false;
    private bool mostrandoSecuencia = false;
    private bool juegoCompleto = false;

    void Start()
    {
        if (panelJuego != null) panelJuego.SetActive(false);
        if (mainCamera == null) mainCamera = Camera.main;
    }

    void Update()
    {
        if (!jugando || juegoCompleto || mostrandoSecuencia) return;

        if (Mouse.current.leftButton.wasPressedThisFrame)
            DetectarClickEnLetra();
    }

    public void IniciarJuego()
    {
        if (juegoCompleto) return;

        jugando = true;
        nivelActual = 0;
        if (panelJuego != null) panelJuego.SetActive(true);

        StartCoroutine(EmpezarSiguienteNivel());
    }

    public void TerminarJuego()
    {
        if (juegoCompleto) return;

        jugando = false;
        StopAllCoroutines();
        if (panelJuego != null) panelJuego.SetActive(false);

        foreach (var l in letras)
            l.Apagar();
    }

    IEnumerator EmpezarSiguienteNivel()
    {
        nivelActual++;

        if (textoEstado != null)
            textoEstado.text = "Nivel " + nivelActual + " de " + totalNiveles + "\n";

        GenerarSecuencia(letrasIniciales + nivelActual - 1);

        yield return new WaitForSeconds(1.5f);

        yield return StartCoroutine(MostrarSecuencia());

        if (textoEstado != null)
            textoEstado.text = mensajeRepetir;

        indiceJugador = 0;
    }

    void GenerarSecuencia(int cantidad)
    {
        secuenciaActual.Clear();
        for (int i = 0; i < cantidad; i++)
            secuenciaActual.Add(Random.Range(0, letras.Length));
    }

    IEnumerator MostrarSecuencia()
    {
        mostrandoSecuencia = true;

        foreach (int idx in secuenciaActual)
        {
            letras[idx].Encender();

            if (sonidoBrillo != null)
                AudioSource.PlayClipAtPoint(sonidoBrillo, transform.position);

            yield return new WaitForSeconds(duracionBrillo);

            letras[idx].Apagar();

            yield return new WaitForSeconds(tiempoEntreLetras);
        }

        mostrandoSecuencia = false;
    }

    void DetectarClickEnLetra()
    {
        if (mainCamera == null) return;

        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, clickDistance, ~0, QueryTriggerInteraction.Ignore))
        {
            MemoryLetter letter = hit.collider.GetComponent<MemoryLetter>();
            if (letter == null) letter = hit.collider.GetComponentInParent<MemoryLetter>();

            if (letter != null)
                OnLetterClicked(letter);
        }
    }

    public void OnLetterClicked(MemoryLetter letter)
    {
        if (juegoCompleto || mostrandoSecuencia) return;

        StartCoroutine(BrilloCorto(letter));

        int indiceClickeado = System.Array.IndexOf(letras, letter);
        if (indiceClickeado < 0) return;

        if (indiceClickeado == secuenciaActual[indiceJugador])
        {
            indiceJugador++;

            if (sonidoCorrecto != null)
                AudioSource.PlayClipAtPoint(sonidoCorrecto, transform.position);

            if (indiceJugador >= secuenciaActual.Count)
            {
                if (nivelActual >= totalNiveles)
                    Ganar();
                else
                    StartCoroutine(SiguienteNivelConDelay());
            }
        }
        else
        {
            Fallar();
        }
    }

    IEnumerator BrilloCorto(MemoryLetter letter)
    {
        letter.Encender();
        yield return new WaitForSeconds(0.2f);
        letter.Apagar();
    }

    IEnumerator SiguienteNivelConDelay()
    {
        if (textoEstado != null) textoEstado.text = mensajeAcierto;
        yield return new WaitForSeconds(1.5f);
        yield return StartCoroutine(EmpezarSiguienteNivel());
    }

    void Fallar()
    {
        if (sonidoFallo != null)
            AudioSource.PlayClipAtPoint(sonidoFallo, transform.position);

        if (textoEstado != null) textoEstado.text = mensajeFallo;

        StartCoroutine(ReiniciarNivel());
    }

    IEnumerator ReiniciarNivel()
    {
        yield return new WaitForSeconds(2f);

        nivelActual--;  
        yield return StartCoroutine(EmpezarSiguienteNivel());
    }

    void Ganar()
    {
        juegoCompleto = true;
        jugando = false;

        if (sonidoVictoria != null)
            AudioSource.PlayClipAtPoint(sonidoVictoria, transform.position);

        if (textoEstado != null) textoEstado.text = mensajeVictoria;

        if (objetoADesaparecer != null)
        {
            objetoADesaparecer.SetActive(false);
        }

        Invoke(nameof(CerrarPanel), duracionMensajeVictoria);
    }

    void CerrarPanel()
    {
        if (panelJuego != null) panelJuego.SetActive(false);
    }

    public bool EstaCompleto()
    {
        return juegoCompleto;
    }
}