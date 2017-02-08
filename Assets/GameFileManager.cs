using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Assets.Scripts;

namespace Assets
{
    public class GameFileManager
    {
        public static List<LevelStatus> Load(string pathFile)
        {
            if (!File.Exists(pathFile)) return null;
            XmlSerializer formatter = new XmlSerializer(typeof(List<LevelStatus>), new Type[] { typeof(LevelStatus) });
            using (FileStream savedFile = new FileStream(pathFile, FileMode.Open))
            {
                byte[] buffer = new byte[savedFile.Length];
                savedFile.Read(buffer, 0, (int)savedFile.Length);
                using (MemoryStream stream = new MemoryStream(buffer))
                {
                    List<LevelStatus> deserialize = formatter.Deserialize(stream) as List<LevelStatus> ;
                    return deserialize;
                }
            }




        }

        public static void Save(string pathFile, List<LevelStatus> gameStatus)
        {
            using (FileStream outFile = File.Create(pathFile))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(List<LevelStatus>), new Type[] { typeof(LevelStatus)});
                formatter.Serialize(outFile, gameStatus);
                outFile.Close();
            }
        }
    }
}