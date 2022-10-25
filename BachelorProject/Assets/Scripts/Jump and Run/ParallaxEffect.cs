using System;
using UnityEngine;

namespace Jump_and_Run
{
    public class ParallaxEffect : MonoBehaviour
    {
        [SerializeField] private float startPos;
        [SerializeField] private float length;
        [SerializeField] private float parallaxValue;
        [SerializeField] private GameObject cam;

        private void Start()
        {
            startPos = transform.position.x;
            length = GetComponent<SpriteRenderer>().bounds.size.x;
        }

        private void FixedUpdate()
        {
            float temp = cam.transform.position.x * (1- parallaxValue);
            float dist = cam.transform.position.x * parallaxValue;

            transform.position = new Vector3(startPos + dist, transform.position.y, transform.position.z);

            if (temp > startPos + length) startPos += length;
            else if (temp < startPos - length) startPos -= length;
        }
    }
}
