namespace SalesManGame;

public class Sponser : Card
{
    public override string GetCardPicture()
    {
        return "SponserBy";
    }

    public override string GetDescription()
    {
        return "Deal 7 points";
    }

    public override string GetTextureName()
    {
        return "CardSprite";
    }

    public override string GetTitleCard()
    {
        return "Sponser By...";
    }

    public override void Play(ScreenManager screen, Summary results)
    {
        results.netPoints += 7;
    }
}