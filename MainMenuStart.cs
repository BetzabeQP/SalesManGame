using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SalesManGame;
public class MainMenuStart: GameScreen
{
    private bool debounce = false;
    string text = "start";
    City city;
    SkyEffect sky;
    SunGoingOver sun;
    CloudAcross cloud;
    private ContentManager content;
    ScreenManager screenManager;
    private KeyboardState keyboardState;
    private bool confirm;
    private InputState inputState;
    public PersistentScreen persistentScreen;
    Texture2D texture;
    Rectangle start;
    Rectangle load;
    //Rectangle exit;
    Game game;
    GraphicsDevice graphics;
    public MainMenuStart(Game parentGame, GraphicsDevice gD, ScreenManager sM)
    {
        confirm = false;
        screenManager = sM;
        game = parentGame;
        graphics = gD;
        content = new ContentManager(game.Services);
        content.RootDirectory = "Content";
        start = new Rectangle(40*2, 80,100*2,20*2);
        load = new Rectangle(40*2, 120,100*2,20*2);
        //persistentScreen = ps;
        
    }
    public void LoadContent()
    {
      texture = new Texture2D(graphics, 1, 1);
      texture.SetData(new[]{Color.White});
      sky.LoadContent(content);
      sun.LoadContent(graphics, content);
      cloud.LoadContent(content);
      city.LoadContent(content);
    }
    public override void HandleInput(GameTime gameTime, InputState input)
    {
        inputState = input;
        base.HandleInput(gameTime, input);
    }
    public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
    {
        //cloud.Update(gameTime);
        sun.Update(gameTime);
        sky.Update(sun.GetSunAngle());
        //if(persistentScreen == null) return;
        cloud.Update(gameTime);
        if(inputState is null) return;
        var mouseState = inputState.CurrentMouseStates[0];
        if(mouseState.X >= start.X && mouseState.X<= (start.X + start.Width))
        {
            if(mouseState.Y >= start.Y && mouseState.Y <= (start.Y + start.Height))
            {
                if(mouseState.LeftButton == ButtonState.Pressed) confirm = true;
                if(mouseState.LeftButton ==  ButtonState.Released && confirm == true) 
                { 
                    //this.Deactivate();
                    if(debounce == false) {
                    debounce = true;
                    this.ScreenState = ScreenState.TransitionOff;
                    LevelSelectScreen lvSelect = new LevelSelectScreen(game, graphics, screenManager, persistentScreen);
                    persistentScreen.ScreenState = ScreenState.TransitionOn;
                    screenManager.AddScreen(lvSelect);}
                    //text = "clicked";
                }
                if(mouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
                {
                    
                    //text = ($"X:{mouseState.X.ToString()}, Y:{mouseState.Y.ToString()}");
                    
                } 
                
            }
            else confirm = false;
        }
        if(mouseState.X >= load.X && mouseState.X<= (load.X + load.Width))
        {
            if(mouseState.Y >= load.Y && mouseState.Y <= (load.Y + load.Height))
            {
               
                if(mouseState.LeftButton ==  ButtonState.Pressed) 
                { 
                    //this.Deactivate();
                   
                    {
                        //debounce = true;
                        string filePath = "save.json";
                        if(File.Exists(filePath)) 
                        {
                            string save = File.ReadAllText(filePath);
                            persistentScreen.Load(save);
                        }
                    }
                    //text = "clicked";
                }
            }
            //else confirm = false;
        }
        else confirm=false;
        if(mouseState.RightButton == ButtonState.Pressed) text = "start";
        if(mouseState.RightButton == ButtonState.Pressed) text = "Load";
            
    }
    public override void Activate()
    {
        base.Activate();
        sky = new();
        sun = new();
        cloud = new();
        city = new();
        this.LoadContent();
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if(this.IsActive ==false) return;
        spriteBatch.Begin();
        spriteBatch.Draw(texture,start,new Rectangle(0,0,1,1),Color.Blue);
        spriteBatch.Draw(texture, load, new Rectangle(0,0,1,1),Color.Blue);
        sky.Draw(gameTime, spriteBatch);
        sun.Draw(gameTime, spriteBatch);
        city.Draw(gameTime,spriteBatch);
        cloud.Draw(gameTime, spriteBatch);
        spriteBatch.DrawString(screenManager.MenuFont, text, new Vector2(start.X, start.Y), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
        spriteBatch.DrawString(screenManager.MenuFont, "load", new Vector2(load.X, load.Y), Color.White, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
        //spriteBatch.Draw(texture, new Rectangle(0,0,50,50), Color.Red);
        spriteBatch.End();
    }


}