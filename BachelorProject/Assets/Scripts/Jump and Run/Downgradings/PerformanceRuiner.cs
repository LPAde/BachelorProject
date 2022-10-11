using System;
using UnityEngine;
using Visual_Novel;
using Random = UnityEngine.Random;

namespace Jump_and_Run.Downgradings
{
    public class PerformanceRuiner : MonoBehaviour
    {
        [SerializeField] private float intervalDuration;
        [SerializeField] private float lagDuration;
        [SerializeField] private float duration;

        private void Awake()
        {
            Activate();
        }

        private void Update()
        {
            StartLagging();
        }

        /// <summary>
        /// Makes the game lag;
        /// </summary>
        private void StartLagging()
        {
            duration -= Time.deltaTime / Time.timeScale;

            if (duration <= 0)
            {
                if (Math.Abs(Time.timeScale - 1) < .1f)
                {
                    float rn = Random.Range(.1f, .5f);

                    // Only 60% Chance of slowdowns to make them seem more random.
                    if (rn > .3f)
                    {
                        duration = intervalDuration;
                    }
                    else
                    {
                        Time.timeScale = rn;
                        duration = Time.timeScale * lagDuration;

                        print(Time.timeScale);
                    }
                }
                else
                {
                    // Adds a random time to make it seem random.
                    duration = intervalDuration + Random.Range(0, intervalDuration * .5f);
                    Time.timeScale = 1;
                }
            }
        }

        /// <summary>
        /// Activates this mischievous script.
        /// </summary>
        private void Activate()
        {
            if (JumpAndRunChanger.Instance == null)
            {
                gameObject.SetActive(false);
                return;
            }

            if (JumpAndRunChanger.Instance.FiredDepartments[(int) GameDepartments.PerformanceOptimisation])
                return;

            gameObject.SetActive(false);
        }
    }
}