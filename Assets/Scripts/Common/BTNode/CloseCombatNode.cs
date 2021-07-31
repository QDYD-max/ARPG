using System;
using Pathfinding;
using UnityEngine;

namespace CFramework
{
    public class CloseCombatNode : ActionNode
    {
        [SerializeField] private int allAttackCombos = 2;
        [SerializeField] private string preAttackName = "Attack_0";

        private int _attackCombo;

        private RoleCtrl _curRoleCtrl;

        protected override void OnStart()
        {
            _curRoleCtrl = entity.GetComponent<RoleCtrl>();
            _attackCombo = 1;
        }


        protected override NodeState OnUpdate()
        {
            if (!_curRoleCtrl.isAttack)
            {
                _curRoleCtrl.direction = Vector3.zero;
                _curRoleCtrl.isAttack = true;
                if (_attackCombo > allAttackCombos)
                    return NodeState.Success;

                _curRoleCtrl.curAnimator.Play(preAttackName + _attackCombo);
                ++_attackCombo;
            }
            Debug.Log(entity.name + "战斗中");
            return NodeState.Running;
        }

        protected override void OnStop()
        {
            _attackCombo = 1;
            //_curRoleCtrl.curRoleFSM.ChangeState(RoleState.Idle, 0);
            Debug.Log("攻击结束");
        }
    }
}