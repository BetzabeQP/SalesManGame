using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SalesManGame;
public class SunGoingOver
{
    const float speed = 30.0f;
    Texture2D texture;
    private Vector2 spritePosition;
    private Vector2 spriteOrigin;
    private float rotationAngle;
    private Vector2 pivot;
    public float GetSunAngle()
    {
        return rotationAngle;
    }
    public void LoadContent(GraphicsDevice gd, ContentManager content)
    {
        if(content is null) return;
        texture = content.Load<Texture2D>("Sun");
        Viewport viewport = gd.Viewport;
        spriteOrigin.X = texture.Width / 2;
        spriteOrigin.Y = texture.Height/2;
        spritePosition.X = viewport.Width / 2;
        spritePosition.Y = viewport.Height / 2;
        pivot.X = viewport.Width/2;
        pivot.Y = viewport.Height;
    }
    public void Update(GameTime gt)
    {
        float elapsed = (float)gt.ElapsedGameTime.TotalSeconds;
        rotationAngle += elapsed;
        float circle = (float)Math.PI * 2;
        rotationAngle %= circle;       
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        Matrix Ra = Matrix.CreateRotationZ(rotationAngle-(float)(Math.PI/2.0f));
        Vector2 offset = spritePosition - pivot;
        Vector2 pos = pivot+ Vector2.Transform(offset, Ra);
        spriteBatch.Draw(texture, pos, null, Color.White, rotationAngle,
        spriteOrigin, 3.0f, SpriteEffects.None, 0f);
        
    }
}