using System;
using UnityEngine;

namespace Puzzle
{
    [ExecuteAlways]
    public class LaserEmitterIndicatorUpdate : MonoBehaviour
    {
        public int materialIndex;
        
        private void Update()
        {
            var emitter = GetComponent<LaserEmitter>();
            var meshRenderer = GetComponent<MeshRenderer>();
            var mats = meshRenderer.sharedMaterials;
                
            if (emitter.color)
            {
                mats[materialIndex] = emitter.color.material;
            }
            else
            {
                mats[materialIndex] = null;
            }

            meshRenderer.sharedMaterials = mats;
        }
    }
}