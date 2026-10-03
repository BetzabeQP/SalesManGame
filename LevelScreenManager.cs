using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using  Microsoft.Xna.Framework.Media;

namespace SalesManGame;

public class LevelScreenManager: GameScreen
{
    //SpriteBatch spriteBatch;
    private SoundEffect cardSwitching;
    private ContentManager content;
    private GraphicsDevice graphicsDevice;
    private InputState inputState;
    private bool keyDown =  false;
    Rectangle start = new Rectangle(50, 50,100,20);
    Texture2D texture;
    List<Texture2D> listTexture =  new List<Texture2D>();
    private int _cardCurrent;
    Cards[] cards; 
    MainChar mc;
    Customer granny;
    private Song backgroundSong;
    public LevelScreenManager(Game game, GraphicsDevice gD)
    {
        graphicsDevice = gD;
        content = new ContentManager(game.Services);
        content.RootDirectory = "Content";
    }
    public override void HandleInput(GameTime gameTime, InputState input)
    {
        inputState = input;
        base.HandleInput(gameTime, input);
    }
    public override void Activate()
    {
        //mc = new();
        granny = new();
        cards = new Cards[]
        {
            new Cards(){Position = new Vector2(100,200)},
            new Cards(){Position = new Vector2(200, 200)},
            new Cards(){Position = new Vector2(300,200)},
            new Cards(){Position = new Vector2(400, 200)},
            new Cards(){Position = new Vector2(500,200)},
            new Cards(){Position = new Vector2(600, 200)}
        };
        this.LoadContent();
    }
    protected void LoadContent()
    {
        texture = new Texture2D(graphicsDevice, 1, 1);
        texture.SetData(new[]{Color.White});
        /*spriteBatch = new SpriteBatch(GraphicsDevice);
        mc.LoadContent(content);*/
        granny.LoadContent(content);
        foreach(Cards card in cards) 
        {
            card.LoadContent(content);
        }
        cardSwitching = content.Load<SoundEffect>("HeavySwing");
        backgroundSong = content.Load<Song>("John Bartmann - Gameshow Brazz");
        MediaPlayer.IsRepeating = true;
        MediaPlayer.Volume = 0.05f;
        MediaPlayer.Play(backgroundSong);
    }
    public override void Unload()
    {
        content.Unload();
        base.Unload();
    }
    public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
    {

        if(inputState is null) return;
        var keyboardState = inputState.CurrentKeyboardStates[0];
        /*if(keyboardState.IsKeyDown(Keys.Left)|| keyboardState.IsKeyDown(Keys.Right)
        ||keyboardState.IsKeyDown(Keys.A)|| keyboardState.IsKeyDown(Keys.D)) keyDown = true;
        if(keyDown)
        {
            if(keyboardState.IsKeyUp(Keys.Left) ||keyboardState.IsKeyUp(Keys.A)) {_cardCurrent--; keyDown=false;}
            if(keyboardState.IsKeyUp(Keys.Right) ||keyboardState.IsKeyUp(Keys.D)){ _cardCurrent++; keyDown=false;}
        }*/
        PlayerIndex player;
        if(inputState.IsNewKeyPress(Keys.A, null, out player)
        || inputState.IsNewKeyPress(Keys.Left, null, out player))
        {
            _cardCurrent--;
            SoundEffectInstance instance = cardSwitching.CreateInstance();
            instance.Volume = 0.1f;
            instance.Play();
        }
        if(inputState.IsNewKeyPress(Keys.D, null, out player)
        || inputState.IsNewKeyPress(Keys.Right, null, out player))
        {
            _cardCurrent++;
            SoundEffectInstance instance = cardSwitching.CreateInstance();
            instance.Volume = 0.1f;
            instance.Play();
            
        }
        if(_cardCurrent>=cards.Length)
        {
            _cardCurrent = 0;
        }
        if(_cardCurrent<0) _cardCurrent = cards.Length-1;
        for(int i = 0; i<cards.Length; i++)
        {
            if(i ==  _cardCurrent) cards[i].Scale=2f;
            else cards[i].Scale=1f;
        }
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if(texture == null )return;
        //if(spriteBatch ==null) return;
        spriteBatch.Begin();
        spriteBatch.Draw(texture,start,new Rectangle(0,0,1,1),Color.DarkOrange);
        //granny.Draw(gameTime, spriteBatch);
        foreach(Cards c in cards) c.Draw(gameTime, spriteBatch);
        //spriteBatch.Draw(texture, new Rectangle(0,0,50,50), Color.Red);
        spriteBatch.End();
    }

}