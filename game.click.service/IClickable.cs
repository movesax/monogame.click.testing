﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;

namespace game.click.service
{
    public interface IClickable
    {
        bool onClick(MouseState state);
        bool hasCursor(MouseState state);
    }
}
