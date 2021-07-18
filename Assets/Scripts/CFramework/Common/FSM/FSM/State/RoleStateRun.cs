using System.Collections;
using System.Collections.Generic;
using CFramework;
using UnityEngine;

public class RoleStateRun : RoleStateAbstract
{
    public float turnSpeed = 10f;
    public KeyCode sprintKeyboard = KeyCode.LeftShift;

    private float turnSpeedMultiplier;
    private float speed = 0f;

    private Animator anim;
    private Vector3 targetDirection;
    private Vector2 direction;
    private Quaternion freeRotation;
    private Camera mainCamera = Camera.main;
    private float velocity;
    private bool isRunState;

    public RoleStateRun(RoleFSMMgr roleFsm) : base(roleFsm)
    {
    }


    /// <summary>
    /// 实现 进入状态
    /// </summary>
    public override void OnEnter()
    {
        base.OnEnter();
    }

    /// <summary>
    /// 实现 状态进行
    /// </summary>
    public override void OnUpdate()
    {
        base.OnUpdate();

        #region 移动(使用速度控制)

        direction = CurRoleFSM.CurRoleCtrl.direction;
        
        speed = CurRoleFSM.CurRoleCtrl.curNumeric[NumericType.Speed];

        if (CurRoleFSM.CurRoleCtrl.isRunState)
        {
            speed = CurRoleFSM.CurRoleCtrl.curNumeric[NumericType.Speed];
            curStateValue = 2;
        }
        else
        {
            speed = CurRoleFSM.CurRoleCtrl.curNumeric[NumericType.Speed];
            curStateValue = 1;
        }
        
        CurRoleFSM.CurRoleCtrl.Animator.SetInteger(RoleAnimatorCondition.ToRun.ToString(), curStateValue);

        UpdateTargetDirection();
        if (direction != Vector2.zero && targetDirection.magnitude > 0.1f)
        {
            Vector3 lookDirection = targetDirection.normalized;
            freeRotation = Quaternion.LookRotation(lookDirection, CurRoleFSM.CurRoleCtrl.transform.up);
            var differenceRotation = freeRotation.eulerAngles.y - CurRoleFSM.CurRoleCtrl.transform.eulerAngles.y;
            var eulerY = CurRoleFSM.CurRoleCtrl.transform.eulerAngles.y;

            if (differenceRotation < 0 || differenceRotation > 0) eulerY = freeRotation.eulerAngles.y;
            var euler = new Vector3(0, eulerY, 0);

            CurRoleFSM.CurRoleCtrl.transform.rotation = Quaternion.Slerp(
                CurRoleFSM.CurRoleCtrl.transform.rotation, Quaternion.Euler(euler),
                turnSpeed * turnSpeedMultiplier * Time.deltaTime);
        }
        else
        {
            CurRoleFSM.ChangeState(RoleState.Idle, 1);
        }

        #endregion
    }

    /// <summary>
    /// 离开状态
    /// </summary>
    /// 
    public override void OnLeave()
    {
        base.OnLeave();
        CurRoleFSM.CurRoleCtrl.Animator.SetInteger(RoleAnimatorCondition.ToRun.ToString(), 0);
    }

    #region 转身

    private void UpdateTargetDirection()
    {
        turnSpeedMultiplier = 1f;
        var forward = mainCamera.transform.TransformDirection(Vector3.forward);
        forward.y = 0;

        //get the right-facing direction of the referenceTransform
        var right = mainCamera.transform.TransformDirection(Vector3.right);

        // determine the direction the player will face based on direction and the referenceTransform's right and forward directions
        targetDirection = direction.x * right + direction.y * forward;

        CurRoleFSM.CurRoleCtrl.mainCharacterController.Move(targetDirection.normalized * Time.deltaTime * speed);
    }

    #endregion
}