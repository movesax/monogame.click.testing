using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace game.click.service
{
    class SceneObject : IClickable, IDrawable
    {
        public Texture2D Texture { get; set; }
        public Vector2 Location { get; set; }
        public float Angle { get; set; }
        public SpriteEffects Effect { get; set; }
        public float Scale { get;set; }
        public float _Scale { get; private set; }
        public Vector2 RotOrigin { get; set; }

        private Color[] colorBits;
        private Vector2 rotBase;

        //for hasCursor
        private bool withinXBound;
        private bool withinYBound;
        private bool canvasClicked;
        private Vector2 mbase;
        private Vector2 r;
        private float rotRx;
        private float rotRy;
        private int xOffset;
        private int yOffset;
        private int colorIndex;
        private string name;

        public SceneObject(Texture2D texture, Vector2 location, SpriteEffects effect = SpriteEffects.None, float scale = 1, float angle = 0, Vector2 rotOrigin = new Vector2(), string n = "no name")
        {
            Texture = texture;
            Location = location;
            Angle = angle;
            Effect = effect;
            Scale = scale;
            _Scale = (float) 1.0 / scale;
            RotOrigin = rotOrigin;
            colorBits = new Color[texture.Width * texture.Height];
            Texture.GetData(colorBits);
            rotOrigin = new Vector2(0, 0);
            rotBase = Vector2.Add(Location, RotOrigin);
            name = n;
        }

        public void Update(Vector2? location = null, SpriteEffects? effect = null, float? scale = null, float? angle = null, Vector2? rotOrigin = null)
        {
            
            if(location != null) Location = location ?? new Vector2(0,0);
            if(effect != null) Effect = effect ?? SpriteEffects.None;
            if(angle != null) Angle = angle ?? 0;
            if(scale != null) Scale = scale ?? 1;
            if (rotOrigin != null) RotOrigin = rotOrigin ?? new Vector2(0, 0);

            if(location !=null && effect != null && angle != null && scale != null && rotOrigin != null)
            {// something has been updated
              rotBase = Vector2.Add(Location, RotOrigin);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Begin();
            spriteBatch.Draw(Texture, Location, null, Color.White, Angle, RotOrigin, Scale, Effect, 1);
            spriteBatch.End();
        }

        public bool onClick(MouseState state)
        {
            Console.WriteLine($"{name} clicked");
            return false;
        }

        public bool hasCursor(MouseState state)
        {
            mbase = new Vector2(state.X, state.Y);
            r = Vector2.Subtract(rotBase, mbase);

            rotRx = (r.Y * (float) Math.Sin(Angle) + r.X*(float) Math.Cos(Angle))*_Scale;
            rotRy = (r.Y * (float) Math.Cos(Angle) - r.X*(float) Math.Sin(Angle))*_Scale;

            xOffset = (Effect == SpriteEffects.FlipHorizontally) ? (int)(Texture.Width + rotRx) : (int)(-1*rotRx);
            yOffset = (Effect == SpriteEffects.FlipVertically) ? (int)(Texture.Height + rotRy) : (int)(-1*rotRy);
            colorIndex = yOffset * Texture.Width + xOffset;

            withinXBound = xOffset > 0 && xOffset < Texture.Width;
            withinYBound = yOffset > 0 && yOffset < Texture.Height;
            canvasClicked = withinXBound && withinYBound;

            if (canvasClicked) return (colorBits[colorIndex].A != 0);

            return false;
           
        }
    }
}
