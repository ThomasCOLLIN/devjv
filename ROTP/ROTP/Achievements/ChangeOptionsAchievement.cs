﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ROTP.Achievements.Common;
using ROTP.Save;

namespace ROTP.Achievements
{
    [Serializable]
    public class ChangeOptionsAchievement : Achievement
    {
        public ChangeOptionsAchievement()
            : base("Changeur d'options",
                   "Tu as changé la configuration du jeu!!!!",
                   "")
        {
            SaveManager.OptionsChanged += onEvent;
        }

        protected override void onEvent(object sender, EventArgs args)
        {
            if (!IsOwned)
            {
                IsOwned = true;
                saveAchievement();
            }
        }
    }
}
