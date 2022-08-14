using Data.ValueObject;
using UnityEngine;

namespace Data.UnityObject
{
    [CreateAssetMenu(fileName = "SO_Input", menuName = "Data/SO_Input", order = 0)]
    public class SO_Input : ScriptableObject
    {
        public InputData InputData;
    }
}