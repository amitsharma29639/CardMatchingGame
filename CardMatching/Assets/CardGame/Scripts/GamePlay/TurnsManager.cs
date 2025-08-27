using System;
using UnityEngine;

public class TurnsManager
{
    public Action<int> OnTurnCountChanged = delegate { };
    private int turn = 0;

    public TurnsManager(int turn)
    {
        this.turn = turn;
    }
    
    public int Turn => turn;
    
    public void AddTurnsListner(Action<int> listner)
    {
        OnTurnCountChanged += listner;
    }

    public void RemoveListner(Action<int> listner)
    {
        OnTurnCountChanged -= listner;
    }
   
    public void RemoveAllListner()
    {
        OnTurnCountChanged = delegate { };;
    }

    public void IncreamentTurnCount()
    {
        turn++;
        OnTurnCountChanged(turn);
        Debug.Log("Turn count "+turn);
    }

    public void SetTurns(int turns)
    {
        this.turn = turns;
        OnTurnCountChanged(turn);
    }

    public void OnDestroy()
    {
        RemoveAllListner();
    }
}
