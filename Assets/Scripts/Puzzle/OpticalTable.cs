using System;
using UnityEngine;

namespace Puzzle
{
    [ExecuteAlways]
    public class OpticalTable : MonoBehaviour
    {
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
            transform.localScale = new Vector3(width / 5f, 1f, height / 5f);
        }
    }
}