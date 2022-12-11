using UnityEngine;

namespace Jump_and_Run.UX
{
    public class ParallaxEffect : MonoBehaviour
    {
        [SerializeField] private float length;
        [SerializeField] private float parallaxValue;
        [SerializeField] private Vector3 startPos;
        [SerializeField] private GameObject cam;

        private void Start()
        {
            startPos = transform.position;
            length = GetComponent<SpriteRenderer>().bounds.size.x;
            cam = Camera.main.gameObject;
        }

        private void LateUpdate()
        {
            var position = cam.transform.position;
            float temp = position.x * (1- parallaxValue);
            float dist = position.x * parallaxValue;

            
            transform.position = new Vector3(startPos.x + dist, startPos.y, startPos.z);

            if (temp > startPos.x + length) startPos.x += length;
            else if (temp < startPos.x - length) startPos.x -= length;
        }
    }
}