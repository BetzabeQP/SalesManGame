using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace SalesManGame;
public class MainChar
{
    public List<Card> Deck = new();
    private Texture2D texture;
    private short frame = 0;
    private const short IDLEMAXFRAMES = 9;
    private double changeFrame;
    private Vector2 position = new Vector2(300*2,300);
    public void LoadContent(ContentManager content)
    {
        texture = content.Load<Texture2D>("SaleMan-Sheet");
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        changeFrame += gameTime.ElapsedGameTime.TotalSeconds;
        if(changeFrame > 1)
        {
            frame++;
            changeFrame = 0;
            if(frame >= IDLEMAXFRAMES) frame = 0;
        }
        Rectangle source = new Rectangle(0,0,64,128);
        source.X = frame * 64;
        spriteBatch.Draw(texture, position, source, Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
    }
}