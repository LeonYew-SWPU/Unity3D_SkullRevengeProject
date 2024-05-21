using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SeaDrop1 : MonoBehaviour
{
    public GameObject player;
    public GameEnding gameEnding;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            gameEnding.DropDefeated();
        }
    }
}
