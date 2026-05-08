using UnityEngine;
using TMPro;

public class AnomalyManager : MonoBehaviour
{
    public static AnomalyManager Instance; 

    public TMP_Text textAnomalies;
    public int totalAnomalies = 10;
    
  
    public GameObject wallToDisappear; 
    public GameObject objectToPassThrough;

   
    public AudioSource audioSource;
    public AudioClip doorOpenSound;

    public MensajeMisionCafe mensajeMision2;

    private int currentAnomalies = 0;

    void Awake()
    {
        
        Instance = this;
    }

    void Start()
    {
        Debug.Log("AnomalyManager iniciado.");
        
        
        if (textAnomalies != null)
        {
            textAnomalies.gameObject.SetActive(false); 
        }

        UpdateUI();
    }

    
    public void MostrarTextoAnomalias()
    {
        if (currentAnomalies >= totalAnomalies) return; 

        if (textAnomalies != null)
        {
            textAnomalies.gameObject.SetActive(true);
            textAnomalies.transform.SetAsLastSibling(); 
            Debug.Log("¡El AnomalyManager encendió el texto con éxito!");
        }
    }

    public void AddAnomaly()
    {
        currentAnomalies++;
        Debug.Log("Anomalía recolectada. Total actual: " + currentAnomalies);
        UpdateUI();

        if (currentAnomalies >= totalAnomalies)
        {
            Debug.Log("¡Todas las anomalías recolectadas!");

           
            if (wallToDisappear != null)
            {
                wallToDisappear.SetActive(false);
            }

           
            if (objectToPassThrough != null)
            {
                Collider col = objectToPassThrough.GetComponent<Collider>();
                if (col != null)
                {
                    col.enabled = false;
                }
            }

            
            if (textAnomalies != null)
            {
                textAnomalies.gameObject.SetActive(false);
            }

           
            if (audioSource != null && doorOpenSound != null)
            {
                audioSource.PlayOneShot(doorOpenSound);
            }

            if (mensajeMision2 != null)
            {
                mensajeMision2.Mostrar();
            }
        }
    }

    void UpdateUI()
    {
        if (textAnomalies != null)
        {
            textAnomalies.text = "Anomalías: " + currentAnomalies + " / " + totalAnomalies;
            Debug.Log("UI Actualizada a: " + textAnomalies.text);
        }
        else
        {
            Debug.LogWarning("¡ATENCIÓN! textAnomalies está vacío en AnomalyManager. ¡Arrastra el texto desde la Jerarquía!");
        }
    }
}