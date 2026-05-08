using UnityEngine;

public class LetterButton : MonoBehaviour
{
    public string letra = "D";
    public OrderLettersGame manager;

    void OnMouseDown()
    {
        if (manager != null)
            manager.OnLetterClicked(letra);
    }

}