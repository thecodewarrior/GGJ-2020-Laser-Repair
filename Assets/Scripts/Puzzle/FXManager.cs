using System;
using System.Collections.Generic;
using UnityEngine;

namespace Puzzle
{
    public class FXManager : MonoBehaviour
    {
        public FXEntry[] effects;
        public FXDelay[] delays;

        private void Awake()
        {
            foreach (var fxDelay in delays)
            {
                if(fxDelay.audio)
                    fxDelay.audio.PlayDelayed(fxDelay.delay);
            }
        }

        private void FixedUpdate()
        {
            var allSet = new HashSet<string>();
            foreach (var fxRequests in FindObjectsOfType<FXRequests>())
            {
                foreach (var request in fxRequests.requests)
                {
                    allSet.Add(request);
                }
            }
            
            foreach (var fxEntry in effects)
            {
                if(!fxEntry.audio) continue;
                if(fxEntry.audio.loop)
                    fxEntry.audio.mute = !allSet.Contains(fxEntry.name);
                // if ()
                // {
                //     if (!fxEntry.audio.isPlaying)
                //         fxEntry.audio.Play();
                // }
                // else
                // {
                //     if(fxEntry.audio.isPlaying)
                //         fxEntry.audio.Stop();
                // }
            }
        }

        public void Play(string effect)
        {
            foreach (var fxEntry in effects)
            {
                if (fxEntry.name == effect)
                {
                    if(!fxEntry.audio.isPlaying)
                        fxEntry.audio.Play();
                    return;
                }
            }
        }
        
        public static void PlaySound(string effect)
        {
            FindObjectOfType<FXManager>().Play(effect);
        }
    }

    [Serializable]
    public class FXEntry
    {
        public string name;
        public AudioSource audio;
    }
    
    [Serializable]
    public class FXDelay
    {
        public AudioSource audio;
        public float delay;
    }
}