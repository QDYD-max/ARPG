using System;
using UnityEngine;

namespace CFramework
{
    public class BTTest : MonoBehaviour
    {
        public BehaviorTree tree;
        public GameObject gameObject;

        private void Start()
        {
            //GameObject = transform.GetComponent<RoleDatabase>();
            tree = tree.Clone();
            tree.Activate(gameObject);
        }

        private void Update()
        {
            tree.Update();
        }
    }
}