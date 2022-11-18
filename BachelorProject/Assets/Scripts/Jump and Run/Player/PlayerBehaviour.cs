using Misc;
using UnityEngine;
using UnityEngine.SceneManagement;
using Visual_Novel;
using Random = UnityEngine.Random;

namespace Jump_and_Run.Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        #region Fields

        [SerializeField] private JumpAndRunChanger changer;
        [SerializeField] private GameFeelManager gameFeelManager;

        [Header("Movement Stuff")] 
        [SerializeField] private float speed;
        [SerializeField] private float horizontalInput;
        [SerializeField] private Rigidbody2D rigid;
        [SerializeField] private BoxCollider2D boxCollider;
        [SerializeField] private Vector3 currentRespawnPoint;
        private bool _mayMove = true;
        
        [Header("Jump Stuff")]
        [SerializeField] private float jumpStrength;
        [SerializeField] private float jumpCut;
        [SerializeField] private float timeTillJumpInput;
        [SerializeField] private float groundedTimer;
        [SerializeField] private float resetTimer;
        
        [Header("Visual Stuff")] 
        [SerializeField] private Animator anim;
        [SerializeField] private Animator badAnim;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private SpriteRenderer badSpriteRenderer;

        [Header("Buggy Stuff")]
        [SerializeField] private bool isBuggy;
        [SerializeField] private int jumpSuccessRate;
        private static readonly int IsRunning = Animator.StringToHash("IsRunning");
        private static readonly int Jump1 = Animator.StringToHash("Jump");
        private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");

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

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Untagged"))
            {
                groundedTimer = resetTimer;
                anim.SetBool(IsGrounded, true);
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (!other.gameObject.CompareTag("Untagged"))
            {
                groundedTimer -= Time.deltaTime;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Checks the players inputs.
        /// </summary>
        private void CheckInputs()
        {
            timeTillJumpInput -= Time.deltaTime;
            
            if(!_mayMove)
                return;
            
            if (Input.GetKeyDown(KeyCode.Space))
                timeTillJumpInput = resetTimer;

            // Movement.
            horizontalInput = Input.GetAxis("Horizontal");
            
            if(timeTillJumpInput > 0 && groundedTimer > 0)
                Jump();

            if (Input.GetButtonUp("Jump") && rigid.velocity.y > 0)
            {
                rigid.velocity = new Vector2(rigid.velocity.x, jumpStrength*jumpCut);
            }
        }

        /// <summary>
        /// Moves the player.
        /// </summary>
        private void Move()
        {
            if(!_mayMove)
                return;
            
            float horizontalMovement = horizontalInput * speed * Time.fixedDeltaTime;
            rigid.velocity = new Vector2(horizontalMovement, rigid.velocity.y);

            switch (horizontalInput)
            {
                case > 0:
                    spriteRenderer.flipX = false;
                    anim.SetBool(IsRunning, true);
                    break;
                case < 0:
                    spriteRenderer.flipX = true;
                    anim.SetBool(IsRunning, true);
                    break;
                default:
                    anim.SetBool(IsRunning, false);
                    break;
            }
        }

        private void Jump()
        { 
            timeTillJumpInput = 0;
            groundedTimer = 0;
            
            if (isBuggy)
            {
                // Sometimes won't work when game is buggy.
                if(Random.Range(0, 100) > jumpSuccessRate)
                    return;
            }
            
            anim.SetTrigger(Jump1);
            anim.SetBool(IsGrounded, false);
            rigid.velocity = new Vector2(rigid.velocity.x, jumpStrength);
            AudioManager.Instance.PlaySound("Jump");
        }

        private void FinishLevel()
        {
            SceneManager.LoadScene(2);
        }

        // Respawns the player.
        private void Respawn()
        {
            rigid.velocity = Vector2.zero;
            boxCollider.enabled = false;
            AudioManager.Instance.PlayOnlySound("Death");
            StartCoroutine(gameFeelManager.FadeToBlack());
        }

        /// <summary>
        /// Downgrades the player visually or makes them buggy.
        /// </summary>
        private void Downgrade()
        {
            if (changer.firedDepartments[(int) GameDepartments.Debugging])
                isBuggy = true;
            
            if (!changer.firedDepartments[(int) GameDepartments.CharacterArt])
                return;

            anim = badAnim;
            spriteRenderer = badSpriteRenderer;
        }

        #endregion

        #region Public Methods

        public void ResetPosition()
        { 
            _mayMove = false;
            anim.SetBool(IsRunning, false);
            transform.position = currentRespawnPoint;
            rigid.velocity = Vector2.zero;
            boxCollider.enabled = true;
            timeTillJumpInput = 0;
        }
        
        public void AllowMove()
        {
            _mayMove = true;
        }

        #endregion
        
    }
}