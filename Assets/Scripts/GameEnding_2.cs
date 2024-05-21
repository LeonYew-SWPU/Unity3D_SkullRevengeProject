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
    private float blackTime; //��Ļת��ʱ��

    [SerializeField]
    private CanvasGroup blackCanvas; //��Ļ����

    [SerializeField]
    private GameObject followShoot; //���澵ͷ

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
        // ��Ϸʧ��
        isDefeated = true;
        print("isDefeated");
        EndLevel(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == player)
        {
            // ��Ϸʤ��
            isWin = true;
            print("Win!!!");
            EndLevel(true);
        }
    }

    private void EndLevel(bool isWinEnding)
    {



        if (isWinEnding)
        {
            // ���سɹ�����
            SceneManager.LoadScene("Scene_02_Win");

        }
        else
        {
            // ����ʧ�ܳ���
            SceneManager.LoadScene("Scene_02_Defeat");
        }
    }

    IEnumerator WaitDarkScene(float blackTime)
    {
        // ����ڳ�
        m_Timer += Time.deltaTime;
        blackCanvas.GetComponent<CanvasGroup>().alpha = m_Timer/blackTime;

        yield return new WaitForSeconds(blackTime);
    }

}
