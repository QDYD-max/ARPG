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
        EventCenter.Instance.AddEventListener<RoleBattle>(CEventType.RoleBattle, RoleBattleDamage);
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

    private void RoleBattleDamage(RoleBattle battle)
    {
        if(battle.DamagedEntity != this.gameObject) return;
        
        curNumeric[NumericType.HpBase] += battle.value;
        curRoleFSM.ChangeState(curNumeric[NumericType.Hp] < 0.0001f ? RoleState.Die : RoleState.Hurt, 1);
    }
    

    void CheckGrounded()
    {
        if (!mainCharacterController.isGrounded)
        {
            mainCharacterController.Move((transform.position + new Vector3(0, -1000, 0)) - transform.position);
        }
    }
}