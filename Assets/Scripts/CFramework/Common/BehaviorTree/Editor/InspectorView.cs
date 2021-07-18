using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.Experimental.GraphView;

namespace CFramework.BT
{
    public class InspectorView : VisualElement
    {
        public new class UxmlFactor : UxmlFactory<InspectorView, VisualElement.UxmlTraits>
        {
        }

        private Editor editor;

        public InspectorView()
        {
        }

        public void UpdateSelection(NodeView nodeView)
        {
            Clear();

            UnityEngine.Object.DestroyImmediate(editor);
            editor = Editor.CreateEditor(nodeView.node);
            IMGUIContainer container = new IMGUIContainer(() => { editor.OnInspectorGUI(); });
            Add(container);
        }
    }
}