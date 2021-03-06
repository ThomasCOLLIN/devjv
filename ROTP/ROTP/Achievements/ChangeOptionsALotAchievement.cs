﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ROTP.Achievements.Common;
using ROTP.Save;

namespace ROTP.Achievements
{
    [Serializable]
    public class ChangeOptionsALotAchievement : Achievement
    {
        public ChangeOptionsALotAchievement()
            : base("Maitre des options",
                   "Tu as change la configuration du jeu trois fois!!!!",
                   "Menu/Achievements/options_gold", 3)
        {
            SaveManager.OptionsChanged += onEvent;
        }

        protected override bool IsConditionOk()
        {
            return true;
        }
    }
}
