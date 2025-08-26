

using CardGame.GamePlay;

public interface IGameResultListner 
{
    void OnResultEvaluationStarted(Card firstCard, Card secondCard);
    void OnMatchFound(Card firstCard, Card secondCard);
    void OnNoMatch(Card firstCard, Card secondCard);
    void OnGameFinished();
}
