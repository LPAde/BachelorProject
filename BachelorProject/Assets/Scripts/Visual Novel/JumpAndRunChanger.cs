using System.Collections.Generic;
using UnityEngine;

namespace Visual_Novel
{
    [CreateAssetMenu(fileName = "JumpAndRunChanger", menuName = "JumpAndRunChanger", order = 0)]
    public class JumpAndRunChanger : ScriptableObject
    {
        public List<bool> firedDepartments;

        public int FiredDepartmentsCount
        {
            get;
            private set;
        }

        private void OnEnable()
        {
            FiredDepartmentsCount = 0;
            
            for (int i = 0; i < firedDepartments.Count; i++)
            {
                firedDepartments[i] = false;
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