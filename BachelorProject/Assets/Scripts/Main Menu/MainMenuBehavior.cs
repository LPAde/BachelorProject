using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Main_Menu
{
    public class MainMenuBehavior : MonoBehaviour
    {
        [SerializeField] private List<Button> allButtons;
        [SerializeField] private AudioMixer mixer;

        public void ChangeScene(int newScene)
        {
            SceneManager.LoadScene(newScene);
        }

        public void ChangeMixerVolume(Slider slider)
        {
            mixer.SetFloat(slider.name, slider.value);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}