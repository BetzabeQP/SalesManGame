using System;
using System.Collections;
using System.Diagnostics;
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
    private Vector2 position = new Vector2(300*4,300);

    public void LoadContent(ContentManager content)
    {
        texture = content.Load<Texture2D>("CustSheet");
    }
    public int LoadInDoubt()
    {
        switch(PersonalityType)
        {
            case Personality.Impatient:
                return 50;
            case Personality.Naive:
                return 15;
            case Personality.Skeptic:
                return  60;
            case Personality.Spriritual:
                return 30;
            case Personality.Stingy:
                return 75;
        }
        return 50;
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        changeFrame += gameTime.ElapsedGameTime.TotalSeconds;
        if(changeFrame > 1)
        {
            frame++;
            changeFrame = 0;
            if(frame >= MAXFRAMES) frame = 0;
        }
        Rectangle source = new Rectangle(0,0,64,128);
        source.X = frame * 64;
        spriteBatch.Draw(texture, position, source, Color.White, 0f, new Vector2(),scale: 2f,SpriteEffects.None, 0);
    }

}