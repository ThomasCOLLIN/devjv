using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ROTP.Achievements.Common;
using ROTP.Save;

namespace ROTP.Achievements
{
    class FiveVictoryAchievement : Achievement
    {
        private int times;

        public int Times
        {
            get { return times; }
            set { times = value; }
        }

        public FiveVictoryAchievement()
            : base("Winner débutant",
                   "Tu as gagné cinq partie",
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
