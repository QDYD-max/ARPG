using System;
using UnityEngine;

namespace CFramework
{
    public class SwordAttackComponent : SkillComponent
    {
        private RoleCtrl _roleCtrl;
        private float timer;
        private int attackCombo = 1;
        private void Awake()
        {
            _roleCtrl = GetComponent<RoleCtrl>();
        }

        private void Update()
        {
            if (timer != 0)
            {
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    attackCombo = 1;
                    MainPlayer.Instance.isAttack = false;
                }
            }
        }

        public void Execute()
        {
            if (!MainPlayer.Instance.isAttack)
            {
                _roleCtrl.direction = Vector2.zero;
                MainPlayer.Instance.isAttack = true;
                if (attackCombo > 3)
                    attackCombo = 1;
                
                _roleCtrl.curAnimator.Play("Attack_0" + attackCombo);
                

                //mainPlayerCtrl.curRoleFSM.ChangeState(RoleState.Attack, attackCombo);
                timer = _roleCtrl.curAnimator.GetCurrentAnimatorClipInfo(0).Length;
                attackCombo++;
            }
        }
    }

}