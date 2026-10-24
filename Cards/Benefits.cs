namespace SalesManGame;

public class Benefits : Card
{
    public override string GetCardPicture()
    {
        return "Benefits";
    }

    public override string GetDescription()
    {
        return "Deals 8 points";
    }

    public override string GetTextureName()
    {
        return "CardSprite";
    }

    public override string GetTitleCard()
    {
        return "Benefits";
    }

    public override void Play(ScreenManager screen, Summary results)
    {
        results.netPoints += 8;
    }
}