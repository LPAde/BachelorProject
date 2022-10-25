using UnityEngine;

namespace Jump_and_Run
{
    public class ParallaxEffect : MonoBehaviour
    {
        [SerializeField] private Vector3 startPos;
        [SerializeField] private float length;
        [SerializeField] private float parallaxValue;
        [SerializeField] private GameObject cam;

        private void Start()
        {
            startPos = transform.position;
            length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        private void FixedUpdate()
        {
            float temp = cam.transform.position.x * (1- parallaxValue);
            float dist = cam.transform.position.x * parallaxValue;

            transform.position = new Vector3(startPos.x + dist, startPos.y, startPos.z);

            if (temp > startPos.x + length) startPos.x += length;
            else if (temp < startPos.x - length) startPos.x -= length;
        }
    }
}