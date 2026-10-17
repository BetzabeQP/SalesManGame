using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SalesManGame;
public class CloudAcross
{
    Texture2D texture;
    const float speed = 40.0f;
    private float posX = 0;
    private const float scale =2.5f;
    public void LoadContent(ContentManager content)
    {
        if(content is null) return;
        texture = content.Load<Texture2D>("CloudA1");
    }
    public void Update(GameTime gameTime)
    {
        posX -= speed * (float)gameTime.ElapsedGameTime.TotalSeconds;
        posX = posX % (texture.Width*scale); 
    }
    public void Draw(GameTime gt, SpriteBatch sb)
    {
        
        sb.Draw(texture, new Vector2(posX, 100), new Rectangle(0,0,texture.Width,texture.Height), Color.White, 0, new Vector2(0,0), scale, SpriteEffects.None, 0);
        sb.Draw(texture, new Vector2(posX+(texture.Width*scale), 100), new Rectangle(0,0,texture.Width,texture.Height), Color.White, 0, new Vector2(0,0), scale, SpriteEffects.None, 0);
        sb.Draw(texture, new Vector2(posX+(texture.Width*scale)*2, 100), new Rectangle(0,0,texture.Width,texture.Height), Color.White, 0, new Vector2(0,0), scale, SpriteEffects.None, 0);

        //sb.Draw(texture, new Vector2(-posX, 150), new Rectangle(0,0,texture.Width,texture.Height), Color.Gray, 0, new Vector2(0,0), scale, SpriteEffects.None, 0);
        //sb.Draw(texture, new Vector2(-posX+(texture.Width*scale), 150), new Rectangle(0,0,texture.Width,texture.Height), Color.Gray, 0, new Vector2(0,0), scale, SpriteEffects.None, 0);
        //sb.Draw(texture, new Vector2(-posX-(texture.Width*scale), 150), new Rectangle(0,0,texture.Width,texture.Height), Color.Gray, 0, new Vector2(0,0), scale, SpriteEffects.None, 0);
    }
}