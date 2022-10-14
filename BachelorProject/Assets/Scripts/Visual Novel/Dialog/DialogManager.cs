using System.Collections.Generic;
using UnityEngine;

namespace Visual_Novel.Dialog
{
    public class DialogManager : MonoBehaviour
    {
        [SerializeField] private List<Dialog> dialogs;
        [SerializeField] private List<GameObject> textBoxes;
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
            
            for (int i = 0; i < dialogs.Count; i++)
            {
                dialogs[i].gameObject.SetActive(i == dialogIndex);
            }
            
            textIndex = 0;
            textBoxes = dialogs[dialogIndex].TextBoxes;
            textBoxes[textIndex].SetActive(true);
        }

        /// <summary>
        /// Checks the players inputs.
        /// </summary>
        private void Inputs()
        {
            if(!Input.GetKeyDown(KeyCode.Return))
                return;

            textIndex++;
            
            if(textIndex >= dialogs[dialogIndex].TextBoxes.Count)
                VisualNovelManager.Instance.EndDialog();
            else
            {
                textBoxes[textIndex].SetActive(true);
            }
        }
    }
}