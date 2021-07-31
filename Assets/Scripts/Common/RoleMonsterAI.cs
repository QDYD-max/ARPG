using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoleMonsterAI : IRoleAI
{
    public List<Vector3> PatrolPath;

    private void Awake()
    {
        tree = tree.Clone();
        tree.Activate(gameObject);
    }

    private void Update()
    {
        DoAI();
    }

    public override void DoAI()
    {
        if (tree != null)
        {
            tree.Update();
        }
    }
}
