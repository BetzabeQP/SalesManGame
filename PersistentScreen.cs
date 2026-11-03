using System;
using System.IO;
using System.Net.Mime;
using System.Text.Json;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
namespace SalesManGame;

public class PersistentScreen: GameScreen
{
    public class Serial
    {
        public int money{get;set;}
        public int levels{get;set;}
    }
    public int money;
    private static readonly Vector2 origin = new Vector2(1200, 20);
    private static readonly Rectangle saveBox = new Rectangle((int)origin.X, (int)origin.Y + 40 ,100*2,20*2); 
    public int levels;
    SpriteFont font;
    GraphicsDevice graphicsDevice;
    private InputState inputState;
    Texture2D texture;
    private ContentManager content;
    public PersistentScreen(Game game, GraphicsDevice gd, ScreenManager screenManager)
    {
        content = new ContentManager(game.Services);
        content.RootDirectory = "Content";
        graphicsDevice = gd;
        money = 0;
        levels = 0;
        //this.Activate();
        
    }
    public void Load(string json)
    {
        Serial data = JsonSerializer.Deserialize<Serial>(json);
        this.money = data.money;
        this.levels = data.levels;
    }
    public string Save()
    {
        Serial data = new Serial{money = this.money, levels = this.levels};
        return JsonSerializer.Serialize<Serial>(data);
    }
    public override void Activate()
    {
        this.LoadContent();
        base.drawOrder = 100;
    }
    public void LoadContent()
    {
        font = content.Load<SpriteFont>("Roboto-BlackItalic");
        texture = new Texture2D(graphicsDevice, 1, 1);
    }
    public override void HandleInput(GameTime gameTime, InputState input)
    {
        inputState = input;
        base.HandleInput(gameTime, input);
    }
    public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
    {
        //base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);
        if(inputState is null) return;
        var mouseState = inputState.CurrentMouseStates[0];
        if(mouseState.X >= saveBox.X && mouseState.X<= (saveBox.X + saveBox.Width))
        {
            if(mouseState.Y >= saveBox.Y && mouseState.Y <= (saveBox.Y + saveBox.Height))
            {
                
                if(mouseState.LeftButton ==  ButtonState.Released) 
                { 
                    //this.Deactivate();
                    string json = Save();
                    string path = "save.json";
                    File.WriteAllText(path, json);
                    
                    //text = "clicked";
                }
                if(mouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
                {
                    
                    //text = ($"X:{mouseState.X.ToString()}, Y:{mouseState.Y.ToString()}");
                    
                } 
                
            }
          
        }
    }
    public override void Draw(GameTime gt, SpriteBatch spriteBatch)
    {
        //if(money == 0) return;
        Vector2 origin = new Vector2(1200, 20);
        spriteBatch.Begin();
        spriteBatch.Draw(texture,saveBox,new Rectangle(0,0,1,1),Color.Blue);
        spriteBatch.DrawString(font, "Save", new Vector2(0 + origin.X, 40+origin.Y), Color.DarkCyan, 0f, new Vector2(), 3f, SpriteEffects.None, 0f);
        spriteBatch.DrawString(font, money.ToString(), origin, Color.Black, 0f, new Vector2(), 3f, SpriteEffects.None, 0f);
        spriteBatch.End();
    }
}