using UnityEngine;

public class TrophyInteract : MonoBehaviour
{
    private bool interacted = false;

    
    void OnMouseDown()
    {
        if (interacted) return;
        interacted = true;

        if (FinalSequenceManager.Instance != null)
        {
            FinalSequenceManager.Instance.ActivarProyector();
        }
        else
        {
            Debug.LogError("No hay FinalSequenceManager en la escena.");
        }
    }
}