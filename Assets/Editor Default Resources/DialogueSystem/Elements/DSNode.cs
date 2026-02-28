using UnityEditor.Experimental.GraphView;
using System.Collections.Generic;

namespace misionvial.Ds.Elements
{
    using Enumerations;


    public class DSNode : Node
    {
        public string DialogueName { set; get; }
        public List<string> Choises { set; get; }
        public string text { set; get; }
        public DSDialogueType DialogueType { set; get; }

        public void Initialize()
        {
            DialogueName = "Dialogue Name";
            Choises = new List<string>();
            text = "Dialogue Text";
        }
    }
}
