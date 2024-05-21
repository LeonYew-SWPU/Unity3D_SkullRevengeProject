using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnding_2 : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private GameObject endingPlayer;

    [SerializeField]
    private GameObject winPoint;

    [SerializeField]
    private GameObject defeatPoint;

    [SerializeField]
    private float blackTime; //黑幕转场时间

    [SerializeField]
    private CanvasGroup blackCanvas; //黑幕对象

    [SerializeField]
    private GameObject followShoot; //跟随镜头

    private bool? isWin;
    private bool? isDefeated;

    float m_Timer;

    private void Start()
    {
        isWin = null;
        isDefeated = null;
    }

    private void Update()
    {
        if (isWin == true)
        {
            EndLevel(true);
        }
        else if(isDefeated == true)
        {
            EndLevel(false);
        }
    }

    public void DropDefeated()
    {
        // 游戏失败
        isDefeated = true;
        print("isDefeated");
        EndLevel(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            // 游戏胜利
            isWin = true;
            print("Win!!!");
            EndLevel(true);
        }
    }

    private void EndLevel(bool isWinEnding)
    {



        if (isWinEnding)
        {
            // 加载成功场景
            SceneManager.LoadScene("Scene_02_Win");

        }
        else
        {
            // 加载失败场景
            SceneManager.LoadScene("Scene_02_Defeat");
        }
    }

    IEnumerator WaitDarkScene(float blackTime)
    {
        // 渐变黑场
        m_Timer += Time.deltaTime;
        blackCanvas.GetComponent<CanvasGroup>().alpha = m_Timer/blackTime;

        yield return new WaitForSeconds(blackTime);
    }

}
