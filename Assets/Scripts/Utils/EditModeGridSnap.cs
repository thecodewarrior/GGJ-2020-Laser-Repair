using Puzzle;
using UnityEngine;

namespace Utils
{
    [ExecuteAlways]
    public class EditModeGridSnap : MonoBehaviour
    {
        public string gridName;
        public Vector3 localOffset;

        void Update()
        {
            if (Application.IsPlaying(gameObject)) return;
            
            var offset = transform.TransformVector(localOffset);

            var grid = SocketGrid.Find(gridName);
            if (grid)
            {
                transform.position = grid.Snap(transform.position - offset) + offset;
            }
        }
    }
}