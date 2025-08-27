
using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameState
{
    [SerializeField]
    public int score;
    [SerializeField]
    public int turns;

    [SerializeField] 
    public int powerCount;
    
    [SerializeField]
    public GameGridStateData gridData;
    
    public GameState(int row, int col)
    {
        gridData = new GameGridStateData(row, col);
    }

    public void SetGridData(List<CardStateData> cardsData)
    {
        gridData.SetGridData(cardsData);
    }

    public void SetScore(int score)
    {
        this.score = score;
    }

    public void SetTurns(int turns)
    {
        this.turns = turns;
    }

    public void SetPowerCount(int powerCount)
    {
        this.powerCount = powerCount;
    }

}
