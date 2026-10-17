using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace SalesManGame;
public class City
{
    private int time = 0;
    public Texture2D houseTexture;
    private Color curColor = Color.CornflowerBlue;
    public void LoadContent(ContentManager content)
    {
        if(content is null) return;
        houseTexture = content.Load<Texture2D>("MainMenu");
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        spriteBatch.Draw(houseTexture, new Vector2(0, 300), new Rectangle(0,0, 192, 100),Color.White, 0,new Vector2(0,0), scale:3f, SpriteEffects.None, 0);
        spriteBatch.Draw(houseTexture, new Vector2(576, 300), new Rectangle(0,0, 128, 100),Color.White, 0,new Vector2(0,0), scale:3f, SpriteEffects.None, 0);
    }
}