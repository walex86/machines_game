using System;
using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "PlayableLevel")]
    public class PlayableLevel : ScriptableObject
    {
        [SerializeField] private TextAsset LevelData;
        public Level Level => JsonUtility.FromJson<Level>(LevelData.text);
        public List<AvailableElement> AvailableElements;
    }

    [Serializable]
    public class AvailableElement
    {
        public ElementData Element;
        public int Count;
    }
}