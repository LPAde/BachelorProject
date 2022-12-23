using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Visual_Novel.Dialog;

namespace Visual_Novel
{
    public class VisualNovelManager : MonoBehaviour
    {
        public static VisualNovelManager Instance;
        [SerializeField] private JumpAndRunChanger changer;
 
        [SerializeField] private DialogManager dialogManager;
        [SerializeField] private List<bool> firedDepartments;
        [SerializeField] private List<Button> fireButtons;
        [SerializeField] private GameObject dialog;
        [SerializeField] private GameObject buttons;

        [Header("Text Feel Stuff")]
        [SerializeField] private List<GameObject> backGround;
        [SerializeField] private Image phone;
        [SerializeField] private Animator phoneAnim;
        
        [Header("Fade to black stuff")]
        [SerializeField] private Image blackColor;
        [SerializeField] private TextMeshProUGUI laterText;
        [SerializeField] private float delay;
        
        private bool _hasFired;
        private static readonly int MovePhoneAnim = Animator.StringToHash("MovePhone");

        private void Awake()
        {
            if(Instance != null)
                Destroy(Instance.gameObject);

            Instance = this;
        }

        /// <summary>
        /// Fires a department of your liking.
        /// </summary>
        /// <param name="department"> The department you want to fire. </param>
        public void FireDepartment(int department)
        {
            firedDepartments[department] = true;
            changer.FireDepartment((GameDepartments)department);
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

            backGround[0].SetActive(false);
            backGround[1].SetActive(true);
            backGround[2].SetActive(false);
            dialogManager.UpdateDialog(firedAmount);
        }

        /// <summary>
        /// Ends the visual novel part and goes into the jump and run Scene.
        /// </summary>
        public void EndDialog()
        {
            phone.enabled = false;
            backGround[0].SetActive(false);
            backGround[1].SetActive(false);
            backGround[2].SetActive(true);
            
            if (_hasFired)
                StartCoroutine(FadeToBlack());
            else
            {
                buttons.SetActive(true);
                dialog.SetActive(false);
                _hasFired = true;
            }
        }

        public void MovePhone()
        {
            phoneAnim.SetTrigger(MovePhoneAnim);
        }

        public void PlayRing()
        {
            
        }

        public void StartDialog()
        {
            backGround[0].SetActive(false);
            backGround[0].GetComponent<Animator>().enabled = false;
            backGround[1].SetActive(true);
            phone.enabled = false;
            Initialize(changer.firedDepartments);
        }

        /// <summary>
        /// Initializes the visual novel.
        /// </summary>
        /// <param name="currentState"></param>
        private void Initialize(List<bool> currentState)
        {
            firedDepartments = currentState;

            for (int i = 0; i < firedDepartments.Count; i++)
            {
                if (!firedDepartments[i])
                    continue;
                
                fireButtons[i].interactable = false;
            }
            
            dialogManager.UpdateDialog(changer.FiredDepartmentsCount);
        }
        
        private IEnumerator FadeToBlack()
        {
            while (blackColor.color.a < 1)
            {
                var color = blackColor.color;
                var textColor = laterText.color;
                
                color = new Color(color.r, color.g, color.b,
                    color.a + Time.deltaTime*2);
                textColor = new Color(textColor.r, textColor.g, textColor.b,
                    textColor.a + Time.deltaTime*2);
                
                
                blackColor.color = color;
                laterText.color = textColor;
                yield return new WaitForEndOfFrame();
            }
            
            yield return new WaitForSeconds(delay);
            
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