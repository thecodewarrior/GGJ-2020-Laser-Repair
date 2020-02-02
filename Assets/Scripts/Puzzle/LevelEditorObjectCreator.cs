using UnityEngine;

namespace Puzzle
{
    public class LevelEditorObjectCreator : MonoBehaviour
    {
        public GameObject emitterPrefab;
        public GameObject receiverPrefab;
        public GameObject mirrorPrefab;

        public void CreateEmitter()
        {
            Instantiate(emitterPrefab);
        }
        
        public void CreateReceiver()
        {
            Instantiate(receiverPrefab);
        }
        
        public void CreateMirror()
        {
            Instantiate(mirrorPrefab);
        }
    }
}