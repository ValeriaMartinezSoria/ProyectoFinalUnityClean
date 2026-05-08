using UnityEngine;
using System.Collections;
using TMPro;

public class MensajeInicial : MonoBehaviour
{
 
    public TextMeshProUGUI texto;

    
    [TextArea]
    public string mensaje =
    "> EL TIEMPO CORRE...\n> ENCUENTRA EL CÓDIGO DE 4 DÍGITOS";

 
    public float typingSpeed = 0.08f; 
    public float duracionEnPantalla = 5f;

   
    public AudioSource audioSource;
    public AudioClip typingSound;

    void Start()
    {
        if (texto != null) texto.text = "";
        StartCoroutine(MostrarMensaje());
    }

    IEnumerator MostrarMensaje()
    {
        yield return StartCoroutine(TypeText(texto, mensaje));

        yield return new WaitForSeconds(duracionEnPantalla);

        if (texto != null)
            texto.text = "";
    }

    IEnumerator TypeText(TextMeshProUGUI textoUI, string contenido)
    {
        textoUI.text = "";

        foreach (char letra in contenido)
        {
            textoUI.text += letra;


            if (typingSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(typingSound, 0.2f);
            }

           
            if (letra == '.' || letra == '\n')
            {
                yield return new WaitForSeconds(0.4f); 
            }
            else
            {
                yield return new WaitForSeconds(typingSpeed);
            }
        }
    }
}