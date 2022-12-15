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
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if(other.gameObject.CompareTag("Player"))
            {
                other.gameObject.transform.SetParent(null, true); 
            }
        }
    }
}
