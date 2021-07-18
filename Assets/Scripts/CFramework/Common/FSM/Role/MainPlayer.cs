using System;
using System.Collections;
using System.Collections.Generic;
using CFramework;
using Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainPlayer : MonoSingleton<MainPlayer>, PlayerInput.IPlayerActions
{
    public RoleCtrl mainPlayerCtrl;
    private GameObject mainPlayer;
    public PlayerInput playerInput;

    private GameObject _vCamera;

    //连击输入相关，具体由动画事件修改
    public bool isAttack = false;
    private int attackCombo = 1;
    private float _mosueDeltaX = 0;
    private float _mosueDeltaY = 0;

    private float timer;


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


        CinemachineVirtualCamera _virtue3RdCamera = _vCamera.GetComponent<CinemachineVirtualCamera>();
        _virtue3RdCamera.Follow = mainPlayerCtrl.gameObject.transform;
    }

    void OnGUI()
    {
        if (GUILayout.Button("Init Player"))
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
    }


    private void OnEnable()
    {
        playerInput.Player.Enable();
    }

    private void FixedUpdate()
    {
        if (timer != 0)
        {
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                attackCombo = 1;
                isAttack = false;
            }
        }
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


    //-----镭射枪内容
    private bool flag = false;
    GameObject effect;

    private IEnumerator CoBuffOnTick(GameObject entity)
    {
        while (flag)
        {
            yield return new WaitForSeconds(0.2f);
            print(entity.GetComponent<NumericComponent>()[NumericType.HpBase]);
            EventCenter.Instance.EventTrigger<RoleBattle>(CEventType.RoleBattle, new RoleBattle()
            {
                Attacker = mainPlayerCtrl.gameObject,
                DamagedEntity = entity,
                value = -10
            });
        }

        yield return null;
    }
    //-----

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (effect == null)
        {
            GameObject effectPrefab = Resources.Load<GameObject>("LaserBeam");
            effect = GameObject.Instantiate(effectPrefab);
        }

        switch (context.phase)
        {
            case InputActionPhase.Disabled:
                break;
            case InputActionPhase.Waiting:
                break;
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Performed:
            {
                Vector3 pos = mainPlayerCtrl.transform.Find("GunPos").position;
                //--------------交互部分
                mainPlayerCtrl.Animator.SetBool("ToIdle", false);
                mainPlayerCtrl.Animator.SetInteger("ToAttack", 5);
                //--------------特效部分
                effect.transform.position = pos; //有枪模型的话应该跟随枪 TODO
                effect.transform.forward = mainPlayerCtrl.transform.forward;
                effect.SetActive(true);
                //------------程序判断部分
                RaycastHit hit;
                if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit,
                    Mathf.Infinity, 1 << LayerMask.NameToLayer($"Enemy")))
                {
                    flag = true;
                    StartCoroutine(CoBuffOnTick(hit.collider.gameObject));
                    Debug.Log("Did Hit");
                }
                else
                {
                    Debug.Log("Did not Hit");
                }

                break;
            }
            case InputActionPhase.Canceled:
                //-------松开结束镭射枪内容
                mainPlayerCtrl.Animator.SetInteger("ToAttack", 0);
                mainPlayerCtrl.Animator.SetBool("ToIdle", true);
                flag = false;
                effect.SetActive(false);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        //连击放在这里完成
        /*if (!isAttack)
        {
            mainPlayerCtrl.direction = Vector2.zero;
            isAttack = true;
            if (attackCombo > 3)
                attackCombo = 1;
            mainPlayerCtrl.Animator.Play("Attack_0" + attackCombo);
            //mainPlayerCtrl.curRoleFSM.ChangeState(RoleState.Attack, attackCombo);
            timer = mainPlayerCtrl.Animator.GetCurrentAnimatorClipInfo(0).Length;
            attackCombo++;
        }*/
    }

    public void OnDodge(InputAction.CallbackContext context)
    {
        //冲刺
        Debug.Log("冲刺");
    }

    public void OnSkill1(InputAction.CallbackContext context)
    {
        //技能1 小技能
        Debug.Log("小技能");
        mainPlayerCtrl.Animator.Play("Attack_04");
    }

    public void OnSkill2(InputAction.CallbackContext context)
    {
        //技能2 大招
        Debug.Log("大招");
    }

    public void OnCameraRotate(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Disabled:
                break;
            case InputActionPhase.Waiting:
                break;
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Performed:
                _mosueDeltaX = context.ReadValue<Vector2>().x;
                _mosueDeltaY = context.ReadValue<Vector2>().y;
                break;
            case InputActionPhase.Canceled:
                _mosueDeltaX = 0;
                _mosueDeltaY = 0;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        _mosueDeltaY /= 5;

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
        Debug.Log("摄像机距离");
    }

    private void InitMainPlayer()
    {
        GameObject mainPlayerPrefab = ResourceLoader.Load<GameObject>(ResourceType.Role, "MainPlayer");
        mainPlayer = Instantiate(mainPlayerPrefab);
        mainPlayerCtrl = mainPlayer.GetComponent<RoleCtrl>();
        mainPlayerCtrl.Init(RoleType.Player);
    }

    private void Init3RdCamera()
    {
        GameObject cameraPrefab = ResourceLoader.Load<GameObject>(ResourceType.Game, "CM3rdPersonNormal");
        _vCamera = Instantiate(cameraPrefab);
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