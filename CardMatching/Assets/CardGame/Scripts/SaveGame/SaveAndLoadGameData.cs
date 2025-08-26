

using System.Collections.Generic;
using System.IO;
using CardGame.GamePlay;
using UnityEngine;

public class SaveAndLoadGameData 
{
    private int rows, cols;
    private CardsManager cardsManager;
    private ScoreManager scoreManager;
    private TurnsManager turnsManager;
    private GameState gameState;

    private string path;

    public SaveAndLoadGameData(int rows, int cols , CardsManager cardsManager , ScoreManager scoreManager, TurnsManager turnsManager)
    {
        this.rows = rows;
        this.cols = cols;
        this.cardsManager = cardsManager;
        this.scoreManager = scoreManager;
        this.turnsManager = turnsManager;
        
        gameState = new GameState(rows, cols);
        
        path = Application.persistentDataPath + "/SaveGameData.json";
        Debug.Log(path);
    }
    public void Save()
    {
        List<CardStateData> cardsData = new List<CardStateData>();
        List<GameObject> cards = cardsManager.GetCards();
        foreach (GameObject cardObj in cards)
        {
            Card card = cardObj.GetComponent<Card>();
            CardData cardData = card.GetCardData();
            CardStateData data = new CardStateData(cardData.id, cardData.suit, cardData.rank, 
                card.gameObject.activeSelf, card.CurrentFace);
            cardsData.Add(data);
        }
        
        gameState.SetGridData(cardsData);
        
        gameState.SetScore(scoreManager.Score);
        
        gameState.SetTurns(turnsManager.Turn);
        
        string jsonData = JsonUtility.ToJson(gameState);
        
        File.WriteAllText(path , jsonData);
    }
    

    public GameState Load()
    {
       string jsonData = File.ReadAllText(path);
       gameState = JsonUtility.FromJson<GameState>(jsonData);
       return gameState;
    }
}
