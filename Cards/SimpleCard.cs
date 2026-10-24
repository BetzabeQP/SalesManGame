namespace SalesManGame;

public class SimpleCard : Card
{
    public override string GetTextureName()
    {
        return "CardSprite";
    }
    public override string GetCardPicture()
    {
        return "TalkingPts";
    }
    public override void Play(ScreenManager screen, Summary result)
    {
        result.netPoints +=  2;
    }

    public override string GetTitleCard()
    {
        return "Talking Points";
    }

    public override string GetDescription()
    {
        return "Deal 2 points";
    }
}