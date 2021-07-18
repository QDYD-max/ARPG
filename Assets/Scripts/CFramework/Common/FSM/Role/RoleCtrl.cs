using System;
using UnityEngine;
using System.Collections;
using CFramework;
using DG.Tweening;
using UnityEngine.UI;

public class RoleCtrl : MonoBehaviour
{
    public Animator Animator;
    public RoleType curRoleType = RoleType.None;
    public IRoleAI curAI = null;
    public CharacterController mainCharacterController;
    public RoleFSMMgr curRoleFSM = null;

    public NumericComponent curNumeric;

    //摄像机、移动相关
    public Vector2 direction = Vector2.one;
    public bool isRunState = false;

    private void Awake()
    {
        Animator = gameObject.GetComponent<Animator>();
        curNumeric = gameObject.AddComponent<NumericComponent>();
    }

    public void Init(RoleType roleType)
    {
        curRoleType = roleType;
    }

    // Use this for initialization
    void Start()
    {
        mainCharacterController = GetComponent<CharacterController>();

        curRoleFSM = new RoleFSMMgr(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (curAI != null)
        {
            curAI.DoAI();
        }

        if (curRoleFSM != null)
        {
            curRoleFSM.OnUpdate();
        }
        
        //if (mainCharacterController == null) return;


        //CheckGrounded();
    }
    
    public void Damaged(int damageNum)
    {
        curNumeric[NumericType.HpBase] -= damageNum;
        
        
        if (curNumeric[NumericType.Hp] < 0.0001f)
        {
            curRoleFSM.ChangeState(RoleState.Die, 1);
        }
        else
        {
            curRoleFSM.ChangeState(RoleState.Hurt, 1);
        }
    }

    void CheckGrounded()
    {
        if (!mainCharacterController.isGrounded)
        {
            mainCharacterController.Move((transform.position + new Vector3(0, -1000, 0)) - transform.position);
        }
    }
}