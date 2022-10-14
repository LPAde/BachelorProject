using System.Collections.Generic;
using UnityEngine;

namespace Visual_Novel.Dialog
{
    public class Dialog : MonoBehaviour
    {
        [SerializeField] private List<GameObject> textBoxes;

        public List<GameObject> TextBoxes => textBoxes;
    }
}
