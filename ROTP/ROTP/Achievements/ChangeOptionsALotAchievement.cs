using System;
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
        private int times;

        public int Times
        {
            get { return times; }
            set { times = value; }
        }

        public ChangeOptionsALotAchievement()
            : base("Changeur d'options fou",
                   "Tu as changé la configuration du jeu cinq fois!!!!",
                   "")
        {
            SaveManager.OptionsChanged += onEvent;
        }

        protected override void onEvent(object sender, EventArgs args)
        {
            if (!IsOwned)
            {
                if (++times >= 5)
                    IsOwned = true;
                saveAchievement();
            }
        }
    }
}
