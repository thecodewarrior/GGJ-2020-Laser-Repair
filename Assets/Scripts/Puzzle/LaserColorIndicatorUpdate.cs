using System;
using UnityEngine;

namespace Puzzle
{
    [ExecuteAlways]
    public class LaserColorIndicatorUpdate : MonoBehaviour
    {
        public int materialIndex;
        
        private void Update()
        {
            var emitter = GetComponent<ColoredObject>();
            var meshRenderer = GetComponent<MeshRenderer>();
            var mats = meshRenderer.sharedMaterials;
                
            if (emitter.Color)
            {
                mats[materialIndex] = emitter.Color.material;
            }
            else
            {
                mats[materialIndex] = null;
            }

            meshRenderer.sharedMaterials = mats;
        }
    }
}