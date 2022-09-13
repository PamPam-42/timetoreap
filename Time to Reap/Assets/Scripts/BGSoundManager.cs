using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGSoundManager : MonoBehaviour
{
        [SerializeField] public AudioSource bgSource;
        [SerializeField] public AudioSource clockTickSource;

		private static bool tickingOn = false;
		
        public static BGSoundManager instance = null;

		void Awake ()
		{
			if (instance == null)
				instance = this;
			else if (instance != this)
				Destroy (gameObject);
			
			DontDestroyOnLoad (gameObject);
        }
		
		public void toggleClockSound() {
				if (tickingOn) {
					clockTickSource.Pause();
					tickingOn = false;
				}
				else {
					clockTickSource.Play();
					tickingOn = true;
				}
		}
		
		public void stopClockSound() {
			clockTickSource.Pause();
			tickingOn = false;
		}
		
		public void startPlaying() {
				bgSource.Play();
		}
		
		public void stopPlaying() {
			clockTickSource.Stop();
			tickingOn = false;
			bgSource.Stop();
		}
		
		public void pauseMusic() {
			clockTickSource.Stop();
			tickingOn = false;
			bgSource.Pause();
		}
}
