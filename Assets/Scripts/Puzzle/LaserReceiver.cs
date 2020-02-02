using System;
using UnityEngine;

namespace Puzzle
{
    public class LaserReceiver : LightComponent, ColoredObject
    {

        public LaserColor color;

        public LaserColor Color
        {
            get => color;
            set => color = value;
        }

        private bool wasOn;
        private bool turnedOff;

        private void FixedUpdate()
        {
            if (turnedOff && wasOn)
            {
                FXManager.PlaySound("target_power_down");
                wasOn = false;
            }
            turnedOff = true;
            var rot = transform.localRotation;
            rot.eulerAngles = new Vector3(0, 0, 0);
            transform.localRotation = rot;
        }

        public override void Propagate(LaserSegment inputSegment, Collider collider)
        {
            if (inputSegment.Color == color)
            {
                var rot = transform.localRotation;
                rot.eulerAngles = new Vector3(90, 0, 0);
                transform.localRotation = rot;
                turnedOff = false;
                if (wasOn == false)
                {
                    FXManager.PlaySound("target_power_up");
                }
                wasOn = true;
            }
            else
            {
                
            }
        }
    }
}