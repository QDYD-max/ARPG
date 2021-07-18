using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;

namespace CFramework.BT
{
    public class BehaviorTreeEditor : EditorWindow
    {
        private BehaviorTreeView treeView;
        private InspectorView inspectorView;

        [MenuItem("QDYD/BehaviorTree")]
        public static void OpenEditor()
        {
            BehaviorTreeEditor wnd = GetWindow<BehaviorTreeEditor>();
            wnd.titleContent = new GUIContent("BehaviorTreeEditor");
        }

        public void CreateGUI()
        {
            // Each editor window contains a root VisualElement object
            VisualElement root = rootVisualElement;

            // Import UXML
            var visualTree =
                AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(
                    "Assets/Scripts/Framework/Common/BehaviorTree/Editor/BehaviorTreeEditor.uxml");
            visualTree.CloneTree(root);

            // A stylesheet can be added to a VisualElement.
            // The style will be applied to the VisualElement and all of its children.
            var styleSheet =
                AssetDatabase.LoadAssetAtPath<StyleSheet>(
                    "Assets/Scripts/Framework/Common/BehaviorTree/Editor/BehaviorTreeEditor.uss");
            root.styleSheets.Add(styleSheet);

            treeView = root.Q<BehaviorTreeView>();
            inspectorView = root.Q<InspectorView>();
            treeView.OnNodeSelected = OnNodeSelectedChanged;
            OnSelectionChange();
        }
        
        private void OnSelectionChange()
        {
            BehaviorTree tree = Selection.activeObject as BehaviorTree;
            if (tree)
            {
                treeView.PopulateView(tree);
            }
        }

        private void OnNodeSelectedChanged(NodeView nodeView)
        {
            inspectorView.UpdateSelection(nodeView);
        }
    }
}