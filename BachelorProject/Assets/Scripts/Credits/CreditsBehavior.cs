using UnityEngine;
using UnityEngine.SceneManagement;

namespace Credits
{
    public class CreditsBehavior : MonoBehaviour
    {
        [SerializeField] private float scrollSpeed;
        [SerializeField] private Transform finalHeight;

        private void Start()
        {
            // Prevent the resolution hurting the credit duration.
            scrollSpeed = (finalHeight.position.y - transform.position.y) * .025f;
        }

        void Update()
        {
            MoveCredits();
        }

        private void MoveCredits()
        {
            transform.position += new Vector3(0,Time.deltaTime * scrollSpeed,0);

            if (!(transform.position.y > finalHeight.position.y) && !Input.GetKeyDown(KeyCode.Escape)) 
                return;
            
            SceneManager.LoadScene(0);
        }
    }
}