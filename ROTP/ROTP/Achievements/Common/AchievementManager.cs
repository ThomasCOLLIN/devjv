using System;
using System.Collections.Generic;
using System.Linq;
using ROTP.Save;

namespace ROTP.Achievements.Common
{
    class AchievementManager
    {
        private static AchievementManager _instance;
        public static AchievementManager Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new AchievementManager();
                return AchievementManager._instance;
            }
        }

        private List<Achievement> achievements;

        private AchievementManager()
        {
            achievements = SaveManager.LoadAchievements();
        }

        public List<Achievement> GetAll()
        {
            return achievements;
        }

        public Achievement GetAchievement(Type type)
        {
            return achievements.FirstOrDefault(ach => ach.GetType() == type);
        }

        public void ResetAchievements()
        {
            achievements = GetEmptyAchievementList();
            SaveManager.SaveAchievements(achievements);
        }

        public static List<Achievement> GetEmptyAchievementList()
        {
            List<Achievement> achievements = new List<Achievement>();

            achievements.Add(new ChangeOptionsAchievement());
            achievements.Add(new ChangeOptionsALotAchievement());

            return achievements;
        }

        public static void Load()
        {
            _instance = new AchievementManager();
        }
    }
}
