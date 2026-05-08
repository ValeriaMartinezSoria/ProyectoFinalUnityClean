using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class IntroHistoria : MonoBehaviour
{
    public TextMeshProUGUI textoUI;

    [TextArea]
    public string[] frases;

    public float velocidadTyping = 0.05f;
    public float pausaEntreFrases = 2f;

    public AudioSource audioSource;
    public AudioClip sonidoTecla;

    public string siguienteEscena = "Controles"; 

    private Coroutine historiaCoroutine;
    private bool omitir = false;

    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        historiaCoroutine = StartCoroutine(ReproducirHistoria());
    }

    public void Omitir()
    {
        omitir = true;

        if (historiaCoroutine != null)
        {
            StopCoroutine(historiaCoroutine);
        }

        CargarEscena(); 
    }

    IEnumerator ReproducirHistoria()
    {
        foreach (string frase in frases)
        {
            if (omitir) yield break;

            yield return StartCoroutine(EscribirFrase(frase));

            if (omitir) yield break;

            yield return new WaitForSeconds(pausaEntreFrases);
            textoUI.text = "";
        }

        CargarEscena(); 
    }

    IEnumerator EscribirFrase(string frase)
    {
        textoUI.text = "";

        foreach (char letra in frase)
        {
            if (omitir) yield break;

            textoUI.text += letra;

            if (audioSource != null && sonidoTecla != null)
            {
                audioSource.PlayOneShot(sonidoTecla, 0.2f);
            }

            yield return new WaitForSeconds(velocidadTyping);
        }
    }

    void CargarEscena()
    {
        SceneManager.LoadScene(siguienteEscena);
    }
}