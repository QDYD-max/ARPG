using System;
using System.Collections;
using System.Collections.Generic;
using CFramework;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public enum WeaponState
{
    Sword,
    Gun
}

public class MainPlayer : MonoSingleton<MainPlayer>, PlayerInput.IPlayerActions
{
    public RoleCtrl mainPlayerCtrl;
    public GameObject mainPlayer;
    public PlayerInput playerInput;

    private GameObject _vCamera;
    private CinemachineVirtualCamera _virtueCamera;

    //连击输入相关，具体由动画事件修改
    public bool isAttack = false;
    private WeaponState _curState;

    private float _mosueDeltaX = 0;
    private float _mosueDeltaY = 0;
    private Vector2 _mousePos;

    private bool _isSkill1Hold = false;

    private UnityAction Attack;
    private UnityAction AttackHold;
    private UnityAction AttackCancle;
    private UnityAction Skill1;
    private UnityAction Skill1Hold;
    private UnityAction Skill1Cancle;
    private UnityAction Skill2;
    private UnityAction Skill2Hold;
    private UnityAction Skill2Cancle;


    private void Awake()
    {
        playerInput = new PlayerInput();
        playerInput.Player.SetCallbacks(this);
    }

    private void Start()
    {
        UIPanel mainUI = UIManager.Instance.LoadUI(UILayer.MainLayer, "Panel_MainPlayer");
        mainUI.OnInit();
        mainUI.OnShow();

        InitMainPlayer();
        Init3RdCamera();

        _virtueCamera = _vCamera.GetComponent<CinemachineVirtualCamera>();
        _virtueCamera.Follow = mainPlayer.transform;

        InitPlayer();
        ChangeWeapon(WeaponState.Sword);
    }

    void InitPlayer()
    {
        NumericComponent playerNumeric = mainPlayerCtrl.curNumeric;

        playerNumeric.Initialize(NumericType.Speed);
        playerNumeric.Set(NumericType.SpeedBase, 10);

        playerNumeric.Initialize(NumericType.MaxHp);
        playerNumeric.Initialize(NumericType.Hp);

        playerNumeric.Set(NumericType.MaxHpBase, 2000);
        playerNumeric.Set(NumericType.HpBase, 1000);

        playerNumeric.Initialize(NumericType.MaxShield);
        playerNumeric.Initialize(NumericType.Shield);

        playerNumeric.Set(NumericType.MaxShieldBase, 30);
        playerNumeric.Set(NumericType.ShieldBase, 10);

        playerNumeric.Initialize(NumericType.MaxEnergy);
        playerNumeric.Initialize(NumericType.Energy);

        playerNumeric.Set(NumericType.MaxEnergyBase, 300);
        playerNumeric.Set(NumericType.EnergyBase, 100);

        playerNumeric.Initialize(NumericType.MaxSkillCd1);
        playerNumeric.Initialize(NumericType.SkillCd1);

        playerNumeric.Set(NumericType.MaxSkillCd1Base, 10);
        playerNumeric.Set(NumericType.SkillCd1Base, 10);

        playerNumeric.Initialize(NumericType.MaxSkillCd2);
        playerNumeric.Initialize(NumericType.SkillCd2);

        playerNumeric.Set(NumericType.MaxSkillCd2Base, 10);
        playerNumeric.Set(NumericType.SkillCd2Base, 10);

        playerNumeric.Initialize(NumericType.MaxSkillCd3);
        playerNumeric.Initialize(NumericType.SkillCd3);

        playerNumeric.Set(NumericType.MaxSkillCd3Base, 10);
        playerNumeric.Set(NumericType.SkillCd3Base, 10);
    }


    private void OnEnable()
    {
        playerInput.Player.Enable();
    }


    private void OnDisable()
    {
        if (playerInput != null)
        {
            playerInput.Player.Disable();
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (isAttack) return;
        switch (context.phase)
        {
            case InputActionPhase.Disabled:
                break;
            case InputActionPhase.Waiting:
                break;
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Performed:
                mainPlayerCtrl.direction = context.ReadValue<Vector2>();
                mainPlayerCtrl.curRoleFSM.ChangeState(RoleState.Run, 1);
                break;
            case InputActionPhase.Canceled:
                mainPlayerCtrl.direction = Vector2.zero;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                Attack?.Invoke();
                break;
        }
    }
    
    public void OnAttackCancled(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                AttackCancle?.Invoke();
                break;
        }
    }

    public void OnAttackHold(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                AttackHold?.Invoke();
                break;
        }
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        //冲刺
        Debug.Log("冲刺");
    }

    public void OnSkill1(InputAction.CallbackContext context)
    {
        //技能1 小技能
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                if (!_isSkill1Hold)
                {
                    Skill1.Invoke();
                }
                _isSkill1Hold = false;
                break;
            case InputActionPhase.Canceled:
                break;
        }
    }

    public void OnSkill1Hold(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                //小技能长按形态
                Skill1Hold?.Invoke();
                _isSkill1Hold = true;
                break;
        }
    }

    public void OnSkill2(InputAction.CallbackContext context)
    {
        //技能2 大招
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                Skill2?.Invoke();
                break;
            case InputActionPhase.Canceled:
                Skill2Cancle?.Invoke();
                break;
        }
    }

    private void OnGUI()
    {
        if (GUILayout.Button("ChangeState"))
        {
            switch (_curState)
            {
                case WeaponState.Sword:
                    ChangeWeapon(WeaponState.Gun);
                    break;
                case WeaponState.Gun:
                    ChangeWeapon(WeaponState.Sword);
                    break;
            }
        }
    }

    public void ChangeWeapon(WeaponState state)
    {
        ClearWeaponSkill();
        _curState = state;
        switch (state)
        {
            case WeaponState.Sword:
                mainPlayer.GetComponent<Animator>().runtimeAnimatorController =
                    mainPlayer.GetComponent<AnimEvents>().AnimatorSword;
                Attack = mainPlayer.GetComponent<SwordAttackComponent>().Execute;
                Skill1 = mainPlayer.GetComponent<SwordSkillComponent>().Execute;
                break;
            case WeaponState.Gun:
                mainPlayer.GetComponent<Animator>().runtimeAnimatorController =
                    mainPlayer.GetComponent<AnimEvents>().AnimatorGun;
                Attack = mainPlayer.GetComponent<LesserGunAttackComponent>().Execute;
                AttackCancle = mainPlayer.GetComponent<LesserGunAttackComponent>().Cancle;
                Skill1 = mainPlayer.GetComponent<LesserGunSkillComponent>().Execute;
                Skill1Hold = mainPlayer.GetComponent<LesserGunSkillComponent>().ExecuteHold;
                break;
        }
    }

    public void ClearWeaponSkill()
    {
        Attack = null;
        AttackHold = null;
        AttackCancle = null;
        Skill1 = null;
        Skill1Hold = null;
        Skill1Cancle = null;
        Skill2 = null;
        Skill2Hold = null;
        Skill2Cancle = null;
    }

    public void OnCameraRotate(InputAction.CallbackContext context)
    {
        _mosueDeltaX = context.ReadValue<Vector2>().x;
        _mosueDeltaY = context.ReadValue<Vector2>().y;

        _mosueDeltaX /= 20;
        _mosueDeltaY /= 100;

        float rotateX = _vCamera.transform.localEulerAngles.x + _mosueDeltaY;
        float rotateY = _vCamera.transform.localEulerAngles.y + _mosueDeltaX;
        //localEulerAngles 0~90 270~360 --> -90~90 *改变的是270~360部分
        rotateX = CheckAngle(rotateX);
        rotateX = Mathf.Clamp(rotateX, -89.5f, 89.5f);
        if (rotateX < -88 || rotateX > 88)
        {
            //到达顶点时防止画面抖动，不进行绕Y轴旋转
            rotateX = ReturnToEulerAngle(rotateX);
            _vCamera.transform.localEulerAngles = new Vector3(rotateX, _vCamera.transform.localEulerAngles.y, 0);
        }
        else
        {
            rotateX = ReturnToEulerAngle(rotateX);
            _vCamera.transform.localEulerAngles = new Vector3(rotateX, rotateY, 0);
        }
    }

    public void OnCameraDistance(InputAction.CallbackContext context)
    {
        //1.5~7
        Vector2 scrollVector2 = context.ReadValue<Vector2>();
        float distanceChange = scrollVector2.y * 0.01f;
        CinemachineFramingTransposer transposer = _virtueCamera.GetCinemachineComponent<CinemachineFramingTransposer>();
        transposer.m_CameraDistance = Mathf.Clamp(transposer.m_CameraDistance - distanceChange, 1.5f, 7f);
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        _mousePos = context.ReadValue<Vector2>();
    }

    private void InitMainPlayer()
    {
        GameObject mainPlayerPrefab = ResourceLoader.Load<GameObject>(ResourceType.Role, "MainPlayer");
        mainPlayer = Instantiate(mainPlayerPrefab);
        mainPlayerCtrl = mainPlayer.GetComponent<RoleCtrl>();
        mainPlayer.AddComponent<SwordSkillComponent>();
        mainPlayer.AddComponent<SwordAttackComponent>();
        mainPlayer.AddComponent<LesserGunAttackComponent>();
        mainPlayer.AddComponent<LesserGunSkillComponent>();
        
    }

    private void Init3RdCamera()
    {
        //GameObject cameraPrefab = ResourceLoader.Load<GameObject>(ResourceType.Game, "CM3rdPersonNormal");
        //_vCamera = Instantiate(cameraPrefab);
        _vCamera = GameObject.Find("CM3rdPersonNormal");
    }

    private float CheckAngle(float value) // 将大于180度角进行以负数形式输出
    {
        float angle = value;
        if (angle > 260)
        {
            angle -= 360;
        }

        return angle;
    }

    private float ReturnToEulerAngle(float value)
    {
        float angle = value;
        if (value < 0)
        {
            angle += 360;
        }

        return angle;
    }
}