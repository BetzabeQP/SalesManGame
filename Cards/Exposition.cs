namespace SalesManGame;

public class Exposition : Card
{
    public override string GetCardPicture()
    {
        return "Exposition";
    }

    public override string GetDescription()
    {
        return "Deals 4 points and multiply twice the points total";
    }

    public override string GetTextureName()
    {
        return "CardSprite";
    }

    public override string GetTitleCard()
    {
        return "Exposition";
    }

    public override void Play(ScreenManager screen, Summary results)
    {
        results.netPointMultiplyer *= 1.25f;
        results.netPoints += 4;
    }
}