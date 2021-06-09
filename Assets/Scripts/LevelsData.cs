using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LevelsData
{
    public FileData FileData;

    public string GetPath(int index)
    {
        return Path.Combine(Application.persistentDataPath, $"Level{index}.json");
    }

    public void CheckNeedToAdd(Level level)
    {
        if (FileData.Levels.Count == level.LevelIndex)
        {
            FileData.Levels.Add(level);
        }
    }
    
    public LevelsData()
    {
       Load(); 
    }

    public void Load()
    {
        var files = Directory.GetFiles(Application.persistentDataPath).Where(x => x.Contains("Level")).ToList();
        
        var fileDatas = files.Select(x => File.ReadAllText(x)).ToList();

        var levelsData = fileDatas.Select(x => JsonUtility.FromJson<Level>(x));
        FileData = new FileData()
        {
            Levels = levelsData.ToList()
        };
        foreach (var obj in FileData.Levels.Select((x, i) => (x, i)))
        {
            obj.x.LevelIndex = obj.i;
        }
    }

    public void Save()
    {
        for (int i = 0; i < FileData.Levels.Count; i++)
        {
            var data = JsonUtility.ToJson(FileData.Levels[i]);
            var path = GetPath(i);
            File.WriteAllText(path, data);
        }
    }
}

[Serializable]
public class FileData
{
    public List<Level> Levels;
}

[Serializable]
public class Level
{
    public int LevelIndex { get; set; }
    public List<Element> Elements;
}

[Serializable]
public class Element
{
    public string ElementName;
    public Vector3 Position;
    public bool HelpElement;
}
