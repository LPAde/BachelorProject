using Jump_and_Run.Player;
using UnityEngine;

namespace Jump_and_Run.Enemy
{
    public class PlatformBehavior : EnemyBehavior
    {
        [SerializeField] private PlayerBehaviour playerBehavior;
    }
}
