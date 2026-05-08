using UnityEngine;
using UnityEngine.SceneManagement;

public class JugarAhora : MonoBehaviour
{
    void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

  
    public void EmpezarJuego()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene("Intro2Scene");
    }

 
    public void Siguiente()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        SceneManager.LoadScene("All"); 
    }
}