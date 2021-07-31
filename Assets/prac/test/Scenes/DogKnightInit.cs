using System.Collections;
using System.Collections.Generic;
using CFramework;
using UnityEngine;

public class DogKnightInit : MonoBehaviour
{
    private Animator _animator;
    // Start is called before the first frame update
    void Start()
    {
        RoleCtrl dogCtrl = GetComponent<RoleCtrl>();
        NumericComponent dogNumeric = dogCtrl.curNumeric;
        dogNumeric.Initialize(NumericType.Hp);
        dogNumeric.Initialize(NumericType.MaxHp);
        dogNumeric.Initialize(NumericType.Speed);
        
        dogNumeric.Set(NumericType.HpBase, 100);
        dogNumeric.Set(NumericType.MaxHpBase, 100);
        dogNumeric.Set(NumericType.SpeedBase, 2);

        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<RoleCtrl>().curNumeric[NumericType.Hp] < 0)
        {
            //Debug.Log("Dog Knight Dead");
        }
    }
}
