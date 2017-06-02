using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Assets.Scripts
{
    public class XmlFileManager
    {


        public static T Load<T>(string pathFile) where T:class,new()
        {
            if (!File.Exists(pathFile)) return new T();
            XmlSerializer formatter = new XmlSerializer(typeof(T), new Type[] { typeof(T) });
            return Deseralize <T>(formatter, FileToByteArray(pathFile));

        }

        public static void Save <T>(string pathFile, T gameStatus)
        {
            using (FileStream outFile = File.Create(pathFile))
            {
                XmlSerializer formatter = new XmlSerializer(typeof(T), new Type[] { typeof(T) });
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

        private static T Deseralize<T>(XmlSerializer formatter, byte[] buffer) where T : class,new()
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