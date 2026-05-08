using UnityEngine;

public class AnomalyInteract : MonoBehaviour
{
    public AnomalyManager manager;
    public AudioClip pickupSound;

    private void OnMouseDown() 
    {
        Debug.Log("OnMouseDown() detectado en: " + gameObject.name);

        if (manager != null)
        {
            manager.AddAnomaly();
        }
        else
        {
            Debug.LogWarning("¡ATENCIÓN! La variable 'manager' está vacía en la anomalía: " + gameObject.name);
        }

       
        if (pickupSound != null)
        {
            AudioSource.PlayClipAtPoint(pickupSound, transform.position);
        }

        Destroy(gameObject);
    }
}