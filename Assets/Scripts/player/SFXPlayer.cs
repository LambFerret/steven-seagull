using UnityEngine;

namespace player
{
    public class SFXPlayer : MonoBehaviour
    {
        public static SFXPlayer Instance;

        public AudioClip[] clips;
        private AudioSource _audioSource;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
        private void Start()
        {
            _audioSource = gameObject.AddComponent<AudioSource>();
        }

        public void Play(string clipName, float volume = 1.0f)
        {
            foreach (var clip in clips)
            {
                if (clip.name == clipName)
                {
                    _audioSource.PlayOneShot(clip, volume);
                    break;
                }
            }
        }
    }
}