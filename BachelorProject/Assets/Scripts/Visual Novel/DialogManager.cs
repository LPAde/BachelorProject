using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Visual_Novel
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI currentDialog;
        [SerializeField] private TextMeshProUGUI currentTalker;
        [SerializeField] private List<Dialog> dialogs;
        [SerializeField] private int dialogIndex;
        [SerializeField] private int textIndex;

        private void Update()
        {
            Inputs();
        }

        public void UpdateDialog(List<string> talkers, List<string> newDialogs)
        {
            
        }

        private void Inputs()
        {
            
        }
    }

    [Serializable]
    struct Dialog
    {
        public List<string> talkers;
        public List<string> dialogs;
    }
}