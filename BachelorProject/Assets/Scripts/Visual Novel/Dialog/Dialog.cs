using System.Collections.Generic;
using UnityEngine;

namespace Visual_Novel.Dialog
{
    [CreateAssetMenu(fileName = "Dialog", menuName = "Dialog", order = 0)]
    public class Dialog : ScriptableObject
    {
        public List<string> talkers; 
        public List<string> dialogs;
    }
}
