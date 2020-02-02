using System;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle
{
    public class FXRequests : MonoBehaviour
    {
        public HashSet<string> requests = new HashSet<string>();

        private void FixedUpdate()
        {
            requests.Clear();
        }
    }
}