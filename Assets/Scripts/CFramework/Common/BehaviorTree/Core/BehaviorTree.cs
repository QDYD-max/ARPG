using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace CFramework.BT
{
    [CreateAssetMenu(fileName = "BehaviorTree", menuName = "QDYD")]
    public class BehaviorTree : ScriptableObject
    {
        public Node rootNode;
        public NodeState treeState = NodeState.Running;
        public List<Node> nodes = new List<Node>();

        public void Activate(Database database)
        {
            rootNode.Activate(database);
        }
        
        public NodeState Update()
        {
            if (rootNode.state == NodeState.Running)
            {
                treeState = rootNode.Update();
            }
            return treeState;
        }

        public Node CreateNode(System.Type type)
        {
            Node node = ScriptableObject.CreateInstance(type) as Node;
            node.name = type.Name;
            node.guid = GUID.Generate().ToString();
            nodes.Add(node);
            
            AssetDatabase.AddObjectToAsset(node, this);
            AssetDatabase.SaveAssets();
            return node;
        }

        public void DeleteNode(Node node)
        {
            nodes.Remove(node);
            AssetDatabase.RemoveObjectFromAsset(node);
            AssetDatabase.SaveAssets();        
        }

        public void AddChild(Node parent, Node child)
        {
            DecoratorNode decorator = parent as DecoratorNode;
            if (decorator)
            {
                decorator.child = child;
            }
            
            RootNode root = parent as RootNode;
            if (root)
            {
                root.child = child;
            }
            
            CompositeNode composite = parent as CompositeNode;
            if (composite)
            {
                composite.children.Add(child);
            }
            
        }

        public void RemoveChild(Node parent, Node child)
        {
            DecoratorNode decorator = parent as DecoratorNode;
            if (decorator)
            {
                decorator.child = null;
            }
            
            RootNode root = parent as RootNode;
            if (root)
            {
                root.child = null;
            }
            
            CompositeNode composite = parent as CompositeNode;
            if (composite)
            {
                composite.children.Remove(child);
            }
        }

        public List<Node> GetChildren(Node parent)
        {
            List<Node> children = new List<Node>();
            DecoratorNode decorator = parent as DecoratorNode;
            if (decorator && decorator.child!=null)
            {
                children.Add(decorator.child);
            }
            
            RootNode root = parent as RootNode;
            if (root && root.child!=null)
            {
                children.Add(root.child);
            }
            
            CompositeNode composite = parent as CompositeNode;
            if (composite)
            {
                return composite.children;
            }
            return children;
        }

        public BehaviorTree Clone()
        {
            BehaviorTree cloneTree = Instantiate(this);
            cloneTree.rootNode = rootNode.Clone();
            return cloneTree;
        }
    }
}