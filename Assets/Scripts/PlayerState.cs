using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace DefaultNamespace
{
    public class PlayerData
    {
        private string _path => Path.Combine(Application.persistentDataPath, "playerData.json");
        public PlayerState PlayerState;
        public int CurrentChapter;

        public PlayerData()
        {
           Load(); 
        }

        public void Load()
        {
            if (!File.Exists(_path))
            {
                PlayerState = new PlayerState()
                {
                    PlayerChapterStates = new List<PlayerChapterState>()
                };
                return;
            }
            
            var fileData = File.ReadAllText(_path);
            PlayerState = JsonUtility.FromJson<PlayerState>(fileData);
        }

        public void Save()
        {
            var data = JsonUtility.ToJson(PlayerState);
            File.WriteAllText(_path, data);
        }
    }
    
    [Serializable]
    public class PlayerState
    {
        public List<PlayerChapterState> PlayerChapterStates;
    }

    [Serializable]
    public class PlayerChapterState
    {
        public int ChapterIndex;
        public int CompletedLevels;
    }
}