using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace SalesManGame;
public class Cards
{
    string spriteName = "CardSprite";
    public float Scale = 1f;
    public Perks perkType{get;set;}
    private Texture2D texture;
    public Vector2 Position = new Vector2(100,500);
    public void LoadContent(ContentManager content)
    {
        if(content is null) return;
        texture = content.Load<Texture2D>(spriteName);
    }

    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        float offset = (-texture.Width/2f)*Scale;
        Vector2 final = new Vector2(Position.X+offset, Position.Y-(texture.Height/2f)*Scale);
        spriteBatch.Draw(texture, final, null, Color.White, 0, new Vector2(0,0), Scale, SpriteEffects.None, 0);
    }
}