using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Visual_Novel.Dialog;

namespace Visual_Novel
{
    public class VisualNovelManager : MonoBehaviour
    {
        public static VisualNovelManager Instance;

        [SerializeField] private DialogManager dialogManager;
        [SerializeField] private List<bool> firedDepartments;
        [SerializeField] private List<Button> fireButtons;
        [SerializeField] private GameObject dialog;
        [SerializeField] private GameObject buttons;
        
        private bool _hasFired;
        
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
            
            dialogManager.UpdateDialog(firedAmount);
        }

        /// <summary>
        /// Fires a department of your liking.
        /// </summary>
        /// <param name="department"> The department you want to fire. </param>
        public void FireDepartment(int department)
        {
            firedDepartments[department] = true;
            JumpAndRunChanger.Instance.FireDepartment((GameDepartments)department);
            buttons.SetActive(false);
            dialog.SetActive(true);
            
            // Starts the correct dialog.
            byte firedAmount = 3;
            
            for (int i = 0; i < fireButtons.Count; i++)
            {
                if (!firedDepartments[i])
                    continue;
                
                fireButtons[i].interactable = false;
                firedAmount++;
            }
            
            dialogManager.UpdateDialog(firedAmount);
        }

        /// <summary>
        /// Ends the visual novel part and goes into the jump and run Scene.
        /// </summary>
        public void EndDialog()
        {
            if(_hasFired)
                SceneManager.LoadScene(1);
            else
            {
                buttons.SetActive(true);
                dialog.SetActive(false);
                _hasFired = true;
            }
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