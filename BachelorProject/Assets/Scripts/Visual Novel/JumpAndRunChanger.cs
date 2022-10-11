using System.Collections.Generic;
using UnityEngine;

namespace Visual_Novel
{
    public class JumpAndRunChanger : MonoBehaviour
    {
        // TODO: Move Game Object to main menu scene to prevent bugs.
        public static JumpAndRunChanger Instance;
        
        [SerializeField] private List<bool> firedDepartments;

        public List<bool> FiredDepartments => firedDepartments;

        public int FiredDepartmentsCount
        {
            get;
            private set;
        }

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
            FiredDepartmentsCount++;
        }
    }
}