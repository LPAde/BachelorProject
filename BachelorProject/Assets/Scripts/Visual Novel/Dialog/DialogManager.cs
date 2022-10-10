using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Visual_Novel.Dialog
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

        /// <summary>
        /// Updates the dialog.
        /// </summary>
        /// <param name="newDialogIndex"> The new dialog that is being spoken. </param>
        public void UpdateDialog(int newDialogIndex)
        {
            dialogIndex = newDialogIndex;
            textIndex = 0;
            
            currentTalker.text = dialogs[dialogIndex].talkers[textIndex];
            currentDialog.text = dialogs[dialogIndex].dialogs[textIndex];
        }

        /// <summary>
        /// Checks the players inputs.
        /// </summary>
        private void Inputs()
        {
            if(!Input.GetKeyDown(KeyCode.Return))
                return;

            textIndex++;
            
            if(textIndex >= dialogs[dialogIndex].dialogs.Count)
                VisualNovelManager.Instance.EndDialog();
            else
            {
                currentTalker.text = dialogs[dialogIndex].talkers[textIndex];
                currentDialog.text = dialogs[dialogIndex].dialogs[textIndex];
            }
        }
    }
}