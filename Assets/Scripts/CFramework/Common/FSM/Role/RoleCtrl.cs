using System;
using UnityEngine;
using System.Collections;
using CFramework;
using DG.Tweening;
using UnityEngine.UI;

public class RoleCtrl : MonoBehaviour
{
    public Animator curAnimator;
    public IRoleAI curAI = null;
    public CharacterController curCharacterController;
    public RoleFSMMgr curRoleFSM = null;
    public NumericComponent curNumeric;

    //摄像机、移动相关
    public Vector3 direction = Vector3.one;
    public bool isRunState = false;
    public GameObject target;
    public bool isAttack = false;

    private void Awake()
    {
        curNumeric = gameObject.AddComponent<NumericComponent>();

        EventCenter.Instance.AddEventListener<RoleBattle>(CEventType.RoleBattle, RoleBattleDamage);
    }

    // Use this for initialization
    void Start()
    {
        curAnimator = gameObject.GetComponent<Animator>();
        curCharacterController = GetComponent<CharacterController>();
        curAI = GetComponent<IRoleAI>();
        curRoleFSM = new RoleFSMMgr(this);
    }

    // Update is called once per frame
    void Update()
    {
        CheckGrounded();
        if (curRoleFSM != null)
        {
            curRoleFSM.OnUpdate();
        }
    }
    

    private void RoleBattleDamage(RoleBattle battle)
    {
        if(battle.DamagedEntity != this.gameObject) return;
        
        curNumeric[NumericType.HpBase] += battle.value;
        curRoleFSM.ChangeState(curNumeric[NumericType.Hp] < 0.0001f ? RoleState.Die : RoleState.Hurt, 1);
    }


    void CheckGrounded()
    {
        if (!curCharacterController.isGrounded)
        {
            curCharacterController.Move((new Vector3(0, -1000, 0)));
        }
    }
}