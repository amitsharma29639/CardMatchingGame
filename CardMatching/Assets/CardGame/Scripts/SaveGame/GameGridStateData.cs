

using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameGridStateData 
{
    [SerializeField]
    public int row;
    [SerializeField]
    public int col;
    [SerializeField]
    public List<CardStateData> cardsData;

    public GameGridStateData(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    public void SetGridData(List<CardStateData> cardsDate)
    {
        this.cardsData = cardsDate;
    }
}
