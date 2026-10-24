namespace SalesManGame;

public class GoodOffer : Card
{
    public override string GetCardPicture()
    {
        return "OneTimeOffer";
    }

    public override string GetDescription()
    {
        return "Deals 5 points";
    }

    public override string GetTextureName()
    {
        return "CardSprite";
    }

    public override string GetTitleCard()
    {
        return "One Time Offer";
    }

    public override void Play(ScreenManager screen, Summary results)
    {
        results.netPoints += 5;
    }
}