using System;
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
                   "Tu as change la configuration du jeu!!!!",
                   "Menu/Achievements/options_bronze")
        {
            SaveManager.OptionsChanged += onEvent;
        }

        protected override bool IsConditionOk()
        {
            return true;
        }
    }
}
