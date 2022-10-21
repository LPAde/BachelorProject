using System.Collections.Generic;
using Misc;
using UnityEngine;
using Visual_Novel;

namespace Jump_and_Run.Enemy
{
    public class EnemyBehavior : MonoBehaviour
    {
        [SerializeField] private Vector3 movementVector;
        [SerializeField] private Rigidbody2D rb;
        
        [Header("Visual")]
        [SerializeField] private JumpAndRunChanger changer;
        [SerializeField] private List<GameObject> models;
        [SerializeField] private Animator anim;
        [SerializeField] private Animator badAnim;
        [SerializeField] private SpriteRenderer render;
        [SerializeField] private SpriteRenderer badRender;

        private void Awake()
        {
            Downgrade();
        }

        private void Update()
        {
            Move();
            CheckSound();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("MainCamera"))
                return;
            
            movementVector *= -1;
            render.flipX = !render.flipX;
        }

        private void Move()
        {
            rb.velocity = movementVector;
        }

        /// <summary>
        /// Checks if its sound should be played and plays it.
        /// </summary>
        private void CheckSound()
        {
            if(!render.isVisible)
                return;
            
            if(AudioManager.Instance.CheckSoundPlaying("Enemy"))
                return;

            print("play");
            AudioManager.Instance.PlaySound("Enemy");
        }

        private void Downgrade()
        {
            if(!changer.firedDepartments[(int)GameDepartments.CreatureArt])
                return;
            
            models[0].SetActive(false);
            models[1].SetActive(true);
            anim = badAnim;
            render = badRender;
        }
    }
}