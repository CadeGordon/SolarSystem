using System;
using System.Collections.Generic;
using System.Text;
using Raylib_cs;
using MathLibrary;

namespace MathForGames
{
    
    class UIText : Actor
    {
        public string Text;
        public int Width;
        public int Height;
        private Font Font;
        public int FontSize;
        public Color FontColor;

        public UIText(float x, float y, string name, Color color, int width, int height, int fontsize, string text = "")
            : base (x, y, name, "")
        {
            Text = text;
            Width = width;
            Height = height;
            FontSize = fontsize;
            Font = Raylib.LoadFont("resources/fonts/alagrad.png");
            FontColor = color;

        }

        public override void Draw()
        {
            //Create a new rectangel that willa ct as the borders of the text box
            Rectangle textBox = new Rectangle(LocalPosition.X, LocalPosition.Y, Width, Height);
            //Draw text box
            Raylib.DrawTextRec(Font, Text, textBox, FontSize, 1, true, FontColor);
        }
    }
}
