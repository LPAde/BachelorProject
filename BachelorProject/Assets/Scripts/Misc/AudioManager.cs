using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Visual_Novel;

namespace Misc
{
    public class AudioManager : MonoBehaviour
    {
        #region Fields

        public static AudioManager Instance;
                
        [SerializeField] private JumpAndRunChanger changer;

        [SerializeField] private List<Sound> sfxSounds;
        [SerializeField] private List<Sound> badSfxSounds;
        
        [SerializeField] private List<Sound> musicSounds;
        [SerializeField] private List<Sound> badMusicSounds;

        #endregion

        #region Unity Methods

        private void Awake()
        {
            // Singleton.
            if (Instance != null)
            {
                Destroy(Instance);
            }
        
            Instance = this;
            DontDestroyOnLoad(gameObject);
        
            // Sound setup.
            foreach (var sound in sfxSounds)
            {
                sound.source = gameObject.AddComponent<AudioSource>();
        
                sound.source.clip = sound.clip;
                sound.source.outputAudioMixerGroup = sound.mixer;
                
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
        
                sound.source.loop = false;
            }
            
            foreach (var music in musicSounds)
            {
                music.source = gameObject.AddComponent<AudioSource>();
        
                music.source.clip = music.clip;
                music.source.outputAudioMixerGroup = music.mixer;
                
                music.source.volume = music.volume;
                music.source.pitch = music.pitch;
        
                music.source.loop = true;
            }
            
            SceneManager.sceneLoaded += CheckDownGrade;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Plays a sound of your liking.
        /// </summary>
        /// <param name="soundName"> The sound you wanna play. </param>
        /// <returns> Did it succeed? </returns>
        public bool PlaySound(string soundName)
        {
            Sound s = sfxSounds.Find(sound => sound.soundName == soundName);
            
            // Check if it is a sfx sound.
            if (s == null)
            {
                s = musicSounds.Find(sound => sound.soundName == soundName);
        
                // Check is it is a music sound.
                if (s == null)
                    return false;
        
                // Stop all songs to avoid multiple playing at once.
                foreach (var music in musicSounds)
                {
                    music.source.Stop();
                }
                
                s.source.Play();
                return true;
            }
            if(!s.source.isPlaying)
                s.source.Play();
            
            return true;
        }
        
        /// <summary>
        /// Stops all other sounds and plays one.
        /// </summary>
        /// <param name="soundName"> The sound it shall play. </param>
        /// <returns> Did it succeed? </returns>
        public bool PlayOnlySound(string soundName)
        {
            // Stop all sfx to avoid multiple playing at once.
            foreach (var sfx in sfxSounds)
            {
                sfx.source.Stop();
            }
            
            return PlaySound(soundName);
        }
        
        /// <summary>
        /// Checks if a specific sound is playing.
        /// </summary>
        /// <param name="soundName"> The sound you wanna check. </param>
        /// <returns> If it is playing. </returns>
        public bool CheckSoundPlaying(string soundName)
        {
            Sound s = sfxSounds.Find(sound => sound.soundName == soundName);
            
            // Check if it is a sfx sound.
            if (s != null) 
                return s.source.isPlaying;
            
            s = musicSounds.Find(sound => sound.soundName == soundName);
        
            // Check is it is a music sound.
            if (s == null)
                return false;
        
            // Stop all songs to avoid multiple playing at once.
            foreach (var music in musicSounds)
            {
                music.source.Stop();
            }
                
            return s.source.isPlaying;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Checks if one of the important departments have been fired.
        /// </summary>
        /// <param name="sc"> Forced cause event. </param>
        /// <param name="mode"> Forced cause event. </param>
        private void CheckDownGrade(Scene sc, LoadSceneMode mode)
        {
            StopAllSounds();
            
            if (changer.firedDepartments[(int) GameDepartments.Sfx])
            {
                for (int i = 0; i < sfxSounds.Count; i++)
                {
                    sfxSounds[i].source.clip = badSfxSounds[i].clip;
                    sfxSounds[i].source.outputAudioMixerGroup = badSfxSounds[i].mixer;
                    sfxSounds[i].source.volume = badSfxSounds[i].volume;
                    sfxSounds[i].source.pitch = badSfxSounds[i].pitch;
                }
            }
        
            if (changer.firedDepartments[(int) GameDepartments.Music])
            {
                for (int i = 0; i < musicSounds.Count; i++)
                {
                    musicSounds[i].source.clip = badMusicSounds[i].clip;
                    musicSounds[i].source.outputAudioMixerGroup = badMusicSounds[i].mixer;
                    musicSounds[i].source.volume = badMusicSounds[i].volume;
                    musicSounds[i].source.pitch = badMusicSounds[i].pitch;
                }
            }
        }
        
        /// <summary>
        /// Stops all sounds from playing.
        /// </summary>
        private void StopAllSounds()
        {
            // Stop all songs to avoid multiple playing at once.
            foreach (var music in musicSounds)
            {
                music.source.Stop();
            }
            
            // Stop all sfx to avoid multiple playing at once.
            foreach (var sfx in sfxSounds)
            {
                sfx.source.Stop();
            }
        }

        #endregion
    }
}