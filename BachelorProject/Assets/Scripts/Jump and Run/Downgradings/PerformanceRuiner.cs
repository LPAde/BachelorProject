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
        [SerializeField] private float currentDuration;

        private void Awake()
        {
            Activate();
            currentDuration = intervalDuration;
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
            currentDuration -= Time.deltaTime / Time.timeScale;

            if (currentDuration <= 0)
            {
                if (Math.Abs(Time.timeScale - 1) < .1f)
                {
                    float rn = Random.Range(.1f, .5f);

                    // Only 60% Chance of slowdowns to make them seem more random.
                    if (rn > .3f)
                    {
                        currentDuration = intervalDuration;
                    }
                    else
                    {
                        Time.timeScale = rn;
                        // TODO: Slow down animations maybe
                        currentDuration = Time.timeScale * lagDuration;
                    }
                }
                else
                {
                    // Adds a random time to make it seem random.
                    currentDuration = intervalDuration + Random.Range(0, intervalDuration * .5f);
                    Time.timeScale = 1;
                }
            }
        }

        /// <summary>
        /// Activates this mischievous script.
        /// </summary>
        private void Activate()
        {
          if (JumpAndRunChanger.Instance.FiredDepartments[(int) GameDepartments.PerformanceOptimisation])
                return;

          gameObject.SetActive(false);
        }
    }
}