using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace SalesManGame;
public class Customer
{
    public Personality PersonalityType;
    private Texture2D texture;
    private short frame = 0;
    private const short MAXFRAMES = 6;
    private double changeFrame;
    private Vector2 position = new Vector2(300,300);

    public void LoadContent(ContentManager content)
    {
        //texture = content.Load<Texture2D>("CustSheet");
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        changeFrame += gameTime.ElapsedGameTime.TotalSeconds;
        if(changeFrame > 0.5)
        {
            frame++;
            if(frame > MAXFRAMES) frame = 0;
        }
        Rectangle source = new Rectangle(0,64,64,64);
        source.X = frame * 32;
        //spriteBatch.Draw(texture, position, source, Color.White);
    }

}