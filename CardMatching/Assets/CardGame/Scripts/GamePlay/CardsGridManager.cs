
using CardGame.GamePlay;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class CardsGridManager : MonoBehaviour, IGameResultListner
{

    private float hSpacing = 1.5f;
    private float vSpacing = 2f;
    private CardsManager cardsManager;

    private GameResultEvaluator gameResultEvaluator;
    [SerializeField] private GameObject cardPrefab;
    [SerializeField] private SpriteAtlas spriteAtlas;

    void Start()
    {
        Init(4, 4);
    }

    public void Init(int rows, int cols)
    {
        cardsManager = new CardsManager(cardPrefab, transform, rows * cols, spriteAtlas);
        CreateGrid(rows, cols);

        gameResultEvaluator = new GameResultEvaluator(cardsManager.GetCards());
        gameResultEvaluator.AddListener(this);
        gameResultEvaluator.AddListener(cardsManager);
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

    }

    public void OnNoMatch(Card firstCard, Card secondCard)
    {

    }

    public void OnGameFinished()
    {

    }

    void OnDisable()
    {
        gameResultEvaluator.OnDisable();
        cardsManager.OnDisable();
    }

    
}
