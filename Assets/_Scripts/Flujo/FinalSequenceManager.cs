using System.Collections;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement; 

public class FinalSequenceManager : MonoBehaviour
{
    public static FinalSequenceManager Instance;

    [Header("Animación Estante")]
    public Transform estante;
    public Transform posicionFinalEstante; 
    public float velocidadSubida = 1.5f;

    [Header("Proyector y Video")]
    public GameObject luzProyector;
    public VideoPlayer reproductorVideo; 
    public GameObject pantallaVideo; 

    [Header("Escena Final")]
    public string winSceneName = "WinScene"; 

    public AudioSource musicaFondo;
    private bool secuenciaIniciada = false;
    public AudioSource musicaVideo;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        
        if (luzProyector != null) luzProyector.SetActive(false);
        if (pantallaVideo != null) pantallaVideo.SetActive(false);
    }

   
    public void IniciarSubidaEstante()
    {
        if (secuenciaIniciada) return;
        secuenciaIniciada = true;

        StartCoroutine(MoverEstante());
    }

    private IEnumerator MoverEstante()
    {
        
        while (Vector3.Distance(estante.position, posicionFinalEstante.position) > 0.01f)
        {
            estante.position = Vector3.MoveTowards(estante.position, posicionFinalEstante.position, velocidadSubida * Time.deltaTime);
            yield return null;
        }
    }

   
    public void ActivarProyector()
    {

        if (luzProyector != null) luzProyector.SetActive(true);
        if (pantallaVideo != null) pantallaVideo.SetActive(true);
        if (musicaFondo != null)
        {
            musicaFondo.Stop();
        }
        if (musicaVideo != null)
        {
            musicaVideo.Play();
        }
        if (reproductorVideo != null)
        {
           
            reproductorVideo.loopPointReached += OnVideoFinished;
            reproductorVideo.Play();
        }

        Debug.Log("¡Secuencia Final: Video Reproduciéndose!");
    }

    
    void OnVideoFinished(VideoPlayer vp)
    {
        Debug.Log("Video terminado, cargando escena...");
        SceneManager.LoadScene(winSceneName);
    }
}