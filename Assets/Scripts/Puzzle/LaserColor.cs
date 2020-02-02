using UnityEngine;

namespace Puzzle
{
    [CreateAssetMenu(fileName = "Color", menuName = "LaserRepair/LaserColor")]
    public class LaserColor : ScriptableObject
    {
        public string id;
        public Material material;
        public float width = 0.25f;
    }
}