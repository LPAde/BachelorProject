using System.Collections.Generic;
using UnityEngine;
using Visual_Novel;

namespace Jump_and_Run.Downgradings
{
    public class Downgrader : MonoBehaviour
    {
        [Header("Base Stuff")]
        [SerializeField] private JumpAndRunChanger changer;
        [SerializeField] private GameDepartments department;
        
        [Header("Visual Stuff")] 
        [SerializeField] private List<GameObject> models;

        private void Start()
        {
            CheckDowngrade();
        }

        private void CheckDowngrade()
        {
            if(!changer.firedDepartments[(int)department])
                return;
            
            models[0].SetActive(false);
            models[1].SetActive(true);
        }
    }
}