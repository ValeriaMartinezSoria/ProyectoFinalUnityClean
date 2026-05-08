using UnityEngine;

public class OrganizeObjectClick : MonoBehaviour
{
 
    public OrganizeOfficeManager manager;

   
    public AudioClip orderSound;

    private void OnMouseDown() 
    {
        Debug.Log("OnMouseDown() detectado en: " + gameObject.name);

        if (manager != null)
        {
            manager.AddOrderedObject();
        }
        else
        {
            Debug.LogWarning("¡ATENCIÓN! La variable 'manager' está vacía en: " + gameObject.name);
        }

      
        if (orderSound != null)
        {
            AudioSource.PlayClipAtPoint(orderSound, transform.position);
        }

        Destroy(gameObject);
    }
}