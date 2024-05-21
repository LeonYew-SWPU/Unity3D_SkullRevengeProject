using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerScore : MonoBehaviour
{
    public int NumberOfCoins { get; private set; }    // ֻ��

    public UnityEvent<PlayerScore> OnCoinCollected;

    public void CoinCollected()
    {
        NumberOfCoins++;
        OnCoinCollected.Invoke(this);
    }
}
