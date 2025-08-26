using System;
using CardGame.GamePlay;
using UnityEngine;

[Serializable]
public class CardStateData
{
    [SerializeField]
    public int id;
    [SerializeField]
    public string suit;
    [SerializeField]
    public string rank;
    [SerializeField]
    public bool isMatched;
    [SerializeField]
    public CardFace cardFace;


    public CardStateData(int id, string suit, string rank, bool isMatched, CardFace cardFace)
    {
        this.id = id;
        this.suit = suit;
        this.rank = rank;
        this.isMatched = isMatched;
        this.cardFace = cardFace;
    }
}
