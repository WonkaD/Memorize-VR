using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Assets.Scripts
{
    public class BinaryFileManager
    {


        public static T Load <T> (string pathFile) where T:class,new()
        {
            if (!File.Exists(pathFile)) return new T();
            IFormatter formatter = new BinaryFormatter();
            return Deseralize<T> (formatter, FileToByteArray(pathFile));

        }

        public static void Save<T>(string pathFile, T gameStatus)
        {
            using (FileStream outFile = File.Create(pathFile))
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(outFile, gameStatus);
            }
        }

        public static void Remove(string pathFile)
        {
            if (File.Exists(pathFile))
                File.Delete(pathFile);
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

        private static T Deseralize <T> (IFormatter formatter, byte[] buffer) where T:class, new()
        {
            using (MemoryStream stream = new MemoryStream(buffer))
            {
                T deserialize = formatter.Deserialize(stream) as T;
                return deserialize?? new T();
            }
        }

        #endregion
    }
}