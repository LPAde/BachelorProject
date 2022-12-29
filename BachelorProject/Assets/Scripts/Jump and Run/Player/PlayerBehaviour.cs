using System.Collections;
using Jump_and_Run.UX;
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
        [SerializeField] private float maxStepTime;
        [SerializeField] private float stepTime;
        [SerializeField] private Rigidbody2D rigid;
        [SerializeField] private BoxCollider2D boxCollider;
        [SerializeField] private Vector3 currentRespawnPoint;
        private bool _mayMove = true;
        
        [Header("Dash Stuff")]
        [SerializeField] private float dashTime;
        [SerializeField] private float currentDashTime;
        [SerializeField] private bool canDash;
        [SerializeField] private Vector2 dashVector;
        
        
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
        private static readonly int YVelo = Animator.StringToHash("YVelo");
        private static readonly int Dash1 = Animator.StringToHash("Dash");
        private static readonly int Dies = Animator.StringToHash("Dies");
        private static readonly int Death = Animator.StringToHash("Death");

        #endregion

        public Rigidbody2D Rigid => rigid;
        
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

        private void OnTriggerStay2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Untagged") || rigid.velocity.y is <= -1.2f or >= 1.2f)
                return;
            
            anim.SetBool(IsGrounded, true);
            groundedTimer = resetTimer;
            canDash = false;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Untagged"))
                return;
            
            canDash = true;
            groundedTimer = resetTimer;
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Untagged") && rigid.velocity.y == 0)
            {
                anim.SetBool(IsGrounded, true);
                groundedTimer = 1;
                canDash = false;
            }
        }
        
        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Untagged"))
            {
                canDash = true;
                groundedTimer = resetTimer;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Checks the players inputs.
        /// </summary>
        private void CheckInputs()
        {
            anim.SetFloat(YVelo, rigid.velocity.y);
            
            timeTillJumpInput -= Time.deltaTime;

            if (groundedTimer <= resetTimer)
                groundedTimer -= Time.deltaTime;
            
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

            if (canDash && Input.GetKeyDown(KeyCode.LeftShift))
                StartCoroutine(Dash());
        }

        /// <summary>
        /// Moves the player.
        /// </summary>
        private void Move()
        {
            if(!_mayMove)
                return;
            
            if(currentDashTime > 0)
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

            // Makes the step sound.
            if(horizontalInput != 0 && rigid.velocity.y == 0)
            {
                stepTime -= Time.deltaTime;

                if (stepTime > 0)
                    return;

                stepTime = maxStepTime;
                AudioManager.Instance.PlaySound("Step");
            }
        }

        private void Jump()
        {
            timeTillJumpInput = 0;
            
            if (isBuggy)
            {
                // Sometimes won't work when game is buggy.
                if(Random.Range(0, 100) > jumpSuccessRate)
                    return;
            }
            
            groundedTimer = -1;
            anim.SetTrigger(Jump1);
            anim.SetBool(IsGrounded, false);
            rigid.velocity = new Vector2(rigid.velocity.x, jumpStrength);
            AudioManager.Instance.PlaySound("Jump");
        }

        private IEnumerator Dash()
        {
            AudioManager.Instance.PlaySound("Dash");
            canDash = false;
            anim.SetTrigger(Dash1);
            while (currentDashTime < dashTime && _mayMove)
            {
                currentDashTime += Time.deltaTime;
                
                rigid.velocity = !spriteRenderer.flipX ? new Vector2(dashVector.x, dashVector.y) : new Vector2(-dashVector.x, dashVector.y);
                
                yield return new WaitForEndOfFrame();
            }

            currentDashTime = 0;
        }

        private void FinishLevel()
        {
            SceneManager.LoadScene(2);
        }

        
        /// <summary>
        /// Respawns the player.
        /// </summary>
        private void Respawn()
        {
            _mayMove = false;
            rigid.velocity = Vector2.zero;
            boxCollider.enabled = false;
            AudioManager.Instance.PlayOnlySound("Death");
            anim.SetBool(Dies, true);
            anim.SetTrigger(Death);
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
            anim.SetBool(Dies, false);
            transform.position = currentRespawnPoint;
            rigid.velocity = Vector2.zero;
            boxCollider.enabled = true;
            timeTillJumpInput = 0;
            spriteRenderer.flipX = false;
        }
        
        public void AllowMove()
        {
            _mayMove = true;
            AudioManager.Instance.PlaySound("Background Music");
        }
        
        #endregion
        
    }
}