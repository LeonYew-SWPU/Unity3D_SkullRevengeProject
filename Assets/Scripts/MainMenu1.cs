using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Button FirstBtn;
    public Button SecondBtn;
    public Button ExitBtn;

    private void Start()
    {
        FirstBtn.onClick.AddListener(LoadSceneOne);
        SecondBtn.onClick.AddListener(LoadSceneTwo);
        ExitBtn.onClick.AddListener(GameExit);
    }

    void LoadSceneOne()
    {
        SceneManager.LoadScene("Scene_01");
    }

    void LoadSceneTwo()
    {
        SceneManager.LoadScene("Scene_02");
    }
    
    void GameExit()
    {
        Application.Quit();
    }
}
