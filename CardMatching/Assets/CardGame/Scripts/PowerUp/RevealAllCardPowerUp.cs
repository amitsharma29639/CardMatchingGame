

public class RevealAllCardsPowerUp : PowerUp
{
    private CardsManager cardManager;

    public RevealAllCardsPowerUp(CardsManager manager)
    {
        PowerUpName = "Reveal All Cards";
        cardManager = manager;
    }

    public override void Activate()
    {
        cardManager.RevealAllUnMatchedHiddenCards();
    }
}