using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using ROTP.Achievements.Common;
using ROTP.Options;
using ROTP.Achievements;

namespace ROTP.Save
{
    class SaveManager
    {
        private static readonly string optionPath = "options.sav";
        private static readonly string achievementsPath = "achievements.sav";

        #region event OptionChanged handler
        public static event EventHandler OptionsChanged;

        private static void onOptionsChanged()
        {
            if (OptionsChanged != null)
                OptionsChanged(null, new EventArgs());
        }
        #endregion

        #region achievements
        public static bool SaveAchievements(List<Achievement> achievements)
        {
            return Save(achievementsPath, achievements);
        }

        public static List<Achievement> LoadAchievements()
        {
            List<Achievement> achievs = (List<Achievement>)Load(achievementsPath, typeof(List<Achievement>));

            if (achievs == null)
                achievs = AchievementManager.GetEmptyAchievementList();

            return achievs;
        }
        #endregion
        
        #region options
        public static bool SaveOptions(RotpOptions options)
        {
            onOptionsChanged();
            return Save(optionPath, options);
        }

        public static RotpOptions LoadOptions()
        {
            RotpOptions options = (RotpOptions) Load(optionPath, typeof(RotpOptions));

            if (options == null)
            {
                options = new RotpOptions();
                options.IsFullScreen = false;
                options.Resolution = new Vector2(1024, 720);
                options.IsBgmOn = true;
                options.IsSoundOn = true;

            }

            return options;
        }
        #endregion

        #region general Save/Load
        private static bool Save(string path, object obj)
        {
            FileStream file = null;

            try
            {
                file = File.Open(path, FileMode.OpenOrCreate);
                XmlSerializer serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(file, obj);
                file.Close();
                return true;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);
                if (file != null)
                    file.Close();
                return false;
            }
        }

        private static object Load(string path, Type type)
        {
            FileStream file = null;

            try
            {
                file = File.Open(path, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(type);
                object obj = serializer.Deserialize(file);
                file.Close();

                return obj;
            }
            catch (Exception e)
            {
                Trace.WriteLine(e.Message);

                if (file != null)
                    file.Close();

                return null;
            }
        }
        #endregion
    }
}
