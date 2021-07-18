using System;
using UnityEngine;

namespace CFramework.BT
{
    public class BTTest : MonoBehaviour
    {
        public BehaviorTree tree;
        public Database _database;

        private void Start()
        {
            //_database = transform.GetComponent<RoleDatabase>();
            tree = tree.Clone();
            tree.Activate(_database);
        }

        private void Update()
        {
            tree.Update();
        }
    }
}