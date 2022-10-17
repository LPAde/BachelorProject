using System.Collections.Generic;
using Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using Visual_Novel;

namespace Jump_and_Run.Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        #region Fields

        [SerializeField] private JumpAndRunChanger changer;

        [Header("Movement Stuff")] 
        [SerializeField] private float speed;
        [SerializeField] private float jumpStrength;
        [SerializeField] private float horizontalInput;
        [SerializeField] private bool isGrounded;
        [SerializeField] private Rigidbody2D rigid;
        [SerializeField] private Vector3 currentRespawnPoint;
        
        [Header("Visual Stuff")] 
        [SerializeField] private List<GameObject> models;
        [SerializeField] private Animator anim;
        [SerializeField] private Animator badAnim;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private SpriteRenderer badSpriteRenderer;

        [Header("Buggy Stuff")]
        [SerializeField] private bool isBuggy;
        [SerializeField] private int jumpSuccessRate;

        #endregion
        
        #region Unity Methods

        private void Awake()
        {
            Downgrade();
        }

        private void Start()
        {
            currentRespawnPoint = transform.position;
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
            if (other.CompareTag("Finish"))
                FinishLevel();

            if (other.CompareTag("Respawn"))
                Respawn();

            if (other.CompareTag("GameController"))
                currentRespawnPoint = other.transform.position;
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

        #region Private Methods

        /// <summary>
        /// Checks the players inputs.
        /// </summary>
        private void CheckInputs()
        {
            if (Input.GetKeyDown(KeyCode.Space))
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
            if (isGrounded)
            {
                if (isBuggy)
                {
                    // Sometimes won't work when game is buggy.
                    if(Random.Range(0, 100) > jumpSuccessRate)
                        return;
                }
                
                rigid.AddForce(new Vector2(0, jumpStrength));
                AudioManager.Instance.PlaySound("Jump");
            }
        }

        private void FinishLevel()
        {
            SceneManager.LoadScene(2);
        }

        // Respawns the player.
        private void Respawn()
        {
            transform.position = currentRespawnPoint;
        }

        /// <summary>
        /// Downgrades the player visually or makes them buggy.
        /// </summary>
        private void Downgrade()
        {
            if (changer.firedDepartments[(int) GameDepartments.Debugging])
            {
                isBuggy = true;
            }
            
            if (!changer.firedDepartments[(int) GameDepartments.CharacterArt])
                return;

            anim = badAnim;
            spriteRenderer = badSpriteRenderer;
            models[0].SetActive(false);
            models[1].SetActive(true);
        }

        #endregion
    }
}