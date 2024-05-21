using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class PauseController : MonoBehaviour
{
    public UnityEvent GamePaused;

    public UnityEvent GameResumed;

    public Button resumeBtn;
    public Button exitBtn;
    public GameObject pauseMenu;

    private bool _isPaused;

    private void Start()
    {
        resumeBtn.onClick.AddListener(ResumeGame);
        exitBtn.onClick.AddListener(ExitGame);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            _isPaused = !_isPaused;

            if (_isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    void ResumeGame()
    {
        pauseMenu.GetComponent<CanvasGroup>().alpha = 0;
        Time.timeScale = 1;
        GameResumed.Invoke();
    }

    void ExitGame()
    {
        Application.Quit();
    }

    void PauseGame()
    {
        pauseMenu.GetComponent<CanvasGroup>().alpha = 1;
        Time.timeScale = 0;
        GamePaused.Invoke();
    }
}
