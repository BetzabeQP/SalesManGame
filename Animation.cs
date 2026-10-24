using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace SalesManGame;
public class Summary
{
    public int netPoints = 0;
    public const float NETPERCENTAGE = 0.25f;
    public int netCash = 0;
    public int netDoubt = 0;
    public int netPatience = 0;
    public float netPointMultiplyer = 1.0f;
}
public class Animation
{
    public float Scale = 2.5f;
    private int roundTarget;
     private float animStart;
    private float animEnd;
    private int pointsTotal = 0;
    public string Objective {get;set;} = string.Empty;
    public string ShowOff {get;set;} = string.Empty;
    Texture2D barMeter;
    Rectangle fillingProgression = new(0,0,0, 2);
    Texture2D filling;
    SpriteFont font;
   // private SpriteBatch _spriteBatch;
    public Animation()
    {
    }
    public void LoadContent(ContentManager content)
    {
        barMeter = content.Load<Texture2D>("Bar");
        filling = content.Load<Texture2D>("Filling");
        font = content.Load<SpriteFont>("Roboto-BlackItalic");
    }
    public void UpdateState(Summary results)
    {
        pointsTotal += results.netPoints;
        fillingProgression.Width += pointsTotal;
    }

    public void Reset(SellingScreen sc)
    {
        roundTarget = sc.pointTarget;
        animStart = 0;
        animEnd = 0;
        
    }
    public void RoundReset(SellingScreen screen)
    {
        animEnd = screen.points;
        animStart = animEnd;
    }
    //public bool DidTheyPastThreshold()
    //{
        //if (pointsTotal >= threasholdPoints) return true;
        //else return false;
    //}
    public void RoundEnd(Summary current, SellingScreen screen)
    {
        float currentDoubt = screen.doubt + current.netDoubt;
        animEnd += (float)current.netPoints * current.netPointMultiplyer - current.netDoubt;
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        int posBarX =140*4;
        if(roundTarget == 0) return;
        animStart += (animEnd - animStart)/5;
        if(Math.Abs(animEnd - animStart) < 2) animStart = animEnd;
        fillingProgression.Width = (int)((animStart/roundTarget) * filling.Width);
        //spriteBatch.DrawString(font, animStart.ToString(), new(300, 300), Color.Black, 0f, new Vector2(0,0), 2f, SpriteEffects.None, 0f);
        spriteBatch.Draw(barMeter, new Vector2(posBarX, 50), null,Color.White, 0, new Vector2(0,0),Scale,SpriteEffects.None, 0f);
        spriteBatch.Draw(filling, new Vector2(posBarX+17, 55), null ,Color.Yellow, 0,new Vector2(0,0),scale: new Vector2(animStart/(float)roundTarget * Scale, Scale),SpriteEffects.None, 0f);
        spriteBatch.Draw(filling, new Vector2(posBarX+17, 55), null ,Color.Red, 0,new Vector2(0,0),scale: new Vector2(Math.Min(animStart/(float)roundTarget, 1.0f) * Scale, Scale),SpriteEffects.None, 0f);
       
    }
}