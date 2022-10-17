using System.Collections.Generic;
using UnityEngine;
using Visual_Novel;

namespace Jump_and_Run
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private JumpAndRunChanger changer;
        [SerializeField] private List<GameObject> levels;
        [SerializeField] private List<GameObject> downgradedLevels;

        private void Awake()
        {
            LevelInitialize(changer.FiredDepartmentsCount);
        }

        /// <summary>
        /// Initializes the correct Level;
        /// </summary>
        /// <param name="finishedLevel"> How many levels have been finished so far. </param>
        private void LevelInitialize(int finishedLevel)
        {
            for (int i = 0; i < levels.Count; i++)
            {
                // Check if we are at the finished level indicator.
                if(i == finishedLevel)
                {
                    Destroy(
                        changer.firedDepartments[(int) GameDepartments.GameDesign] ? levels[i] : downgradedLevels[i]);
                }
                else
                {
                    Destroy(levels[i]);
                    Destroy(downgradedLevels[i]);
                }
            }
        }
    }
}