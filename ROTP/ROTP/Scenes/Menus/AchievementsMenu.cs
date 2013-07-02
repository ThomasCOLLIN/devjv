using ROTP.Scenes.Menus.Common;
using ROTP.Achievements.Common;
using System;

namespace ROTP.Scenes.Common
{
    class AchievementsMenu : Menu
    {
        public AchievementsMenu(SceneManager manager)
            : base(manager, "Succes")
        {
            foreach (Achievement ach in AchievementManager.Instance.GetAll())
                MenuItems.Add(new MenuItem(ach.Name + " " + ach.IsOwned));

            MenuItem cancel = new MenuItem("Retour");
            cancel.Selected += (s, args) => { OnCancel(); };
            MenuItems.Add(cancel);
        }
    }
}
