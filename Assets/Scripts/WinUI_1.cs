using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinUI_1 : MonoBehaviour
{
    public Button MainBtn;
    public Button NextBtn;

    private void Start()
    {
        MainBtn.onClick.AddListener(LoadSceneMain);
        NextBtn.onClick.AddListener(LoadSceneTwo);
    }

    void LoadSceneMain()
    {
        SceneManager.LoadScene("MainScene");
    }

    void LoadSceneTwo()
    {
        SceneManager.LoadScene("Scene_02");
    }
    
}
