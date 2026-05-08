using UnityEngine;

public class KeypadButton : MonoBehaviour
{
    public string value;
    public CodeLock3D keypad;

    void OnMouseDown()
    {
        keypad.PressButton(value);
    }
}