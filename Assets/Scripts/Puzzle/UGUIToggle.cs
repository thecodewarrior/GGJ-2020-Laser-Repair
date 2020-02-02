using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Puzzle
{
    public class UGUIToggle : MonoBehaviour
    {
        private Graphic[] toggleTargets;
        public bool startState;

        private void Start()
        {
            toggleTargets = GetComponents<Graphic>().Where(it => it.enabled).ToArray();
            SetState(startState);
        }

        private bool IsEnabled()
        {
            return toggleTargets.Any(it => it.enabled);
        }
        
        public void Toggle()
        {
            if (IsEnabled())
                Disable();
            else
                Enable();
        }

        public void Disable()
        {
            SetState(false);
        }
        
        public void Enable()
        {
            SetState(true);
        }

        public void SetState(bool state)
        {
            foreach (var target in toggleTargets)
            {
                target.enabled = state;
            }
        }
    }
}