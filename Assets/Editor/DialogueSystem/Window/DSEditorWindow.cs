using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace misionvial.Ds.System
{
    public class DSEditorWindow : EditorWindow
    {
        [MenuItem("Window/Ds/Dialogue Graph")]
        public static void ShowExample()
        {
            EditorWindow window = GetWindow<DSEditorWindow>();
            window.titleContent = new GUIContent("Dialogue Graph");
        }
        private void OnEnable()
        {
            editorGraphView();
            AddVariables();
        }
        private void editorGraphView()
        {
            DSGraphView graphView = new DSGraphView();
            graphView.StretchToParentSize();
            rootVisualElement.Add(graphView);
        }
        private void AddVariables()
        {
            StyleSheet styleSheet = (StyleSheet)EditorGUIUtility.Load("DialogueSystem/DSVariables.uss");
            rootVisualElement.styleSheets.Add(styleSheet);
        }
    }
}