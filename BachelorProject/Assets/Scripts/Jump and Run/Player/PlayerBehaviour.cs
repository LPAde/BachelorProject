using System.Collections.Generic;
using UnityEngine;

namespace Jump_and_Run.Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        public static PlayerBehaviour Instance;
        
        [Header("Movement Stuff")]
        [SerializeField] private float speed;
        [SerializeField] private float jumpStrength;
        [SerializeField] private float horizontalMovement;
        [SerializeField] private CharacterController characterController;
        
        [Header("Visual Stuff")]
        [SerializeField] private List<GameObject> models;
        [SerializeField] private Animator anim;
        [SerializeField] private Animator badAnim;
        [SerializeField] private SpriteRenderer spriteRenderer;

        #region Unity Methods

        private void Awake()
        {
            if(Instance != null)
                Destroy(Instance.gameObject);
        
            Instance = this;
        }
        
        private void Update()
        {
            CheckInputs();
        }
        
        private void FixedUpdate()
        {
            Move();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if(other.CompareTag("Finish"))
                FinishLevel();
            
            if(other.CompareTag("Enemy"))
                Respawn();
        }

        #endregion
        

        /// <summary>
        /// Checks the players inputs.
        /// </summary>
        private void CheckInputs()
        {
            if(Input.GetKey(KeyCode.Space))
                Jump();
            
            // Movement.
            horizontalMovement = Input.GetAxis("Horizontal");
        }
        
        /// <summary>
        /// Moves the player.
        /// </summary>
        private void Move()
        {
            Vector3 movementVector = new Vector3(horizontalMovement * speed * Time.fixedDeltaTime, 0);
            movementVector += (Vector3) Physics2D.gravity * Time.fixedDeltaTime;
            characterController.Move(movementVector);

            if (horizontalMovement > 0)
            {
                spriteRenderer.flipX = false;
            }
            else if (horizontalMovement < 0)
            {
                spriteRenderer.flipX = true;
            }
        }

        private void Jump()
        {
            characterController.Move(new Vector3(0, jumpStrength));
        }

        private void FinishLevel()
        {
            
        }

        private void Respawn()
        {
            
        }

        /// <summary>
        /// Downgrades the player visually.
        /// </summary>
        public void Downgrade()
        {
            anim = badAnim;
            models[0].SetActive(false);
            models[1].SetActive(true);
        }
    }
}