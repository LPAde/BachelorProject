using Jump_and_Run.Player;
using UnityEngine;

namespace Jump_and_Run.Enemy
{
    public class PlatformBehavior : EnemyBehavior
    {
        [SerializeField] private PlayerBehaviour playerBehavior;
    
        private void OnCollisionStay2D(Collision2D other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                other.gameObject.transform.SetParent(gameObject.transform, true);

                if (playerBehavior == null)
                    playerBehavior = other.gameObject.GetComponent<PlayerBehaviour>();
            
                if(playerBehavior.Rigid.velocity.y != 0)
                    playerBehavior.Rigid.isKinematic = true;
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                other.gameObject.transform.SetParent(null, true);
                //  playerBehavior.Rigid.isKinematic = false;
            }
        }
    }
}
