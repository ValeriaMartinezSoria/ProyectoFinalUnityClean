using UnityEngine;
using System.Collections;
using TMPro;

public class MensajeMisionCafe : MonoBehaviour
{

    public TextMeshProUGUI texto;

    public string mensaje =
        "> EL CANDADO SE HA DESBLOQUEADO\n> RESUELVE EL ACERTIJO";

    public float typingSpeed = 0.08f;
    public float duracionEnPantalla = 5f;

    public AudioSource audioSource;
    public AudioClip typingSound;

    private bool mostrando = false;  

    void Start()
    {
        if (texto != null) texto.text = "";
    }

    public void Mostrar()
    {
        if (mostrando) return; 
        StartCoroutine(MostrarMensaje());
    }

    public void Mostrar(string mensajePersonalizado)
    {
        if (mostrando) return;
        mensaje = mensajePersonalizado;
        StartCoroutine(MostrarMensaje());
    }

    public void Cancelar()
    {
        StopAllCoroutines();
        mostrando = false;
        if (texto != null) texto.text = "";
    }

    IEnumerator MostrarMensaje()
    {
        mostrando = true;

        yield return StartCoroutine(TypeText(texto, mensaje));
        yield return new WaitForSeconds(duracionEnPantalla);

        if (texto != null) texto.text = "";
        mostrando = false;
    }

    IEnumerator TypeText(TextMeshProUGUI textoUI, string contenido)
    {
        textoUI.text = "";
        foreach (char letra in contenido)
        {
            textoUI.text += letra;

            if (typingSound != null && audioSource != null)
                audioSource.PlayOneShot(typingSound, 0.2f);

            if (letra == '.' || letra == '\n')
                yield return new WaitForSeconds(0.4f);
            else
                yield return new WaitForSeconds(typingSpeed);
        }
    }
}
