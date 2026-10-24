namespace SalesManGame;

public class Dialogue : Card
{
    public override string GetCardPicture()
    {
        return "Dialogue";
    }

    public override string GetDescription()
    {
        return "Deals 20 points";
    }

    public override string GetTextureName()
    {
        return "CardSprite";
    }

    public override string GetTitleCard()
    {
        return "Dialogue";
    }

    public override void Play(ScreenManager screen, Summary results)
    {
        results.netPoints += 20;
    }
}