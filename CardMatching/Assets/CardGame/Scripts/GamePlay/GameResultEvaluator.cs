
using System.Collections.Generic;
using CardGame.GamePlay;
using UnityEngine;

public class GameResultEvaluator : ICardEventsListner
{
    private List<GameObject> cards;
    private List<IGameResultListner> listeners;
    private Queue<Card> queue;

    public GameResultEvaluator(List<GameObject> cards)
    {
        this.cards = cards;

        listeners = new List<IGameResultListner>();
        queue = new Queue<Card>();
        
        Init();

    }

    public void Init()
    {
        foreach (var cardObj in cards)
        {
            Card card = cardObj.GetComponent<Card>();
            card.AddListener(this);
        }
        
    }

    public void AddListener(IGameResultListner listener)
    {
        listeners.Add(listener);
    }

    public void RemoveListener(IGameResultListner listener)
    {
        listeners.Remove(listener);
        
    }

    private void ClearListeners()
    {
        listeners.Clear();
    }

    private void OnCardRevealed(Card card)
    {
        if (card.CurrentFace == CardFace.frontFace)
        {
            queue.Enqueue(card);
        }

        // Keep evaluating until fewer than 2 cards remain
        while (queue.Count >= 2)
        {
            var firstCard = queue.Dequeue();
            var secondCard = queue.Dequeue();

            NotifyResultEvaluationStarted(firstCard, secondCard);
            EvaluateRevealedCards(firstCard, secondCard);
        }
    }

    private async void EvaluateRevealedCards(Card firstRevealedCard , Card secondRevealedCard)
    {
        if (firstRevealedCard.GetCardData().Equals(secondRevealedCard.GetCardData()))
        {
            await System.Threading.Tasks.Task.Delay(200); // Small delay to show the second card
            Debug.Log("Match Found!");
            NotifyMatchFound(firstRevealedCard, secondRevealedCard);
            if (CheckForGameFinish())
            {
                await System.Threading.Tasks.Task.Delay(200);
                NotifyGameFinished();
            }
        }
        else
        {
            await System.Threading.Tasks.Task.Delay(400);
            Debug.Log("No Match. Hiding cards again.");
            NotifyNoMatch(firstRevealedCard, secondRevealedCard);

        }

        firstRevealedCard = null;
        secondRevealedCard = null;
    }

    private bool CheckForGameFinish()
    {
        foreach (var cardObj in cards)
        {
            if (cardObj.activeSelf)
            {
                return false;
            }
        }
        return true;

    }

    private void NotifyResultEvaluationStarted(Card firstCard, Card secondCard)
    { 
      foreach (var listener in listeners)
        {
            listener.OnResultEvaluationStarted(firstCard, secondCard);
        }
    }
    private void NotifyMatchFound(Card firstCard, Card secondCard)
    {
        foreach (var listener in listeners)
        {
            listener.OnMatchFound(firstCard, secondCard);
        }
    }

    private void NotifyNoMatch(Card firstCard, Card secondCard)
    {
        foreach (var listener in listeners)
        {
            listener.OnNoMatch(firstCard, secondCard);
        }
    }

    private void NotifyGameFinished()
    {
        foreach (var listener in listeners)
        {
            listener.OnGameFinished();
        }
    }

    public void OnDisable()
    {
        ClearListeners();
    }

    public void OnCardClicked(Card card)
    {

    }

    public void OnCardFlipped(Card card)
    {

    }

    public void OnCardFaceUpAnimationFinished(Card card)
    {
        OnCardRevealed(card);

    }

    public void OnCardFaceDownAnimationFinished(Card card)
    {

    }

    public void OnCardDisableAnimationFinished(Card card)
    {

    }
}
