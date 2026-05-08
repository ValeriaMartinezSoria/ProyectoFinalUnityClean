using UnityEngine;
using TMPro;

public class OrganizeOfficeManager : MonoBehaviour
{
    public static OrganizeOfficeManager Instance; 

    [Header("Configuración del Puzzle")]
    [Tooltip("La cantidad total de objetos que el jugador debe encontrar y cliquear.")]
    public int totalObjectsToOrder = 5;
    
    [Header("UI (Interfaz)")]
    public TMP_Text counterText;

    [Header("Recompensas al terminar")]
    [Tooltip("Pared que desaparecerá cuando el jugador termine de limpiar la sala.")]
    public GameObject wallToDisappear; 

    public AudioSource audioSource;
    public AudioClip completadoSound;

    [Header("Objetos Ordenados (Opcional)")]
    [Tooltip("Arrastra aquí un modelo de todos los objetos ya ordenados en el estante (que inicia apagado) para que aparezca al final")]
    public GameObject orderlyObjectsModel;

    private int currentOrdered = 0;

    void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        Debug.Log("OrganizeOfficeManager iniciado.");
        if (orderlyObjectsModel != null)
        {
            orderlyObjectsModel.SetActive(false); 
        }

        if (counterText != null)
        {
            counterText.gameObject.SetActive(false); 
            UpdateUI();
        }
    }

    public void MostrarTextoPuzzle()
    {
        if (currentOrdered >= totalObjectsToOrder) return;

        if (counterText != null)
        {
            counterText.gameObject.SetActive(true);
            Debug.Log("Texto del Puzzle de Oficina Activado.");
        }
    }

    public void AddOrderedObject()
    {
        currentOrdered++;
        Debug.Log("Objeto ordenado. Total actual: " + currentOrdered + " / " + totalObjectsToOrder);
        
        UpdateUI();

        if (currentOrdered >= totalObjectsToOrder)
        {
            Debug.Log("¡Todos los objetos de la oficina han sido ordenados!");

           
            if (counterText != null)
            {
                counterText.gameObject.SetActive(false);
            }

         
            if (wallToDisappear != null)
            {
                wallToDisappear.SetActive(false);
            }

           
            if (orderlyObjectsModel != null)
            {
                orderlyObjectsModel.SetActive(true);
            }

            
            if (audioSource != null && completadoSound != null)
            {
                audioSource.PlayOneShot(completadoSound);
            }
        }
    }

    private void UpdateUI()
    {
        if (counterText != null)
        {
            counterText.text = "Cosas: " + currentOrdered + " / " + totalObjectsToOrder;
        }
    }
}