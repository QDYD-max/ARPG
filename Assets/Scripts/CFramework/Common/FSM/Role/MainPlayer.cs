using System;
using System.Collections;
using System.Collections.Generic;
using CFramework;
using Cinemachine;
using UnityEngine;
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
    private GameObject mainPlayer;
    public PlayerInput playerInput;

    private GameObject _vCamera;
    private CinemachineVirtualCamera _virtueCamera;

    //连击输入相关，具体由动画事件修改
    private WeaponState _weaponState = WeaponState.Sword;
    public bool isAttack = false;
    private int attackCombo = 1;


    private float _mosueDeltaX = 0;
    private float _mosueDeltaY = 0;

    private float timer;

    private Vector2 _mousePos;

    private bool _isSkill1Hold = false;


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
        _virtueCamera.Follow = mainPlayerCtrl.gameObject.transform;
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
    GameObject ballPrefab;
    private List<GameObject> ballList = new List<GameObject>();

    private IEnumerator CoLesserGunAttack(RaycastHit hit)
    {
        GameObject entity = hit.collider.gameObject;
        while (flag)
        {
            yield return new WaitForSeconds(0.2f);
            EventCenter.Instance.EventTrigger<RoleBattle>(CEventType.RoleBattle, new RoleBattle()
            {
                Attacker = mainPlayerCtrl.gameObject,
                DamagedEntity = entity,
                value = -10
            });
            //从命中位置生成小球
            GameObject ballEntity = GameObject.Instantiate(ballPrefab,
                hit.point + new Vector3(Random.Range(0, 0.4f), Random.Range(0, 0.4f), Random.Range(0, 0.4f)),
                Quaternion.identity);
            ballEntity.GetComponent<Rigidbody>().AddExplosionForce(200f, hit.point, 2f);
            ballList.Add(ballEntity);
        }

        yield return null;
    }
    //-----

    //-----领域展开内容
    private IEnumerator BurningScope()
    {
        for (int i = 0; i < 10; ++i)
        {
            Collider[] colliders = Physics.OverlapSphere(mainPlayerCtrl.transform.position, 3f, 1 << LayerMask.NameToLayer($"Enemy"));
            foreach (var collider in colliders)
            {
                EventCenter.Instance.EventTrigger<RoleBattle>(CEventType.RoleBattle, new RoleBattle()
                {
                    Attacker = mainPlayerCtrl.gameObject,
                    DamagedEntity = collider.gameObject,
                    value = -10
                });
                //从命中位置生成小球
                GameObject ballEntity = GameObject.Instantiate(ballPrefab, collider.transform);
            }

            yield return new WaitForSeconds(1f);
        }

        yield return null;
    }
    //---------

    public void OnAttack(InputAction.CallbackContext context)
    {
        if (ballPrefab == null)
        {
            ballPrefab = Resources.Load<GameObject>("ball");
        }

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
                switch (_weaponState)
                {
                    case WeaponState.Sword:

                        break;
                    case WeaponState.Gun:

                        #region 镭射枪

                        Vector3 pos = mainPlayerCtrl.transform.Find("GunPos").position;
                        //--------------交互部分
                        mainPlayerCtrl.Animator.SetBool("ToIdle", false);
                        mainPlayerCtrl.Animator.SetInteger("ToAttack", 5);
                        //--------------特效部分
                        effect.transform.position = pos; //有枪模型的话应该跟随枪 TODO
                        effect.transform.forward = mainPlayerCtrl.transform.forward;
                        effect.SetActive(true);
                        //------------程序判断部分
                        /*Vector3 screenPos = new Vector3(_mousePos.x, _mousePos.y,
                            Mathf.Abs(Camera.main.transform.position.z));
                        
                        Vector3 worldPos =
                            Camera.main.ScreenToWorldPoint(screenPos);*/

                        RaycastHit hit;
                        if (Physics.Raycast(pos, mainPlayerCtrl.transform.forward, out hit,
                            Mathf.Infinity, 1 << LayerMask.NameToLayer($"Enemy")))
                        {
                            flag = true;
                            StartCoroutine(CoLesserGunAttack(hit));
                            Debug.Log("Did Hit");
                        }
                        else
                        {
                            Debug.Log("Did not Hit");
                        }

                        #endregion

                        break;
                }

                break;
            case InputActionPhase.Canceled:
                switch (_weaponState)
                {
                    case WeaponState.Sword:
                        break;
                    case WeaponState.Gun:

                        #region 镭射枪

                        //-------松开结束镭射枪内容
                        mainPlayerCtrl.Animator.SetInteger("ToAttack", 0);
                        mainPlayerCtrl.Animator.SetBool("ToIdle", true);
                        flag = false;
                        effect.SetActive(false);

                        #endregion

                        break;
                }

                break;
        }

        //连击放在这里完成

        #region 武士刀

        if (!isAttack)
        {
            mainPlayerCtrl.direction = Vector2.zero;
            isAttack = true;
            if (attackCombo > 3)
                attackCombo = 1;
            mainPlayerCtrl.Animator.Play("Attack_0" + attackCombo);
            //mainPlayerCtrl.curRoleFSM.ChangeState(RoleState.Attack, attackCombo);
            timer = mainPlayerCtrl.Animator.GetCurrentAnimatorClipInfo(0).Length;
            attackCombo++;
        }

        #endregion
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
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Performed:
                if (!_isSkill1Hold)
                {
                    //小技能单击形态
                    for (int i = 0; i < ballList.Count; i++)
                    {
                        ballList[i].GetComponent<Rigidbody>().velocity =
                            (mainPlayerCtrl.transform.position - ballList[i].transform.position).normalized * 10;
                        ballList[i].GetComponent<BallCtrl>().isActivate = true;
                    }

                    ballList.Clear();
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
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Performed:
                //小技能长按形态
                _isSkill1Hold = true;
                for (int i = 0; i < ballList.Count; i++)
                {
                    ballList[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                    ballList[i].GetComponent<BallCtrl>().BallBoom();
                }

                ballList.Clear();
                break;
            case InputActionPhase.Canceled:
                break;
        }
    }

    public void OnSkill2(InputAction.CallbackContext context)
    {
        //技能2 大招
        switch (context.phase)
        {
            case InputActionPhase.Disabled:
                break;
            case InputActionPhase.Waiting:
                break;
            case InputActionPhase.Started:
                break;
            case InputActionPhase.Performed:
                StartCoroutine(BurningScope());
                break;
            case InputActionPhase.Canceled:
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
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

    public void OnAttackHold(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:

                break;
            case InputActionPhase.Canceled:

                break;
        }
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