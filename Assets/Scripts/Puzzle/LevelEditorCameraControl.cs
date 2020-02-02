using UnityEngine;

namespace Puzzle
{
    public class LevelEditorCameraControl : MonoBehaviour
    {
        public void IncreaseDistance()
        {
            var pos = transform.position;
            pos.z--;
            transform.position = pos;
        }
        
        public void DecreaseDistance()
        {
            var pos = transform.position;
            pos.z++;
            transform.position = pos;
        }
    }
}