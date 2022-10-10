using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Visual_Novel
{
    public class VisualNovelManager : MonoBehaviour
    {
        public static VisualNovelManager Instance;

        [SerializeField] private DialogManager dialogManager;
        [SerializeField] private List<bool> firedDepartments;
        [SerializeField] private List<Button> fireButtons;

        private void Awake()
        {
            if(Instance != null)
                Destroy(Instance.gameObject);

            Instance = this;
        }

        private void Start()
        {
            Initialize(JumpAndRunChanger.Instance.FiredDepartments);
        }

        /// <summary>
        /// Initializes the visual novel.
        /// </summary>
        /// <param name="currentState"></param>
        private void Initialize(List<bool> currentState)
        {
            firedDepartments = currentState;

            byte firedAmount = 0;
            
            for (int i = 0; i < firedDepartments.Count; i++)
            {
                if (!firedDepartments[i])
                    continue;
                
                fireButtons[i].interactable = false;
                firedAmount++;
            }
        }

        /// <summary>
        /// Fires a department of your liking.
        /// </summary>
        /// <param name="department"> The department you want to fire. </param>
        public void FireDepartment(GameDepartments department)
        {
            firedDepartments[(int) department] = true;
            JumpAndRunChanger.Instance.FiredDepartments[(int) department] = true;
        }

        /// <summary>
        /// Ends the visual novel part and goes into the jump and run Scene.
        /// </summary>
        public void StartJumpAndRun()
        {
            SceneManager.LoadScene(1);
        }
    }

    public enum GameDepartments
    {
        CharacterArt = 0,
        CreatureArt = 1,
        EnvironmentArt = 2,
        GameDesign = 3,
        Music = 4,
        Sfx = 5,
        PerformanceOptimisation = 6,
        Debugging = 7
    }
}