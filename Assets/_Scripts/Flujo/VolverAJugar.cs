using UnityEngine;
using UnityEngine.SceneManagement;

public class VolverAJugar : MonoBehaviour
{
    void Start()
    {
   
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ReiniciarJuego()
    {
      
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene("PrincipalScene");
    }
}