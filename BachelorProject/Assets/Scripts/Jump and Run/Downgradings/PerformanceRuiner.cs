using System;
using Cinemachine;
using UnityEngine;
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
                    float rn = Random.Range(.1f, .9f);

                    // Only set chance of slowdowns to make them seem more random.
                    if (rn is > .3f and < .8f)
                    {
                        currentDuration = intervalDuration;
                    }
                    else
                    {
                        // Make the duration also fairly random for... fun... yeah for fun.
                        Time.timeScale = rn;
                        currentDuration = Time.timeScale * lagDuration * Random.Range(.3f, 3);
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
    }
}