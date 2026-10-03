using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SalesManGame;
public class MainMenu: GameScreen
{
    private bool debounce = false;
    string text = "start";
    ScreenManager screenManager;
    private KeyboardState keyboardState;
    private bool confirm;
    private InputState inputState;
    Texture2D texture;
    Rectangle start;
    //Rectangle exit;
    Game game;
    GraphicsDevice graphics;
    public MainMenu(Game parentGame, GraphicsDevice gD, ScreenManager sM)
    {
        confirm = false;
        screenManager = sM;
        game = parentGame;
        graphics = gD;
        start = new Rectangle(40, 80,100,20);
    }
    public void LoadContent()
    {
      texture = new Texture2D(graphics, 1, 1);
      texture.SetData(new[]{Color.White});
    }
    public override void HandleInput(GameTime gameTime, InputState input)
    {
        inputState = input;
        base.HandleInput(gameTime, input);
    }
    public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
    {
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
                    LevelScreenManager levelScreenManager =  new LevelScreenManager(game, graphics);
                    screenManager.AddScreen(levelScreenManager);}
                    //text = "clicked";
                }
                if(mouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released)
                {
                    
                    //text = ($"X:{mouseState.X.ToString()}, Y:{mouseState.Y.ToString()}");
                    
                } 
                
            }
            else confirm = false;
        }
        else confirm=false;
        if(mouseState.RightButton == ButtonState.Pressed) text = "start";
            
    }
    public override void Activate()
    {
        base.Activate();
        this.LoadContent();
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if(this.IsActive ==false) return;
        spriteBatch.Begin();
        spriteBatch.Draw(texture,start,new Rectangle(0,0,1,1),Color.Blue);
        spriteBatch.DrawString(screenManager.MenuFont, text, new Vector2(start.X, start.Y), Color.White);
        //spriteBatch.Draw(texture, new Rectangle(0,0,50,50), Color.Red);
        spriteBatch.End();
    }


}