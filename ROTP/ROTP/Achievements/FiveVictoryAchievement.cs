using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ROTP.Achievements.Common;
using ROTP.Save;

namespace ROTP.Achievements
{
    [Serializable]
    public class FiveVictoryAchievement : Achievement
    {
        public FiveVictoryAchievement()
            : base("Winner debutant",
                   "Tu as gagne cinq partie",
                   "Menu/Achievements/victory_bronze", 5)
        {
        }

        protected override bool IsConditionOk()
        {
            return false;
        }
    }
}
