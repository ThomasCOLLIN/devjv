using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Storage;
using System.IO;
using System.Xml.Serialization;
using Microsoft.Xna.Framework;

namespace ROTP.Save
{
    class SaveManager
    {
        private static readonly string optionPath = "User/options";

        public static bool SaveOptions(Options options)
        {
            try 
	        {	        
                FileStream file = File.Open(optionPath, FileMode.OpenOrCreate);
                XmlSerializer serializer = new XmlSerializer(typeof(Options));
                serializer.Serialize(file, options);
                file.Close();
                return true;
	        }
	        catch (Exception)
	        {
                return false;
	        }
        }

        public static Options LoadOptions()
        {
            try
            {
                FileStream file = File.Open(optionPath, FileMode.Open);
                XmlSerializer serializer = new XmlSerializer(typeof(Options));
                Options options = (Options)serializer.Deserialize(file);
                file.Close();

                return options;
            }
            catch (Exception)
            {
                Options options = new Options();
                options.IsFullScreen = false;
                options.Resolution = new Vector2(1024, 720);
                options.IsBgmOn = true;
                options.IsSoundOn = true;

                return options;
            }
        }
    }
}
