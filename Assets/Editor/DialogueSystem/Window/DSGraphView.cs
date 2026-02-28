using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEngine.UIElements;
using misionvial.Ds.Elements;
namespace misionvial.Ds.System
{
    public class DSGraphView : GraphView
    {
        public DSGraphView()
        {
            AddManipulators();
            AddGridBackground();
            CreateNode();
            AddStyle();
        }
       private void CreateNode()
        {
            DSNode node = new DSNode(); 
            AddElement(node);
        }
        private void AddGridBackground()
        {
            GridBackground gridbackground = new GridBackground();
            gridbackground.StretchToParentSize();
            Insert(0, gridbackground);
        }

        private void AddStyle()
        {
            StyleSheet styleSheet = (StyleSheet)EditorGUIUtility.Load("DialogueSystem/DSGraphViewStyle.uss");
            styleSheets.Add(styleSheet);
        }
        private void AddManipulators()
        {
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());
        }
    }
}
