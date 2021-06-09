using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "Chapter")]
    public class ChapterData : ScriptableObject
    {
        public Sprite Image;
        public List<PlayableLevel> Levels;
        public Achievement AchievementToComplete;
    }
}