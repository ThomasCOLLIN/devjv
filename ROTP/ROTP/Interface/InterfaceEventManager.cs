using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;

namespace ROTP.Interface
{
    public delegate void EventInterfaceClickTowerHandler(SpriteBatch sb);

    public static class InterfaceEventManager
    {
        public static event EventInterfaceClickTowerHandler Messenger;

        public static void InvokeMessenger(SpriteBatch sb)
        {
            EventInterfaceClickTowerHandler handler = Messenger;
            if (handler != null)
                handler(sb);
        }

    }
}
