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
    [XmlInclude(typeof(ChangeOptionsALotAchievement))]
    public abstract class Achievement
    {
        private bool isOwned = false;
        private string name = "";
        private string description = "";
        private string imagePath = "";

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

        public Achievement(string name, string description, string imagePath)
        {
            this.name = name;
            this.description = description;
            this.imagePath = imagePath;
        }

        protected void saveAchievement()
        {
            SaveManager.SaveAchievements(AchievementManager.Instance.GetAll());
        }

        protected abstract void onEvent(object sender, EventArgs args);
    }
}
