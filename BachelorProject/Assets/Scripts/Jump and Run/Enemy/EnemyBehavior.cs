using Misc;
using UnityEngine;
using Visual_Novel;
using Random = UnityEngine.Random;

namespace Jump_and_Run.Enemy
{
    public class EnemyBehavior : MonoBehaviour
    {
        [SerializeField] protected Vector3 movementVector;
        [SerializeField] private Vector3 standardMovementVector;
        [SerializeField] private Rigidbody2D rb;
        
        [Header("Visual")]
        [SerializeField] private SpriteRenderer render;
        [SerializeField] private Animator anim;
        [SerializeField] private float waitingTime;
        
        [Header("Buggy Stuff")]
        [SerializeField] private bool isBuggy;
        [SerializeField] private int rotateSuccessRate;
        [SerializeField] private JumpAndRunChanger changer;

        private void Start()
        {
            Downgrade();
        }

        private void FixedUpdate()
        {
            Move();
            CheckSound();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("MainCamera"))
                return;
            
            // Sometimes doesn't turn around.
            if(isBuggy)
                if(rotateSuccessRate < Random.Range(0,100))
                    return;
            
            movementVector *= -1;
            standardMovementVector.x *= -1;
            
            if(render != null)
                render.flipX = !render.flipX;
        }

        private void Move()
        {
            if (waitingTime > 0)
            {
                waitingTime -= Time.deltaTime;
                
                if (waitingTime < 0)
                    anim.enabled = true;
                
                return;
            }
            
            rb.velocity = movementVector;
        }

        /// <summary>
        /// Checks if its sound should be played and plays it.
        /// </summary>
        private void CheckSound()
        {
            if(render == null)
                return;
            
            if(!render.isVisible)
                return;
            
            if(AudioManager.Instance.CheckSoundPlaying("Enemy"))
                return;

            AudioManager.Instance.PlaySound("Enemy");
        }
        
        /// <summary>
        /// Potentially makes the enemy buggy.
        /// </summary>
        private void Downgrade()
        {
            // Tries making the enemies look more different.
            waitingTime = Random.Range(0, waitingTime);
            
            if (waitingTime == 0 && anim != null)
                anim.enabled = true;
            
            if(changer == null)
                return;
            
            if (changer.firedDepartments[(int) GameDepartments.Debugging])
                isBuggy = true;
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