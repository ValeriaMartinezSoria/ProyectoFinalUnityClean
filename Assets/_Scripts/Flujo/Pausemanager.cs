using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
public class Pausemanager : MonoBehaviour
{
    public static Pausemanager Instance { get; private set; }
    public GameObject pauseMenu;
    public InputActionReference escButton;
    private bool isGamePaused = false;
    public void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            
        }
    }
    void Start()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (escButton.action.WasPressedThisFrame())
        {
            if (isGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }
    private void PauseGame()
    {
        isGamePaused = true;
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }
    private void ResumeGame()
    {
        isGamePaused = false;
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    public void OnResumeButton()
    {
        ResumeGame();
    }
    public void OnExitButton()
    {
        Application.Quit();
    }
    public void OnVolumeButton(float volume)
    {
        AudioListener.volume = volume;
    }

}