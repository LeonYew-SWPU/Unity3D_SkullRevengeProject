using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DefeatUI : MonoBehaviour
{
    public Button MainBtn;
    public Button AgainBtn;
    public string CurrentScene;

    private void Start()
    {
        MainBtn.onClick.AddListener(LoadSceneMain);
        AgainBtn.onClick.AddListener(LoadSceneAgain);
    }

    void LoadSceneMain()
    {
        SceneManager.LoadScene("MainScene");
    }

    void LoadSceneAgain()
    {
        SceneManager.LoadScene(CurrentScene);
    }
    
}
