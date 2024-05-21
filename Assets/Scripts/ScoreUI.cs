using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    private TextMeshProUGUI coinScore;

    void Start()
    {
        coinScore = GetComponent<TextMeshProUGUI>();
    }

    public void UpdateDiamondText(PlayerScore playerScore)
    {
        coinScore.text = playerScore.NumberOfCoins.ToString();
    }
}
