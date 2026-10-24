using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
namespace SalesManGame;
public enum CardPos
{
    Selected,
    NotSelected
}
public abstract class Card
{
    public CardPos Status = CardPos.NotSelected;
    public bool Hovered = false;
    public float Scale = 1f;
    private const float CONSCALE = 2f;
    public List<Perk> Perks = new();
    private Texture2D texture;
    private Texture2D textureCard;
    private SpriteFont font;
    private string title;
    private string description;
    public Vector2 Position = new Vector2(0,0);
    private bool drawable = false;
    public void LoadContent(ContentManager content)
    {
        if(content is null) return;
        texture = content.Load<Texture2D>(GetTextureName());
        textureCard = content.Load<Texture2D>(GetCardPicture());
        font = content.Load<SpriteFont>("Roboto-BlackItalic");
        
    }
    public void PrepareDraw()
    {
        drawable = true;
        title = GetTitleCard();
        int width = texture.Width;
        
        description = WordWrap(GetDescription(), width, font);
    }
    private string WordWrap(string text, float width, SpriteFont font)
    {
        List<string> Words = text.Split(' ').ToList<string>();
        string finalString = "";
        while(Words.Count >0)
        {
            string curString = Words[0];
            Words.RemoveAt(0);
            while(Words.Count > 0)
            {
                string stringTemp = curString + " " + Words[0];
                if (font.MeasureString(stringTemp).X/3<width)
                {
                    curString = stringTemp;
                    Words.RemoveAt(0);
                }
                else
                {
                    curString += '\n';
                    break;
                }

            }
            finalString += curString;
        }
        return finalString;
    }
    public void Draw(GameTime gameTime, SpriteBatch spriteBatch)
    {
        float nudge = 0.1f; 
        if(!drawable) PrepareDraw();
        float drawScale = Scale*CONSCALE;
        Vector2 origin = new Vector2(Position.X - (texture.Width/2)*drawScale, Position.Y - (texture.Height/2)*drawScale);
        if(Status == CardPos.Selected)
        {
            origin.Y -= 100;
            
        }
        if(Hovered == true) nudge = 0f;
        Vector2 offset = new Vector2((texture.Width-textureCard.Width)/2*drawScale, (texture.Width-textureCard.Width + 10)/2*drawScale);
        Vector2 titleOffset = new Vector2((texture.Width - (font.MeasureString(title).X/3))/2*drawScale, 0);
        Vector2 descOffset = new Vector2((texture.Width - (font.MeasureString(description).X/3))/2*drawScale, textureCard.Height *drawScale);
        //Vector2 final = new Vector2(Position.X+offset, Position.Y-(texture.Height/2f)*drawScale);
        spriteBatch.Draw(texture, origin, null, Color.White, 0, new Vector2(0,0), drawScale, SpriteEffects.None, nudge);
        spriteBatch.Draw(textureCard, origin + offset, null, Color.White, 0, new Vector2(0,0), drawScale, SpriteEffects.None, nudge);
        spriteBatch.DrawString(font, title, origin + titleOffset, new Color(34,32,52), 0f, new Vector2(0,0), drawScale/3f, SpriteEffects.None, nudge);
        spriteBatch.DrawString(font, description, origin + descOffset + offset, new Color(34,32,52), 0f, new Vector2(0,0), drawScale/3f, SpriteEffects.None, nudge);
    }
    public abstract void Play(ScreenManager screen, Summary results);

    public abstract string GetTextureName();
    public abstract string GetCardPicture();
    public abstract string GetTitleCard();
    public abstract string GetDescription();
    
}