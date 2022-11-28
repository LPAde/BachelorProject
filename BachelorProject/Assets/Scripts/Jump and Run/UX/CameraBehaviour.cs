using System;
using UnityEngine;

namespace Jump_and_Run.UX
{
    public class CameraBehaviour : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject bottomMin;
        [SerializeField] private Rigidbody2D playerRigid;
        [SerializeField] private Vector2 threshold;
        [SerializeField] private Vector2 followOffset;
        [SerializeField] private Vector2 startVector;
    
        void Start()
        {
            threshold = CalculateThreshold();
            startVector = transform.position;
        }
    
        void Update()
        {
            Move();
        }

        /// <summary>
        /// Calculates the moving threshold based on the camera.
        /// </summary>
        /// <returns> The movement threshold. </returns>
        private Vector2 CalculateThreshold()
        {
            var main = Camera.main;

            if (main is null)
                return Vector2.zero;
        
            Rect aspect = main.pixelRect;
            var orthographicSize = main.orthographicSize;
            Vector2 thresholdVector = new Vector2(orthographicSize * aspect.width / aspect.height,
                orthographicSize);

            thresholdVector.x -= followOffset.x;
            thresholdVector.y -= followOffset.y;

            return thresholdVector;
        }

        /// <summary>
        /// Moves the camera if needed.
        /// </summary>
        private void Move()
        {
            Vector2 follow = player.transform.position + Vector3.down *.4f ;
        
            var position = transform.position;
            float xDifference = Vector2.Distance(Vector2.right * position.x, Vector2.right * follow.x);
            float yDifference = Vector2.Distance(Vector2.up * position.y, Vector2.up * follow.y);

            // Checks if the camera needs to be moved.
            Vector3 newPosition = position;
        
            if (MathF.Abs(xDifference) >= threshold.x)
                newPosition.x = follow.x;

            if (MathF.Abs(yDifference) >= threshold.y)
                newPosition.y = follow.y;
            
            // Min value comparison. 
            if (newPosition.x < startVector.x)
                newPosition.x = startVector.x;
            
            if (newPosition.y < startVector.y)
                newPosition.y = startVector.y;

            // Ensuring a min height.
            if (player.transform.position.y < bottomMin.transform.position.y)
                newPosition.y = bottomMin.transform.position.y;
            
            var velocity = playerRigid.velocity;
        
            // Changes the speed when player is faster then the camera.
            float moveSpeed = velocity.magnitude > speed ? velocity.magnitude : speed;
        
            // Actually moves the camera.
            transform.position = Vector3.MoveTowards(transform.position, newPosition, moveSpeed * Time.deltaTime);
        }

        /// <summary>
        /// Sets the camera position manually.
        /// </summary>
        /// <param name="newPosition"> The position you want the camera to be moved to. </param>
        public void SetCameraPosition(Vector3 newPosition)
        {
            transform.position = newPosition;
        }
    }
}