using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using Visual_Novel;

namespace Credits
{
    public class CreditsBehavior : MonoBehaviour
    {
        [SerializeField] private float scrollSpeed;
        [SerializeField] private Transform finalHeight;
        [SerializeField] private List<TextMeshProUGUI> people;
        [SerializeField] private JumpAndRunChanger changer;

        private void Start()
        {
            // Prevent the resolution hurting the credit duration.
            scrollSpeed = (finalHeight.position.y - transform.position.y) * .025f;
            FirePeople();
        }

        private void Update()
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

        /// <summary>
        /// Marks everyone that was fired red.
        /// </summary>
        private void FirePeople()
        {
            for (int i = 0; i < people.Count; i++)
            {
                if(changer.firedDepartments[i])
                {
                    people[i].color = Color.red;
                }
            }
        }
    }
}