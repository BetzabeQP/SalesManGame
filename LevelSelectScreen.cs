using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SalesManGame;
public class LevelSelectScreen: GameScreen
{
    public InputState inputState;
    Game game;
    ScreenManager screenManager;
    public List<Days> day;
    private ContentManager content;
    PersistentScreen persistentScreen;
    GraphicsDevice graphics;
    private int dayCurrent = 0;
    public LevelSelectScreen(Game Parentgame, GraphicsDevice gD, ScreenManager sM, PersistentScreen ps)
    {
        game = Parentgame;
        graphics = gD;
        content = new ContentManager(game.Services);
        content.RootDirectory = "Content";
        persistentScreen = ps;
        screenManager = sM;
    }
    public override void HandleInput(GameTime gameTime, InputState input)
    {
        inputState = input;
        base.HandleInput(gameTime, input);
    }
    public override void Activate()
    {
        day = new List<Days>
        {
            new Days(100){Restriction = false, Position = new Vector2(150, 20)},
            new Days(200){Today = new Rectangle(8,0,8,8), Position = new Vector2(350, 20)},
            new Days(300){Today = new Rectangle(16,0,8,8), Position = new Vector2(550, 20)},
            new Days(400){Today = new Rectangle(24,0,8,8), Position = new Vector2(750, 20)},
            new Days(500){Today = new Rectangle(0,8,8,8), Position = new Vector2(950, 20)}
        };
        this.LoadContent();
    }
    public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
    {
        persistentScreen.ScreenState = ScreenState.TransitionOn;
        int levelUnlock = persistentScreen.levels;
        if(this.IsActive == false) return;
        if(inputState is null) return;
        if(dayCurrent >= day.Count) dayCurrent = 0;
        var keyboardState = inputState.CurrentKeyboardStates[0];
        PlayerIndex player;
        bool enter = inputState.IsNewKeyPress(Keys.Enter, null, out player);
        bool right = inputState.IsNewKeyPress(Keys.D, null, out player) || inputState.IsNewKeyPress(Keys.Right, null, out player);
        bool left = inputState.IsNewKeyPress(Keys.A, null, out player)|| inputState.IsNewKeyPress(Keys.Left, null, out player);
        if(right)
        {
            dayCurrent++;
        }
        if(left)
        {
            dayCurrent--;
        }
        if(dayCurrent<0) dayCurrent = Math.Min(day.Count-1, levelUnlock);
        if(dayCurrent > levelUnlock)
        {
            dayCurrent = 0;
        }
        if(dayCurrent>=day.Count)
        {
            dayCurrent = 0;
        }
        for(int i = 0; i<day.Count; i++)
        {
            
            if(i  ==  dayCurrent) 
            {
                day[i].color = Color.White;
                day[i].Hovered = true;
            }
            else
            {
                day[i].color = Color.DimGray;
                day[i].Hovered = false;
            }
        }
        if(enter)
        {
            enter = false;
            this.ScreenState = ScreenState.TransitionOff;
            SellingScreen levelScreenManager =  new SellingScreen(game, graphics, dayCurrent, persistentScreen, this);
            var selectedDay = day[dayCurrent]; 
            levelScreenManager.pointTarget = selectedDay.Score;
            screenManager.AddScreen(levelScreenManager);
            
        }
    }
    protected void LoadContent()
    {
        foreach(Days daily in day)
        {
            daily.LoadContent(content);
        }
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if(IsActive == false) return;
        spriteBatch.Begin();
        for(int i =0; i< day.Count; i++)
        {
            day[i].Draw(game,spriteBatch);
        }
        spriteBatch.End();
    }
}