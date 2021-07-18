using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEditor.Experimental.GraphView;
using UnityEditor.VersionControl;
using UnityEngine.UIElements;

namespace CFramework.BT
{
    public class BehaviorTreeView : GraphView
    {
        public Action<NodeView> OnNodeSelected;
        private BehaviorTree tree;

        public new class UxmlFactory : UxmlFactory<BehaviorTreeView, GraphView.UxmlTraits>
        {
        }

        public BehaviorTreeView()
        {
            Insert(0, new GridBackground());

            this.AddManipulator(new ContentZoomer());
            this.AddManipulator(new ContentDragger());
            this.AddManipulator(new SelectionDragger());
            this.AddManipulator(new RectangleSelector());

            var styleSheet =
                AssetDatabase.LoadAssetAtPath<StyleSheet>(
                    "Assets/Scripts/Framework/Common/BehaviorTree/Editor/BehaviorTreeEditor.uss");
            styleSheets.Add(styleSheet);
        }

        private NodeView FindNodeView(Node node)
        {
            return GetNodeByGuid(node.guid) as NodeView;
        }

        internal void PopulateView(BehaviorTree tree)
        {
            //根据选中的BehaviorTree，清除当前面板，创建节点
            //具体操作围绕GraphView的Element操作展开
            this.tree = tree;

            graphViewChanged -= OnGraphViewChanged;
            DeleteElements(graphElements.ToList());
            graphViewChanged += OnGraphViewChanged;

            if (tree.rootNode == null)
            {
                tree.rootNode = tree.CreateNode(typeof(RootNode)) as RootNode;
                EditorUtility.SetDirty(tree);
                AssetDatabase.SaveAssets();
            }

            //创建节点
            tree.nodes.ForEach(n => CreateNodeView(n));

            //创建 边
            tree.nodes.ForEach(n =>
            {
                var children = tree.GetChildren(n);
                children.ForEach(c =>
                {
                    NodeView parentView = FindNodeView(n);
                    NodeView childView = FindNodeView(c);
                    if (parentView.output != null && childView.input != null)
                    {
                        Edge edge = parentView.output.ConnectTo(childView.input);
                        AddElement(edge);
                    }
                });
            });
        }

        public override List<Port> GetCompatiblePorts(Port startPort, NodeAdapter nodeAdapter)
        {
            return ports.ToList()
                .Where(endPort =>
                    endPort.direction != startPort.direction
                    && endPort.node != startPort.node).ToList();
        }

        private GraphViewChange OnGraphViewChanged(GraphViewChange graphviewchange)
        {
            //graphviewchange提供了各种Element的操作事件
            if (graphviewchange.elementsToRemove != null)
            {
                graphviewchange.elementsToRemove.ForEach(elem =>
                {
                    if (elem is NodeView nodeView)
                    {
                        tree.DeleteNode(nodeView.node);
                    }

                    if (elem is Edge edge)
                    {
                        NodeView parentView = edge.output.node as NodeView;
                        NodeView childView = edge.input.node as NodeView;
                        tree.RemoveChild(parentView.node, childView.node);
                    }
                });
            }

            if (graphviewchange.edgesToCreate != null)
            {
                graphviewchange.edgesToCreate.ForEach(edge =>
                {
                    NodeView parentView = edge.output.node as NodeView;
                    NodeView childView = edge.input.node as NodeView;
                    tree.AddChild(parentView.node, childView.node);
                });
            }

            return graphviewchange;
        }

        public override void BuildContextualMenu(ContextualMenuPopulateEvent evt)
        {
            //重写右键菜单
            //base.BuildContextualMenu(evt);
            {
                var types = TypeCache.GetTypesDerivedFrom<ActionNode>();
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
                }
            }

            {
                var types = TypeCache.GetTypesDerivedFrom<CompositeNode>();
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
                }
            }

            {
                var types = TypeCache.GetTypesDerivedFrom<DecoratorNode>();
                foreach (var type in types)
                {
                    evt.menu.AppendAction($"[{type.BaseType.Name}] {type.Name}", (a) => CreateNode(type));
                }
            }
        }

        //数据层操作
        private void CreateNode(System.Type type)
        {
            Node node = this.tree.CreateNode(type);
            CreateNodeView(node);
        }

        private void CreateNodeView(Node node)
        {
            NodeView nodeView = new NodeView(node);
            nodeView.OnNodeSelected = OnNodeSelected;
            AddElement(nodeView);
        }
    }
}