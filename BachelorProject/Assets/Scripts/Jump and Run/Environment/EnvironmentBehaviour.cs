using System.Collections.Generic;
using UnityEngine;

namespace Jump_and_Run.Environment
{
    public class EnvironmentBehaviour : MonoBehaviour
    {
        [SerializeField] private List<GameObject> models;

        public void Downgrade()
        {
            models[0].SetActive(false);
            models[1].SetActive(true);
        }
    } 
}