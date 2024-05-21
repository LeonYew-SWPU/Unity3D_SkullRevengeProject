using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SeaDrop : MonoBehaviour
{
    public GameObject player;
    public GameEnding_2 gameEnding2;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            gameEnding2.DropDefeated();
        }
    }
}
