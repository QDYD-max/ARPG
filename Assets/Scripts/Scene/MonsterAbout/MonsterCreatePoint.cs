using System.Collections;
using System.Collections.Generic;
using CFramework;
using UnityEngine;

public class MonsterCreatePoint : MonoBehaviour
{
    [SerializeField]
    private int m_CurCount;
    private int m_Max_Count = 1;
    private float m_PrevCreateTime;
    // Start is called before the first frame update
    void Start()
    {
        m_CurCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(m_CurCount <m_Max_Count)
        {
            if(Time.time > m_PrevCreateTime + Random.Range(1.5f,3.5f))
            {
                m_PrevCreateTime = Time.time;

                GameObject objClone = RoleMgr.Instance.LoadRole(RoleType.Monster);
                objClone.transform.parent = transform;
                objClone.transform.position = transform.TransformPoint(new Vector3(0, 0, 0));

                RoleCtrl roleCtrl = objClone.GetComponent<RoleCtrl>();
                objClone.AddComponent<RoleMonsterAI>();

                roleCtrl.Init(RoleType.Monster);

                m_CurCount++;
            }
        }
    }
}
