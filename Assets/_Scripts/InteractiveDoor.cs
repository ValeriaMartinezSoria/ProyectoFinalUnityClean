using UnityEngine;
using UnityEngine.InputSystem;

public class DoorInteractive : MonoBehaviour
{
    public float anguloApertura = 90f;
    public float velocidad = 2f;
    public InputActionReference openDoorAction;
    public GameObject textInteraction;
    public string textoPrompt = "Presiona E para abrir";

    private bool jugadorCerca = false;
    private bool estaAbierta = false;
    private Quaternion rotacionCerrada;
    private Quaternion rotacionAbierta;
    private Quaternion rotacionObjetivo;

    void Start()
    {
        rotacionCerrada = transform.rotation;
        rotacionAbierta = rotacionCerrada * Quaternion.Euler(0, anguloApertura, 0);
        rotacionObjetivo = rotacionCerrada;

        if (textInteraction != null)
            textInteraction.SetActive(false);
    }

    void Update()
    {
        if (jugadorCerca && openDoorAction.action.WasPressedThisFrame())
        {
            AlternarPuerta();
        }

        transform.rotation = Quaternion.Slerp(transform.rotation, rotacionObjetivo, Time.deltaTime * velocidad);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = true;
            if (textInteraction != null)
                textInteraction.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorCerca = false;
            if (textInteraction != null)
                textInteraction.SetActive(false);
        }
    }

    void AlternarPuerta()
    {
        estaAbierta = !estaAbierta;
        rotacionObjetivo = estaAbierta ? rotacionAbierta : rotacionCerrada;
    }

    void OnMouseDown()
    {
        if (jugadorCerca)
            AlternarPuerta();
    }
}