using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace SalesManGame;
public class Days
{
    Texture2D tearoff;
    Texture2D daily;
    public readonly int Score;
    public float Scale = 1f;
    private const float CONSCALE = 2f;
    public Vector2 Position = new(); 
    public Rectangle Today = new Rectangle(0,0,8,8);
    public bool Hovered = false;
    public bool Restriction = true;
    public Color color = Color.White;
    
    public Days(int baseScore)
    {
        Score = baseScore;
    }
    public void LoadContent(ContentManager content)
    {
        tearoff = content.Load<Texture2D>("TearOff");
        daily = content.Load<Texture2D>("Daily");
    }
    public void Draw(Game game, SpriteBatch spriteBatch)
    {
        Vector2 PositionLetter = new Vector2(tearoff.Width + Position.X, tearoff.Height + Position.Y);
        spriteBatch.Draw(tearoff, Position,new Rectangle(0,0,tearoff.Width, tearoff.Height),color, 0f,new Vector2(),scale: Scale*4, SpriteEffects.None, 0.1f);
        spriteBatch.Draw(daily,PositionLetter, Today, Color.White, 0f,new Vector2(),scale: Scale*4, SpriteEffects.None, 0.1f);
    }

}