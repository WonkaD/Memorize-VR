using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace Assets.Scripts
{
    public class GameStatusHistoryMode
    {
        public Dictionary<int, Level> Levels;
        private const string PathFile = "gameSave";

        public void SaveStatus()
        {
            FileStream outFile = File.Create(PathFile);
            XmlSerializer formatter = new XmlSerializer(this.GetType());
            formatter.Serialize(outFile, this);
        }

        public void LoadStatus()
        {
            XmlSerializer formatter = new XmlSerializer(this.GetType());
            FileStream savedFile = new FileStream(PathFile, FileMode.Open);
            byte[] buffer = new byte[savedFile.Length];
            savedFile.Read(buffer, 0, (int)savedFile.Length);
            MemoryStream stream = new MemoryStream(buffer);
            GameStatusHistoryMode deserialize =  formatter.Deserialize(stream) as GameStatusHistoryMode;
            if (deserialize != null) this.Levels = deserialize.Levels;

        }

    }
}