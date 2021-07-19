using System.Collections;
using System.Collections.Generic;
using CFramework;
using UnityEngine;

public class BallCtrl : MonoBehaviour
{
    private Rigidbody _rigidbody;

    public bool isActivate = false;

    // Start is called before the first frame update
    void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_rigidbody.velocity.magnitude < 0.05f)
        {
            _rigidbody.velocity = Vector3.zero;
        }

        if (isActivate)
        {
            if ((MainPlayer.Instance.mainPlayerCtrl.transform.position - transform.position).magnitude < 0.5f)
            {
                gameObject.SetActive(false);
                Destroy(this);
            }
        }
    }

    public void BallBoom()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, 1f, 1 << LayerMask.NameToLayer($"Enemy"));
        for (int i = 0; i < hitColliders.Length; i++)
        {
            EventCenter.Instance.EventTrigger<RoleBattle>(CEventType.RoleBattle, new RoleBattle()
            {
                Attacker = MainPlayer.Instance.mainPlayerCtrl.gameObject,
                DamagedEntity = hitColliders[i].gameObject,
                value = -5
            });
        }
        
        gameObject.SetActive(false);
        Destroy(this);
    }
}