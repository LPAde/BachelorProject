using System;
using System.Collections.Generic;
using UnityEngine;

namespace Visual_Novel
{
    public class JumpAndRunChanger : MonoBehaviour
    {
        public static JumpAndRunChanger Instance;
        [SerializeField] private List<bool> firedDepartments;

        public List<bool> FiredDepartments => firedDepartments;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        /// <summary>
        /// Fires a department of your liking.
        /// </summary>
        /// <param name="department"> The game department you want to fire. </param>
        public void FireDepartment(GameDepartments department)
        {
            firedDepartments[(int)department] = true;
        }
    }
}