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

public class SellingScreen: GameScreen
{
    //SpriteBatch spriteBatch;
    public int points;
    public int doubt;
    public int pointTarget;
    private const int ROUNDS = 4;
    private int round = 1;
    Animation animation;
    private const int MAXCARDSSELECTED = 4;
    private SpriteFont fontShow;
    private SoundEffect cardSwitching;
    private ContentManager content;
    private GraphicsDevice graphicsDevice;
    private int ncardsSelected = 0;
    private InputState inputState;
    private bool keyDown =  false;
    Rectangle start = new Rectangle(50, 50,100,20);
    Texture2D texture;
    private int _cardCurrent = 0;
    List<Card> hand  = new List<Card>(); 
    List<Card> graveyard = new List<Card>(); 
    List<Card> deck = new List<Card>(); 
    MainChar mc;
    Customer granny;
    private Song backgroundSong;
    public SellingScreen(Game game, GraphicsDevice gD)
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
        mc = new();
        animation = new();
        points = 0;
        doubt = 0;
        pointTarget = 100;
        granny = new(){PersonalityType = Personality.Skeptic};
        mc.Deck = new List<Card>
        {
            new SimpleCard(),
            new Exposition(),
            new Dialogue(),
            new Sponser(),
            new Charm(),
            new GoodOffer(),
            new SimpleCard(),
            new SimpleCard(),
            new Dialogue(),
            new Sponser(),
            new Charm(),
            new GoodOffer(),
            new SimpleCard(),
            new SimpleCard(),
            new Dialogue(),
            new Sponser(),
            new Charm(),
            new GoodOffer(),
            new SimpleCard(),
            new SimpleCard(),
        };
        deck = new List<Card>(mc.Deck);
        graveyard = new List<Card>();
        animation.Reset(this);
        DrawDeck();
        this.LoadContent();
    }
    protected void LoadContent()
    {
        texture = new Texture2D(graphicsDevice, 1, 1);
        texture.SetData(new[]{Color.White});
        //spriteBatch = new SpriteBatch(GraphicsDevice);
        mc.LoadContent(content);
        animation.LoadContent(content);
        granny.LoadContent(content);

        foreach(Card card in mc.Deck) 
        {
            card.LoadContent(content);
        }
        fontShow = content.Load<SpriteFont>("Roboto-BlackItalic");
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
        if(_cardCurrent >= hand.Count) _cardCurrent = 0;
        var keyboardState = inputState.CurrentKeyboardStates[0];
        /*if(keyboardState.IsKeyDown(Keys.Left)|| keyboardState.IsKeyDown(Keys.Right)
        ||keyboardState.IsKeyDown(Keys.A)|| keyboardState.IsKeyDown(Keys.D)) keyDown = true;
        if(keyDown)
        {
            if(keyboardState.IsKeyUp(Keys.Left) ||keyboardState.IsKeyUp(Keys.A)) {_cardCurrent--; keyDown=false;}
            if(keyboardState.IsKeyUp(Keys.Right) ||keyboardState.IsKeyUp(Keys.D)){ _cardCurrent++; keyDown=false;}
        }*/
        if(round > ROUNDS) return;
        PlayerIndex player;
        bool right = inputState.IsNewKeyPress(Keys.D, null, out player) || inputState.IsNewKeyPress(Keys.Right, null, out player);
        bool left = inputState.IsNewKeyPress(Keys.A, null, out player)|| inputState.IsNewKeyPress(Keys.Left, null, out player);
        bool up = inputState.IsNewKeyPress(Keys.W, null, out player) || inputState.IsNewKeyPress(Keys.Up, null, out player) ;
        bool down = inputState.IsNewKeyPress(Keys.S, null, out player) || inputState.IsNewKeyPress(Keys.Down, null, out player);
        if (up && (hand[_cardCurrent].Status == CardPos.NotSelected) &&(ncardsSelected < MAXCARDSSELECTED)){
            ncardsSelected++;
            hand[_cardCurrent].Status = CardPos.Selected;   
        }
        if(down && (hand[_cardCurrent].Status == CardPos.Selected))
        {
            ncardsSelected--;
            hand[_cardCurrent].Status = CardPos.NotSelected;
        }
        if(left)
        {
            _cardCurrent--;
            SoundEffectInstance instance = cardSwitching.CreateInstance();
            instance.Volume = 0.1f;
            instance.Play();
        }
        if(right)
        {
            _cardCurrent++;
            SoundEffectInstance instance = cardSwitching.CreateInstance();
            instance.Volume = 0.1f;
            instance.Play();
            
        }
        if(_cardCurrent>=hand.Count)
        {
            _cardCurrent = 0;
        }
        if(_cardCurrent<0) _cardCurrent = hand.Count-1;
        for(int i = 0; i<hand.Count; i++)
        {
            if(i  ==  _cardCurrent) 
            {
                hand[i].Scale=2f;
                hand[i].Hovered = true;
            }
            else
            {
                hand[i].Scale=1f;
                hand[i].Hovered = false;
            }
        }
        bool enter = inputState.IsNewKeyPress(Keys.Enter, null, out player);
        
        if(ncardsSelected == MAXCARDSSELECTED && enter)
        {
            animation.RoundReset(this); //move this when cards are drawn
            List<Card> cardsPicked = new();
            int inital =  hand.Count-1;
            for(int i = inital ; i>=0; i--)
            {
                if(hand[i].Status == CardPos.Selected)
                {
                    cardsPicked.Add(hand[i]);
                    hand[i].Status = CardPos.NotSelected;
                    Card card = hand[i];
                    graveyard.Add(card);
                    hand.RemoveAt(i);
                }
            }
            round++;
            Summary result = new();
            foreach (Card card in cardsPicked)
            {
                card.Play(this.ScreenManager, result);
            }
            doubt += result.netDoubt;
            points += (int)(result.netPoints * result.netPointMultiplyer) - doubt;
            animation.RoundEnd(result, this);
            DrawDeck();
            ncardsSelected = 0;
        }
        
    }
    public void DrawDeck()
    { 
        int spaces =  100;
        int posX = 100*5;
        int posY = 800;
        var random = new MathHelper.Random();
        while(hand.Count < 5)
        {
            if(deck.Count == 0)
            {
                deck = new List<Card>(graveyard);
                graveyard = new List<Card>();
            }
            int cardIndex = random.Next(deck.Count);   
            Card newCard = deck[cardIndex];       
            hand.Add(newCard);
            deck.RemoveAt(cardIndex); 
        } 
        for(int i = 0; i<hand.Count; i++)
        {
            hand[i].Position = new Vector2(posX + spaces * i, posY);
        }
    }
    public override void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        if(texture == null )return;
        //if(spriteBatch ==null) return;
        spriteBatch.Begin();
        //spriteBatch.Draw(texture,start,new Rectangle(0,0,1,1),Color.DarkOrange);
        granny.Draw(gameTime, spriteBatch);
        animation.Draw(gameTime, spriteBatch);
        for(int i=0 ;i<hand.Count; i++) 
        {
            hand[i].Draw(gameTime, spriteBatch);
        }
        if(round > ROUNDS) spriteBatch.DrawString(fontShow, "Encounter End", new Vector2(200,100), Color.Beige, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
        else spriteBatch.DrawString(fontShow, "Round " + round.ToString(), new Vector2(200,100), Color.Beige, 0f, new Vector2(), 2f, SpriteEffects.None, 0f);
        mc.Draw(gameTime, spriteBatch);
        //spriteBatch.Draw(texture, new Rectangle(0,0,50,50), Color.Red);
        spriteBatch.End();
    }

}