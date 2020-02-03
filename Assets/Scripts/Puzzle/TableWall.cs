using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

namespace Puzzle
{
    public class TableWall : MonoBehaviour
    {
        public GameObject[] sectionPrefabs;
        
        private int _length = 0;
        public int Length
        {
            get => _length;
            set
            {
                if (_length != value)
                {
                    _length = math.max(0, value);
                    UpdateLength();
                }
            }
        }

        private List<GameObject> segments = new List<GameObject>();

        private void UpdateLength()
        {
            if (!Application.isPlaying) return;
            while (segments.Count > Length)
            {
                Destroy(segments[segments.Count - 1]);
                segments.RemoveAt(segments.Count - 1);
            }

            var random = new System.Random();
            while (segments.Count < Length)
            {
                segments.Add(Instantiate(sectionPrefabs[random.Next(sectionPrefabs.Length)], this.transform, true));
            }

            
            var leftPoint = new float3(math.min(0, 1 - Length), 0, 0);
            var offset = new float3(2, 0, 0);
            for (int i = 0; i < Length; i++)
            {
                var rot = segments[i].transform.localRotation;
                rot.eulerAngles = new Vector3(90, 0, 90);
                segments[i].transform.localRotation = rot;
                segments[i].transform.localPosition = leftPoint + offset * i;
            }
        }
    }
}