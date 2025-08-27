
using System;
using CardGame.GamePlay;
using JTools.Sound.Core;
using JTools.Sound.Core.Constants;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class CardsGridManager : MonoBehaviour, IGameResultListner , IHUDEventsListner
{
    private float hSpacing = 1.5f;
    private float vSpacing = 2f;
    private CardsManager cardsManager;
    private GameResultEvaluator gameResultEvaluator;
    private ScoreManager scoreManager;
    private TurnsManager turnsManager;
    private SaveAndLoadGameData saveAndLoadGameData;
    
    
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private SpriteAtlas spriteAtlas;
    [SerializeField] private HUDManager hudManager;

    private void OnEnable()
    {
        AudioManager.Instance.Play(AudioGroupConstants.BGM_LOOP , AudioGroupConstants.BGM_LOOP, AudioGroupConstants.GAMEPLAYSFX);
    }

    public void Init(GameConfig gameConfig)
    {
        cardsManager = new CardsManager(cardPrefab, transform, gameConfig.Rows * gameConfig.Cols, spriteAtlas);
        
        scoreManager = new ScoreManager(0);
        scoreManager.AddScoreListner(hudManager.OnScoreUpdate);
        
        turnsManager = new TurnsManager(0);
        turnsManager.AddTurnsListner(hudManager.OnTurnsUpdate);
        
        gameResultEvaluator = new GameResultEvaluator(cardsManager.GetCards());
        gameResultEvaluator.AddListener(this);
        gameResultEvaluator.AddListener(cardsManager);
        
        hudManager.AddListener(this);

        saveAndLoadGameData = new SaveAndLoadGameData(gameConfig, cardsManager , scoreManager, turnsManager , gameResultEvaluator);
        
        CreateGrid(gameConfig.Rows, gameConfig.Cols);
    }

    public void SaveGame()
    {
        saveAndLoadGameData.Save();
    }

    private void CreateGrid(int rows, int cols)
    {
        float gridWidth = (cols - 1) * hSpacing;
        float gridHeight = (rows - 1) * vSpacing;

        Vector3 startPosition = new Vector3(-gridWidth / 2, gridHeight / 2, 0);

        var cards = cardsManager.GetCards();
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                int index = i * cols + j;
                if (index < cards.Count)
                {
                    GameObject card = cards[index];
                    card.transform.localPosition = new Vector3(startPosition.x + j * hSpacing, startPosition.y - i * vSpacing, startPosition.z);
                }
            }
        }
    }

    public void OnResultEvaluationStarted(Card firstCard, Card secondCard)
    {
        
    }

    public void OnMatchFound(Card firstCard, Card secondCard)
    {
       scoreManager.AddScore(1);
       turnsManager.IncreamentTurnCount();
    }

    public void OnNoMatch(Card firstCard, Card secondCard)
    {
        turnsManager.IncreamentTurnCount();
    }

    public void OnGameFinished()
    {

    }
    
    private void OnDisable()
    {
        AudioManager.Instance.Stop(AudioGroupConstants.BGM_LOOP );
    }

    void OnDestroy()
    {
        gameResultEvaluator.OnDisable();
        cardsManager.OnDestroy();
        scoreManager.OnDestroy();
        turnsManager.OnDestroy();
    }


    public void OnHomeBtnClicked()
    {
        
    }

    public void OnSaveBtnClicked()
    {
        saveAndLoadGameData.Save();
    }
}
