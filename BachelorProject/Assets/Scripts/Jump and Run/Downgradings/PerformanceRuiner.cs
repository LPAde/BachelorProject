using System;
using System.Collections.Generic;
using UnityEngine;
using Visual_Novel;
using Random = UnityEngine.Random;

namespace Jump_and_Run.Downgradings
{
    public class PerformanceRuiner : MonoBehaviour
    {
        [SerializeField] private float maxDuration;
        [SerializeField] private float duration;
        
        private void Awake()
        {
            Downgrade();
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
                    Time.timeScale = Random.Range(.1f, .3f);
                    duration = Time.timeScale * 20;
                    
                    print(Time.timeScale);
                }
                else
                {
                    // Adds a random time to make it seem random.
                    duration = maxDuration + Random.Range(0, maxDuration);
                    Time.timeScale = 1;
                }
            }
        }
        
        private void Downgrade()
        {
            if(JumpAndRunChanger.Instance == null)
            {
                gameObject.SetActive(false);
                return;
            }
            
            if(JumpAndRunChanger.Instance.FiredDepartments[(int)GameDepartments.PerformanceOptimisation])
                return;
            
            gameObject.SetActive(false);
        }
    }
}
