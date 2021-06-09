using System.Collections.Generic;
using UnityEngine;

namespace DefaultNamespace
{
    [CreateAssetMenu(fileName = "ChaptersDataBase")]
    public class ChaptersDataBase : ScriptableObject
    {
        public List<ChapterData> Chapters;
    }
}