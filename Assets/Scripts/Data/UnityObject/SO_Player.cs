using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "SO_Player", menuName = "Data/SO_Player", order = 0)]
    public class SO_Player : ScriptableObject
    {
        public PlayerData playerData;
    }
}