using Misc;
using UnityEngine;
using Visual_Novel;

namespace Jump_and_Run.Enemy
{
    public class EnemyBehavior : MonoBehaviour
    {
        [SerializeField] private Vector3 movementVector;
        [SerializeField] private Vector3 standardMovementVector;
        [SerializeField] private Rigidbody2D rb;
        
        [Header("Visual")]
        [SerializeField] private SpriteRenderer render;

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
            standardMovementVector.x *= -1;
            
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

            AudioManager.Instance.PlaySound("Enemy");
        }

        /// <summary>
        /// Animation event for stopping and walking again.
        /// </summary>
        public void ChangeMovementVector()
        {
            movementVector = movementVector != standardMovementVector ? standardMovementVector : Vector3.zero;
        }
    }
}