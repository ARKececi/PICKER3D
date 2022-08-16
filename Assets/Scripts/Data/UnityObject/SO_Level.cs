using System.Collections.Generic;
using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "SO_Level", menuName = "Data/SO_Level", order = 0)]
    
    public class SO_Level : ScriptableObject
    {
        public List<LevelData> Levels = new List<LevelData>();
    }
}