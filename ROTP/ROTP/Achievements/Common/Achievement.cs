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
    public abstract class Achievement
    {
        private bool isOwned = false;
        private string name = "";
        private string description = "";
        private string imagePath = "";

        [XmlElement]
        public string Name
        {
            get { return name; }
        }

        [XmlElement]
        public string Description
        {
            get { return description; }
        }

        [XmlElement]
        public string ImagePath
        {
            get { return imagePath; }
        }

        [XmlElement]
        public bool IsOwned
        {
            get { return isOwned; }
        }

        public Achievement(string name, string description, string imagePath)
        {
            this.name = name;
            this.description = description;
            this.imagePath = imagePath;
        }

        protected void validateAchievement()
        {
            isOwned = true;
            SaveManager.SaveAchievements(AchievementManager.Instance.GetAll());
        }

        public abstract void onEvent(object sender, EventArgs args);
    }
}
