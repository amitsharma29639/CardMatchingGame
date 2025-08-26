using System;
using UnityEngine;


public class TurnsManager
{
    public Action<int> OnTurnCountChanged = delegate { };
    private int turn = 0;

    public void IncreamentTurnCount()
    {
        turn++;
        OnTurnCountChanged(turn);
        Debug.Log("Turn count "+turn);
    }

    public void OnDisable()
    {
        OnTurnCountChanged = delegate { };
    }
}
