using UnityEngine;

public class FinalButton : MonoBehaviour
{
    private bool isPressed = false;
    public GameTimer gameTimer; 
    void OnMouseDown()
    {
        if (isPressed) return;
        isPressed = true;

        if (FinalSequenceManager.Instance != null)
        {
            
            if (gameTimer != null)
            {
                gameTimer.enabled = false; 
            }

            
            FinalSequenceManager.Instance.IniciarSubidaEstante();
            Debug.Log("Botón final presionado: El estante está subiendo y el tiempo se detuvo.");
        }
        else
        {
            Debug.LogError("No hay FinalSequenceManager en la escena.");
        }
    }
}