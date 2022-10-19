using System.Collections;
using Jump_and_Run.Player;
using UnityEngine;

namespace Jump_and_Run
{
    public class GameFeelManager : MonoBehaviour
    {
        [SerializeField] private PlayerBehaviour player;
        
        [Header("Fade to black stuff")]
        [SerializeField] private SpriteRenderer blackColor;

        // Fades to black and back to the screen.
        public IEnumerator FadeToBlack()
        {
            while (blackColor.color.a < 1)
            {
                var color = blackColor.color;
                color = new Color(color.r, color.g, color.b,
                    color.a + Time.deltaTime*2);
                blackColor.color = color;
                yield return new WaitForEndOfFrame();
            }

            player.ResetPosition();
            yield return new WaitForSeconds(1);
            
            while (blackColor.color.a > 0)
            {
                var color = blackColor.color;
                color = new Color(color.r, color.g, color.b,
                    color.a - Time.deltaTime);
                blackColor.color = color;
                yield return new WaitForEndOfFrame();
            }
            
            player.AllowMove();
        }
    }
}
