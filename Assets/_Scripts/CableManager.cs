using System.Collections;
using UnityEngine;

public class CableManager : MonoBehaviour
{
    public static CableManager Instance;

    [Header("Referencias")]
    public GameObject cablePrefab;

    [Header("Configuración")]
    public float animationSpeed = 2f;

    [Header("Audio y Victoria")]
    public AudioSource audioSource;
    public AudioClip finalVictorySound;
    public AudioClip singleConnectSound;

    public int totalCablesRequired = 3;
    private int connectedCount = 0;

    private CableNode firstSelectedNode;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void SelectNode(CableNode node)
    {
        if (firstSelectedNode == null)
        {
            firstSelectedNode = node;
            Debug.Log($"Primer nodo seleccionado: {node.name} (Color: {node.cableID})");
        }
        else
        {
            if (firstSelectedNode == node)
            {
                firstSelectedNode = null;
                Debug.Log("Selección cancelada");
                return;
            }

            if (firstSelectedNode.cableID == node.cableID)
            {
                Debug.Log($"¡Conexión correcta de color {node.cableID}!");
                
                StartCoroutine(DrawLineAnimated(firstSelectedNode.transform.position, node.transform.position, node.cableID));

                firstSelectedNode.gameObject.SetActive(false);
                node.gameObject.SetActive(false);

                connectedCount++;
                if (audioSource != null && singleConnectSound != null)
                {
                    audioSource.PlayOneShot(singleConnectSound);
                }

                if (connectedCount >= totalCablesRequired)
                {
                    Debug.Log("¡Minijuego de cables completado!");
                    if (audioSource != null && finalVictorySound != null)
                    {
                        audioSource.clip = finalVictorySound;
                        audioSource.loop = true;
                        audioSource.Play();
                    }
                }
            }
            else
            {
                Debug.Log($"Conexión incorrecta: intentaste unir {firstSelectedNode.cableID} con {node.cableID}");
            }

            firstSelectedNode = null;
        }
    }

    private IEnumerator DrawLineAnimated(Vector3 start, Vector3 end, string cableID)
    {
        GameObject lineObj = Instantiate(cablePrefab);
        LineRenderer lineRenderer = lineObj.GetComponent<LineRenderer>();

        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, start);

        Color cableColor = GetColorFromString(cableID);
        lineRenderer.startColor = cableColor;
        lineRenderer.endColor = cableColor;
        lineRenderer.material.color = cableColor;

        float progress = 0f;
        while (progress < 1f)
        {
            progress += Time.deltaTime * animationSpeed;
            Vector3 currentEndPos = Vector3.Lerp(start, end, progress);
            lineRenderer.SetPosition(1, currentEndPos);
            yield return null;
        }

        lineRenderer.SetPosition(1, end);
    }

    private Color GetColorFromString(string id)
    {
        switch (id.ToLower())
        {
            case "rojo": return Color.red;
            case "azul": return Color.blue;
            case "amarillo": return Color.yellow;
            default: return Color.white;
        }
    }
}
