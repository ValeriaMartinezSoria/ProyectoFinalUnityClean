using TMPro;
using UnityEngine;

public class GameTimer : MonoBehaviour
{
    public TMP_Text textTimer;

    private string loseScene = "LoseScene";
    private float timeRemaining;
    private bool timerEnds = false;

    public float totalTime = 180f;

    public AudioSource audioSource;
    public AudioClip sonidoFinal;
    private bool sonidoYaSonado = false;

    void Start()
    {
        timeRemaining = totalTime;
    }

    void Update()
    {
        if (timerEnds)
        {
            return;
        }

        timeRemaining -= Time.deltaTime;

        if (timeRemaining <= 0f)
        {
            timeRemaining = 0f;
            timerEnds = true;
            UnityEngine.SceneManagement.SceneManager.LoadScene(loseScene);
        }

        UpdateTime();

        if (timeRemaining <= 30f && !sonidoYaSonado)
        {
            if (audioSource != null && sonidoFinal != null)
            {
                audioSource.PlayOneShot(sonidoFinal);
            }

            sonidoYaSonado = true;
        }
    }

    void UpdateTime()
    {
        int minutes = Mathf.FloorToInt(timeRemaining / 60f);
        int seconds = Mathf.FloorToInt(timeRemaining % 60f);
        textTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (timeRemaining <= 25f)
        {
            textTimer.color = Color.red;
        }

    }
}