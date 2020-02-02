using System;
using UnityEngine;

namespace Puzzle
{
    public class MusicController : MonoBehaviour
    {
        public float songChangeInterval;
        public AudioSource track1Intro, track1Loop, track2, track3;
        
        private void Awake()
        {
            SwitchSongs();
        }

        private void SwitchSongs()
        {
            var timeLeft = 0f;
            if (track1Intro.isPlaying)
                timeLeft = track1Intro.clip.length - track1Intro.time;
            if (track1Loop.isPlaying)
                timeLeft = track1Loop.clip.length - track1Loop.time;
            if (track2.isPlaying)
                timeLeft = track2.clip.length - track2.time;
            if (track3.isPlaying)
                timeLeft = track3.clip.length - track3.time;
            Debug.Log("Too early to loop, waiting " + timeLeft + " seconds");
            Invoke(nameof(PlaySome), timeLeft + 2);
            
            track1Loop.loop = false;
            track2.loop = false;
            track3.loop = false;
        }

        private void PlaySome()
        {
            track1Intro.Stop();
            track1Loop.Stop();
            track2.Stop();
            track3.Stop();
            
            track1Loop.loop = true;
            track2.loop = true;
            track3.loop = true;
            
            var song = new System.Random().Next(3);
            Debug.Log("Playing song " + song);
            switch (song)
            {
                case 0:
                    track1Intro.Play();
                    track1Loop.PlayDelayed(track1Intro.clip.length);
                    break;
                case 1:
                    track2.Play();
                    break;
                case 2:
                    track3.Play();
                    break;
            }
            
            Invoke(nameof(SwitchSongs), songChangeInterval + (song == 0 ? track1Intro.clip.length : 0));
        }
    }
}