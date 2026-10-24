using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace SalesManGame;
public class SkyEffect
{
    public Texture2D skyTexture;
    private Color curColor = Color.CornflowerBlue;
    public void LoadContent(ContentManager content)
    {
        if(content is null) return;
        skyTexture = content.Load<Texture2D>("SkyA1");
    }
    public void Update(float angle)
    {
        float degree = MathHelper.ToDegrees(angle);
        if (degree > 20 && degree < 160) curColor =  Color.CornflowerBlue;
        else if(degree > -10 && degree < 190) curColor = new Color(230, 128, 114);
        else curColor = new Color(118,66,138);
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        //spriteBatch.Begin(blendState:BlendState.Additive);
        spriteBatch.Draw(skyTexture, new Vector2(0, 0),  new Rectangle(0,0,192,128),curColor, 0,new Vector2(0,0), scale:3f*2, SpriteEffects.None, 0);
        spriteBatch.Draw(skyTexture, new Vector2(0, 0),  new Rectangle(0,0,108,128),curColor, 0,new Vector2(-192,0), scale:3f*2, SpriteEffects.None, 0);
        //spriteBatch.Draw(cloudTexture, new Vector2(100, 300), curColor);
        //spriteBatch.End();
    }
}