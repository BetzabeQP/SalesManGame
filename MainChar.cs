using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace SalesManGame;
public class MainChar
{
    private Texture2D texture;
    private short frame = 0;
    private const short IDLEMAXFRAMES = 9;
    private double changeFrame;
    private Vector2 position = new Vector2(100,100);
    public void LoadContent(ContentManager content)
    {
        texture = content.Load<Texture2D>("SaleMan-Sheet");
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        changeFrame += gameTime.ElapsedGameTime.TotalSeconds;
        if(changeFrame > 0.5)
        {
            frame++;
            if(frame > IDLEMAXFRAMES) frame = 0;
        }
        Rectangle source = new Rectangle(0,128,64,128);
        source.X = frame * 32;
        spriteBatch.Draw(texture, position, source, Color.White);
    }
}