using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using Assets.Scripts;
using Assets._Scripts.Game;
using Assets._Scripts.Level;
using UnityEngine;

namespace Assets._Scripts.Save_Game
{
    public class SaveGameManager
    {
        public static readonly string path = Application.persistentDataPath + "/Save/";
        public static readonly string file = "SaveGame.bin";

        public static void Remove()
        {
            if (File.Exists(FullNameFile()))
                File.Delete(FullNameFile());
        }

        public static void Create()
        {
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
        }

        public static string FullNameFile()
        {
            return path + file;
        }

    }
}