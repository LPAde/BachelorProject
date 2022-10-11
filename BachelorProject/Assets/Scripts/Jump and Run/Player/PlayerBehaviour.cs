using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Visual_Novel;

namespace Jump_and_Run.Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        public static PlayerBehaviour Instance;
        
        [Header("Movement Stuff")]
        [SerializeField] private float speed;
        [SerializeField] private float jumpStrength;
        [SerializeField] private float horizontalInput;
        [SerializeField] private bool isGrounded;
        [SerializeField] private Rigidbody2D rigid;
        [SerializeField] private Transform currentRespawnPoint;

        [Header("Visual Stuff")]
        [SerializeField] private List<GameObject> models;
        [SerializeField] private Animator anim;
        [SerializeField] private Animator badAnim;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private SpriteRenderer badSpriteRenderer;

        #region Unity Methods

        private void Awake()
        {
            if(Instance != null)
                Destroy(Instance.gameObject);
        
            Instance = this;
            Downgrade();
        }

        private void Start()
        {
            currentRespawnPoint = transform;
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
            
            if(other.CompareTag("Respawn"))
                Respawn();

            if (other.CompareTag("Respawn"))
                currentRespawnPoint = other.transform;
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            isGrounded = other.gameObject.CompareTag("Untagged");
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            isGrounded = !other.gameObject.CompareTag("Untagged");
        }

        #endregion
        

        /// <summary>
        /// Checks the players inputs.
        /// </summary>
        private void CheckInputs()
        {
            if(Input.GetKeyDown(KeyCode.Space))
                Jump();
            
            // Movement.
            horizontalInput = Input.GetAxis("Horizontal");
        }
        
        /// <summary>
        /// Moves the player.
        /// </summary>
        private void Move()
        {
            float horizontalMovement = horizontalInput * speed * Time.fixedDeltaTime;
            rigid.velocity = new Vector2(horizontalMovement, rigid.velocity.y);

            if (horizontalInput > 0)
            {
                spriteRenderer.flipX = true;
            }
            else if (horizontalInput < 0)
            {
                spriteRenderer.flipX = false;
            }
        }

        private void Jump()
        {
            if(isGrounded) 
                rigid.AddForce(new Vector2(0, jumpStrength));
        }

        private void FinishLevel()
        {
            SceneManager.LoadScene(2);
        }

        // Respawns the player.
        private void Respawn()
        {
            transform.position = currentRespawnPoint.position;
        }

        /// <summary>
        /// Downgrades the player visually.
        /// </summary>
        private void Downgrade()
        {
            if(!JumpAndRunChanger.Instance.FiredDepartments[(int)GameDepartments.CharacterArt])
                return;
            
            anim = badAnim;
            spriteRenderer = badSpriteRenderer;
            models[0].SetActive(false);
            models[1].SetActive(true);
        }
    }
}