using UnityEngine;
using TMPro;


public class CodeLock3D : MonoBehaviour
{
    public TextMeshPro displayText;
    public string correctCode = "2580";
    public bool isLocked = false;

    private string input = "";

    public GameObject door;
    public AudioSource audioSource;

    public AudioClip buttonSound;
    public AudioClip correctSound;
    public AudioClip wrongSound;

    public MensajeMisionColiseo mensajeAlDesbloquear;

    [Header("Secuencia Final")]
    public GameObject botonFinalAActivar;

    public void PressButton(string value)
    {
        if (isLocked) 
        {  
            return; 
        }    

        audioSource.PlayOneShot(buttonSound);
        if (value == "C")
        {
            input = "";
        }
        else if (value == "E")
        {
            CheckCode();
            return;
        }
        else
        {
            if (input.Length < 4)
                input += value;
        }

        displayText.text = input;
    }

    void CheckCode()
    {
        Debug.Log("Ingresado: " + input);
        Debug.Log("Correcto: " + correctCode);

        if (input == correctCode)
        {
            audioSource.PlayOneShot(correctSound);
            Debug.Log("Código correcto");
            isLocked = true;
            OpenDoor();
        }
        else
        {
            audioSource.PlayOneShot(wrongSound);
            Debug.Log("Código incorrecto");
            input = "";
            displayText.text = "";
        }
    }

    public bool esAbrirOficina = false;

    void OpenDoor()
    {
        Debug.Log("OpenDoor() llamado. Desactivando puerta...");
        door.SetActive(false);


        if (AnomalyManager.Instance != null)
        {
            AnomalyManager.Instance.MostrarTextoAnomalias();
        }
        else
        {
            Debug.LogError("No se encontró el AnomalyManager en la escena.");
        }

        if (esAbrirOficina && OrganizeOfficeManager.Instance != null)
        {
            OrganizeOfficeManager.Instance.MostrarTextoPuzzle();
        }

        if (mensajeAlDesbloquear != null)
        {
            mensajeAlDesbloquear.Mostrar();
        }

        if (botonFinalAActivar != null)
        {
            botonFinalAActivar.SetActive(true);
        }
    }
}