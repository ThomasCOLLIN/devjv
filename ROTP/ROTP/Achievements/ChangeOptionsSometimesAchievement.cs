using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ROTP.Achievements.Common;
using ROTP.Save;

namespace ROTP.Achievements
{
    [Serializable]
    public class ChangeOptionsSometimesAchievement : Achievement
    {
        public ChangeOptionsSometimesAchievement()
            : base("Changeur d'options experimente",
                   "Tu as change la configuration du jeu deux fois!!!!",
                   "Menu/Achievements/options_silver", 2)
        {
            SaveManager.OptionsChanged += onEvent;
        }

        protected override bool IsConditionOk()
        {
            return true;
        }
    }
}
