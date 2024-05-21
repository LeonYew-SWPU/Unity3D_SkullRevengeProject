using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coins : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerScore playerScore = other.GetComponent<PlayerScore>();

        if (playerScore != null)
        {
            playerScore.CoinCollected();
            gameObject.SetActive(false);
        }
    }
}
