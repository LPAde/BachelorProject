using System;
using System.Collections.Generic;
using Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Visual_Novel;

namespace Main_Menu
{
    public class MainMenuBehavior : MonoBehaviour
    {
        [SerializeField] private List<Button> allButtons;
        [SerializeField] private int newScene;
        [SerializeField] private JumpAndRunChanger changer;

        private void Awake()
        {
            changer.OnEnable();
        }

        private void Update()
        {
            // Assures that the sound finishes playing.
            if(allButtons[0].interactable)
                return;
            
            if(AudioManager.Instance.CheckSoundPlaying("UI2"))
                return;
            
            if(newScene != 0)
                SceneManager.LoadScene(newScene);
            else
                Application.Quit();
        }

        public void ChangeScene(int newSceneBtn)
        {
            AudioManager.Instance.PlaySound("UI2");
            
            foreach (var btn in allButtons)
            {
                btn.interactable = false;
            }

            newScene = newSceneBtn;
        }

        public void ExitGame()
        {
            AudioManager.Instance.PlaySound("UI2");
            
            foreach (var btn in allButtons)
            {
                btn.interactable = false;
            }
        }
    }
}