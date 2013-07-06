using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ROTP.Scenes.Common;
using ROTP.Save;
using System.Xml.Serialization;

namespace ROTP.Achievements.Common
{
    [Serializable]
    [XmlInclude(typeof(ChangeOptionsAchievement))]
    [XmlInclude(typeof(ChangeOptionsSometimesAchievement))]
    [XmlInclude(typeof(ChangeOptionsALotAchievement))]
    [XmlInclude(typeof(FiveVictoryAchievement))]
    public abstract class Achievement
    {
        private bool isOwned = false;
        private string name = "";
        private string description = "";
        private string imagePath = "";

        private int currentTimes = 0;
        private int neededTimes;

        public string Name
        {
            get { return name; }
        }

        public string Description
        {
            get { return description; }
        }

        public string ImagePath
        {
            get { return imagePath; }
        }

        public bool IsOwned
        {
            get { return isOwned; }
            set { isOwned = value; }
        }

        public int CurrentTimes
        {
            get { return currentTimes; }
            set { currentTimes = value; }
        }

        public Achievement(string name, string description, string imagePath, int neededTimes = 1)
        {
            this.neededTimes = neededTimes;
            this.name = name;
            this.description = description;
            this.imagePath = imagePath;
        }

        protected void saveAchievement()
        {
            SaveManager.SaveAchievements(AchievementManager.Instance.GetAll());
        }

        protected void onEvent(object sender, EventArgs args)
        {
            if (!isOwned)
            {
                if (IsConditionOk())
                    currentTimes++;

                if (currentTimes >= neededTimes)
                    isOwned = true;

                saveAchievement();
            }
        }

        protected abstract bool IsConditionOk();
    }
}
