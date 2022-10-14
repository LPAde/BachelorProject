using System.Collections.Generic;
using UnityEngine;
using Visual_Novel;

namespace Jump_and_Run.Environment
{
    public class EnvironmentBehaviour : MonoBehaviour
    {
        [SerializeField] private JumpAndRunChanger changer;
        [SerializeField] private List<GameObject> models;

        private void Awake()
        {
            Downgrade();
        }

        private void Downgrade()
        {
            Destroy(!changer.firedDepartments[(int) GameDepartments.EnvironmentArt] ? models[1] : models[0]);
        }
    } 
}