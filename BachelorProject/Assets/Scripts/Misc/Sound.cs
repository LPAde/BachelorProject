using UnityEngine;

namespace Misc
{
    [CreateAssetMenu(fileName = "Sound", menuName = "Sound", order = 0)]
    public class Sound : ScriptableObject
    {
        public string soundName;
        public AudioClip clip;
        
        [Range(0f,1f)]
        public float volume;
        [Range(.1f, 3f)]
        public float pitch;

        public AudioSource source;
    }
}