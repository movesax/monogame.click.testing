using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace game.click.service
{
    class Container : IClickable, IDrawable
    {
        private Vector2 mbase;
        private Vector2 r;
        private bool withinXBound;
        private bool withinYBound;
        private bool clicked;
        private string name;

        private Texture2D canvas;
        private Color[] colorData;

        public Vector2 Location { get; private set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public bool Show { get; private set; }
        public List<IClickable> ClickableChildren { get; private set; }
        public List<IDrawable> DrawableChildren { get; private set; }


        public Container(Vector2 location, Vector2 rectangle, bool show = false, GraphicsDevice graphics = null, string n = "no name")
        {
            Location = location;
            Width = (int) rectangle.X;
            Height = (int) rectangle.Y;
            Show = show;
            ClickableChildren = new List<IClickable>();
            DrawableChildren = new List<IDrawable>();
            colorData = new Color[Width * Height];
            for(int i = 0; i < colorData.Length; i++) colorData[i] = Color.Aqua;
            canvas = new Texture2D(graphics, Width, Height);
            canvas.SetData(colorData);
            name = n;
        }

        public void AddClickableChild(IClickable child)
        {
            ClickableChildren.Add(child);
        }

        public void AddDrawableChild(IDrawable child)
        {
            DrawableChildren.Add(child);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Show)
            {
                spriteBatch.Begin();
                spriteBatch.Draw(canvas, new Rectangle((int)Location.X, (int)Location.Y, Width, Height), Color.White * 0.5f);
                spriteBatch.End();
            }

            foreach (IDrawable child in DrawableChildren)
                child.Draw(spriteBatch);
  
        }

        public bool hasCursor(MouseState state)
        {
            mbase = new Vector2(state.X, state.Y);
            r = Vector2.Subtract(mbase, Location);

            withinXBound = r.X > 0 && r.X < Width;
            withinYBound = r.Y > 0 && r.Y < Height;
            clicked = withinXBound && withinYBound;

            if (clicked) return true;

            return false;
        }

        private bool clickChildren(MouseState state)
        {
            bool handleClick = true;
            bool hasCursor = false;
            for(int i = ClickableChildren.Count() - 1; i >= 0; i--)
            {
                hasCursor = ClickableChildren[i].hasCursor(state);
                if (hasCursor)
                {
                    handleClick = handleClick && ClickableChildren[i].onClick(state);
                    break;
                }
                
            }
            return handleClick;
        }

        public bool onClick(MouseState state)
        {
            if (clickChildren(state))
            {
                Console.WriteLine($"Click Bubbled to {name}");
            }
            return false;
        }
    }
}
