
using System.Collections.Generic;
using CardGame.GamePlay;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UIElements;

public class CardsManager : IGameResultListner
{
    private List<GameObject> cards;
    private SpriteAtlas spriteAtlas;

    private GameObject cardPrefab;

  //  private List<ClickableObject> clickables;
    public CardsManager(GameObject cardPrefab, Transform parent, int cardsCount, SpriteAtlas spriteAtlas)
    {
        this.cardPrefab = cardPrefab;
        this.spriteAtlas = spriteAtlas;
        cards = new List<GameObject>();
       // clickables = new List<ClickableObject>();
        List<CardData> cardsData = GenerateUniqueRandomCards();
        List<CardData> gridCards = ShuffleCardsData(cardsData.GetRange(0, cardsCount));
        InstantiateCardObjects(cardPrefab, parent, gridCards);

    }

    public List<GameObject> GetCards()
    {
        return cards;
    }

    private List<CardData> GenerateUniqueRandomCards()
    {
        int id = 0;
        // Build full deck
        List<CardData> deck = new List<CardData>();

        foreach (var suit in Constants.SUITS)
        {
            foreach (var rank in Constants.RANKS)
            {
                for (int i = 0; i < 2; i++)
                {
                    deck.Add(new CardData(id++, suit, rank, spriteAtlas));
                }

            }
        }
        return deck;
    }

    private void InstantiateCardObjects(GameObject cardPrefab, Transform parent, List<CardData> cardsData)
    {
        foreach (var cardData in cardsData)
        {
            GameObject cardObj = GameObject.Instantiate(cardPrefab, parent);
            Card card = cardObj.GetComponent<Card>();
            card.Init(cardData);
            cards.Add(cardObj);
        }
        
    }

    private List<CardData> ShuffleCardsData(List<CardData> deck)
    {
        for (int i = 0; i < deck.Count; i++)
        {
            CardData temp = deck[i];
            int randomIndex = Random.Range(i, deck.Count);
            deck[i] = deck[randomIndex];
            deck[randomIndex] = temp;
        }

        return deck;
    }

    public void OnResultEvaluationStarted(Card firstCard, Card secondCard)
    {
       
    }

    public void OnMatchFound(Card firstCard, Card secondCard)
    {
        firstCard.gameObject.SetActive(false);
        secondCard.gameObject.SetActive(false);
    }

    public void OnNoMatch(Card firstCard, Card secondCard)
    {
        firstCard.FlipCard();
        secondCard.FlipCard();
    }

    public void OnGameFinished()
    {
        Debug.Log("Game Finished! All matches found.");
    }

    public void OnDisable()
    {
        ClearListeners();
    }

    private void ClearListeners()
    {
        foreach (var cardObj in cards)
        {
            Card card = cardObj.GetComponent<Card>();
            card.ClearListeners();
        }
    }
}
