namespace SalesManGame;

public class HasChara : Card
{
    public override string GetCardPicture()
    {
        return "HasChar";
    }

    public override string GetDescription()
    {
        return "Deal 6 points";
    }

    public override string GetTextureName()
    {
        return "CardSprite";
    }

    public override string GetTitleCard()
    {
        return "Has Character";
    }

    public override void Play(ScreenManager screen, Summary results)
    {
        results.netPoints += 6;
    }
}