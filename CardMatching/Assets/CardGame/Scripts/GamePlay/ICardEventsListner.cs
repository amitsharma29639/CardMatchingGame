

using CardGame.GamePlay;

public interface ICardEventsListner
{
    void OnCardClicked(Card card);
    void OnCardFlipped(Card card);
    void OnCardFaceUpAnimationFinished(Card card);
    void OnCardFaceDownAnimationFinished(Card card);
   void OnCardDisableAnimationFinished(Card card);
}
