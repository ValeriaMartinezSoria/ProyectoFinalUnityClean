using UnityEngine;

public class HighlightOnLook : MonoBehaviour
{
    public Color colorResaltado = Color.green;

    private Renderer rend;
    private Color colorOriginal;

    void Start()
    {
        rend = GetComponent<Renderer>();

        
        if (rend.material.HasProperty("_BaseColor"))
            colorOriginal = rend.material.GetColor("_BaseColor");
        else if (rend.material.HasProperty("_Color"))
            colorOriginal = rend.material.color;
    }

    public void OnLookEnter()
    {
        if (rend.material.HasProperty("_BaseColor"))
            rend.material.SetColor("_BaseColor", colorResaltado);
        else if (rend.material.HasProperty("_Color"))
            rend.material.color = colorResaltado;
    }

    public void OnLookExit()
    {
        if (rend.material.HasProperty("_BaseColor"))
            rend.material.SetColor("_BaseColor", colorOriginal);
        else if (rend.material.HasProperty("_Color"))
            rend.material.color = colorOriginal;
    }
}