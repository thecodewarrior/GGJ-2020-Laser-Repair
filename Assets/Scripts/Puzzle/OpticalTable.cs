using System;
using UnityEngine;

namespace Puzzle
{
    [ExecuteAlways]
    public class OpticalTable : MonoBehaviour
    {
        public GameObject north, south, east, west;
        
        public int width;
        public int height;

        public void IncreaseWidth()
        {
            width++;
        }
        
        public void DecreaseWidth()
        {
            width--;
        }
        
        public void IncreaseHeight()
        {
            height++;
        }
        
        public void DecreaseHeight()
        {
            height--;
        }

        public void Update()
        {
            // ReSharper disable Unity.InefficientPropertyAccess
            transform.localScale = new Vector3(width / 5f, 1f, height / 5f);
            var nTransform = north.transform;
            var sTransform = south.transform;
            var eTransform = east.transform;
            var wTransform = west.transform;
            nTransform.localPosition = new Vector3(nTransform.localPosition.x, nTransform.localPosition.y, height + nTransform.localScale.z/2);
            nTransform.localScale = new Vector3(width * 2, nTransform.localScale.y, nTransform.localScale.z);
            sTransform.localPosition = new Vector3(sTransform.localPosition.x, sTransform.localPosition.y, -height - sTransform.localScale.z/2);
            sTransform.localScale = new Vector3(width * 2, sTransform.localScale.y, sTransform.localScale.z);
            eTransform.localPosition = new Vector3(width + eTransform.localScale.x/2, eTransform.localPosition.y, eTransform.localPosition.z);
            eTransform.localScale = new Vector3(eTransform.localScale.x, eTransform.localScale.y, height * 2);
            wTransform.localPosition = new Vector3(-width - wTransform.localScale.x/2, wTransform.localPosition.y, wTransform.localPosition.z);
            wTransform.localScale = new Vector3(wTransform.localScale.x, wTransform.localScale.y, height * 2);
        }
    }
}