using UnityEngine;

public class MemoryLetter : MonoBehaviour
{
    public Color colorBrillo = Color.yellow;
    public float intensidadEmision = 2f;

    private Renderer[] renderers;
    private Material[] materialesInstancia;
    private Color[] coloresOriginales;

    void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();

       
        materialesInstancia = new Material[renderers.Length];
        coloresOriginales = new Color[renderers.Length];

        for (int i = 0; i < renderers.Length; i++)
        {
            materialesInstancia[i] = renderers[i].material;
            coloresOriginales[i] = materialesInstancia[i].color;
        }
    }

    public void Encender()
    {
        for (int i = 0; i < materialesInstancia.Length; i++)
        {
            if (materialesInstancia[i] == null) continue;

            materialesInstancia[i].color = colorBrillo;

            if (materialesInstancia[i].HasProperty("_EmissionColor"))
            {
                materialesInstancia[i].EnableKeyword("_EMISSION");
                materialesInstancia[i].SetColor("_EmissionColor", colorBrillo * intensidadEmision);
            }
        }
    }

    public void Apagar()
    {
        for (int i = 0; i < materialesInstancia.Length; i++)
        {
            if (materialesInstancia[i] == null) continue;

            materialesInstancia[i].color = coloresOriginales[i];

            if (materialesInstancia[i].HasProperty("_EmissionColor"))
                materialesInstancia[i].SetColor("_EmissionColor", Color.black);
        }
    }
}