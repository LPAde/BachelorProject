using UnityEngine;
using UnityEngine.SceneManagement;

namespace Disclaimer
{
    public class DisclaimerBehavior : MonoBehaviour
    {
        [SerializeField] private float upTime;
        [SerializeField] private int gameScene;
        
        void Update()
        {
            upTime -= Time.deltaTime;

            if (upTime < 0)
                SceneManager.LoadScene(gameScene);
        }
    }
}
