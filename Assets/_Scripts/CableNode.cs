using UnityEngine;

public class CableNode : MonoBehaviour
{
    [Header("Configuración del Nodo")]
    public string cableID;

    void OnMouseDown()
    {
        if (CableManager.Instance != null)
        {
            CableManager.Instance.SelectNode(this);
        }
        else
        {
            Debug.LogError("No se encontró el objeto o script CableManager en la escena.");
        }
    }
}
