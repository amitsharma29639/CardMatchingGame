

using System.Collections.Generic;
using System.IO;
using CardGame.GamePlay;
using UnityEngine;

public class SaveAndLoadGameData 
{
    private GameConfig gameConfig;
    private CardsManager cardsManager;
    private ScoreManager scoreManager;
    private TurnsManager turnsManager;
    private GameState gameState;
    private GameResultEvaluator evaluator;

    private string path;

    public SaveAndLoadGameData(GameConfig gameConfig , CardsManager cardsManager , ScoreManager scoreManager, TurnsManager turnsManager , GameResultEvaluator evaluator)
    {
        path = Application.persistentDataPath + "/SaveGameData.json";
        
        this.gameConfig = gameConfig;
        this.cardsManager = cardsManager;
        this.scoreManager = scoreManager;
        this.turnsManager = turnsManager;
        this.evaluator = evaluator;
        
        gameState = new GameState(gameConfig.Rows, gameConfig.Cols);

        if (gameConfig.LoadSavedGame)
        {
            ConfigureSavedData();
        }
        Debug.Log(path);
    }

    private void ConfigureSavedData()
    {
        gameState = Load();
        scoreManager.SetScore(gameState.score);
        turnsManager.SetTurns(gameState.turns);
        gameConfig.Rows = gameState.gridData.row;
        gameConfig.Cols = gameState.gridData.col;
        cardsManager.InstantiateFromSavedData(gameState.gridData.cardsData , evaluator);
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
                !cardObj.activeSelf, card.CurrentFace);
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
       return JsonUtility.FromJson<GameState>(jsonData);
    }
}
