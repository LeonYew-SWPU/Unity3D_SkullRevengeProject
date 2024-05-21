using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WinUI_2 : MonoBehaviour
{
    public Button MainBtn;

    private void Start()
    {
        MainBtn.onClick.AddListener(LoadSceneMain);
    }

    void LoadSceneMain()
    {
        SceneManager.LoadScene("MainScene");
    }
    
}
