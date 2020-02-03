using System;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle
{
    [ExecuteAlways]
    public class OpticalTable : MonoBehaviour
    {
        public TableWall north, south, east, west;
        public GameObject geometry;
        
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
            geometry.transform.localScale = new Vector3(width / 5f, 1f, height / 5f);

            var pos = north.transform.localPosition;
            north.Length = width;
            pos.z = height;
            north.transform.localPosition = pos;
            
            pos = south.transform.localPosition;
            south.Length = width;
            pos.z = -height;
            south.transform.localPosition = pos;
            
            pos = east.transform.localPosition;
            east.Length = height;
            pos.x = width;
            east.transform.localPosition = pos;
            
            pos = west.transform.localPosition;
            west.Length = height;
            pos.x = -width;
            west.transform.localPosition = pos;
        }
    }
}