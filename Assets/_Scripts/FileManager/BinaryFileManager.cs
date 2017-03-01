using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Assets.Scripts
{
    public class BinaryFileManager
    {


        public static List<LevelStatus> Load(string pathFile)
        {
            if (!File.Exists(pathFile)) return null;
            IFormatter formatter = new BinaryFormatter();
            return Deseralize(formatter, FileToByteArray(pathFile));

        }

        public static void Save(string pathFile, List<LevelStatus> gameStatus)
        {
            using (FileStream outFile = File.Create(pathFile))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(outFile, gameStatus);
            }
        }

        #region Private Methods

        private static byte[] FileToByteArray(string pathFile)
        {
            using (FileStream savedFile = new FileStream(pathFile, FileMode.Open))
            {
                var buffer = new byte[savedFile.Length];
                savedFile.Read(buffer, 0, (int)savedFile.Length);
                return buffer;
            }
        }

        private static List<LevelStatus> Deseralize(IFormatter formatter, byte[] buffer)
        {
            using (MemoryStream stream = new MemoryStream(buffer))
            {
                List<LevelStatus> deserialize = formatter.Deserialize(stream) as List<LevelStatus>;
                return deserialize;
            }
        }

        #endregion
    }
}