namespace SalesManGame;

public class Charm : Card
{
    public override string GetCardPicture()
    {
        return "Charm";
    }

    public override string GetDescription()
    {
        return "Deals 3 points";
    }

    public override string GetTextureName()
    {
        return "CardSprite";
    }

    public override string GetTitleCard()
    {
        return "Charm";
    }

    public override void Play(ScreenManager screen, Summary results)
    {
        results.netPoints += 3;
    }
}