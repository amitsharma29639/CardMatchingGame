
using System;
using System.Collections.Generic;

[Serializable]
public class GameState
{
    public int score;

    public int turns;

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

}
