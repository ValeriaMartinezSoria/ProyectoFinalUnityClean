using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class IntroVideo : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
        videoPlayer.loopPointReached += FinDelVideo;
    }

    void FinDelVideo(VideoPlayer vp)
    {
        SceneManager.LoadScene("PrincipalScene");
    }
}