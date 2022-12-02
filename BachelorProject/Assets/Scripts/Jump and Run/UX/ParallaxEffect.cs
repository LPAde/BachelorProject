using UnityEngine;

namespace Jump_and_Run.UX
{
    public class ParallaxEffect : MonoBehaviour
    {
        [SerializeField] private float length;
        [SerializeField] private float parallaxValue;
        [SerializeField] private float minY;
        [SerializeField] private Vector3 startPos;
        [SerializeField] private GameObject cam;

        private void Start()
        {
            startPos = transform.position;
            length = GetComponent<SpriteRenderer>().bounds.size.x;
            cam = Camera.main.gameObject;
            minY = cam.transform.position.y;
        }

        private void LateUpdate()
        {
            var position = cam.transform.position;
            float temp = position.x * (1- parallaxValue);
            float dist = position.x * parallaxValue;
            
            float tempY = startPos.y;
            
            if (cam.transform.position.y < minY)
            {
                startPos.y -= cam.transform.position.y;
            }

            transform.position = new Vector3(startPos.x + dist, startPos.y, startPos.z);

            if (temp > startPos.x + length) startPos.x += length;
            else if (temp < startPos.x - length) startPos.x -= length;

            startPos.y = tempY;
        }
    }
}