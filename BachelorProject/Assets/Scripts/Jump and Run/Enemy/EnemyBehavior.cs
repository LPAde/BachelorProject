using System.Collections.Generic;
using UnityEngine;
using Visual_Novel;

namespace Jump_and_Run.Enemy
{
    public class EnemyBehavior : MonoBehaviour
    {
        [SerializeField] private JumpAndRunChanger changer;
        [SerializeField] private List<GameObject> models;
        
        [SerializeField] private Animator anim;
        [SerializeField] private Animator badAnim;

        private void Awake()
        {
            Downgrade();
        }

        private void Downgrade()
        {
            if(!changer.firedDepartments[(int)GameDepartments.CreatureArt])
                return;
            
            models[0].SetActive(false);
            models[1].SetActive(true);
            anim = badAnim;
        }
    }
}