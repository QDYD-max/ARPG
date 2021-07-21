using System;
using System.Collections.Generic;
using UnityEngine;

namespace CFramework
{
    public class LesserGunSkillComponent : MonoBehaviour
    {
        private List<GameObject> ballList;
        private void Awake()
        {
            ballList = GetComponent<LesserGunAttackComponent>().ballList;
        }

        public void Execute()
        {
            //小技能单击形态
            for (int i = 0; i < ballList.Count; i++)
            {
                ballList[i].GetComponent<Rigidbody>().velocity =
                    (transform.position - ballList[i].transform.position).normalized * 10;
                ballList[i].GetComponent<BallCtrl>().isActivate = true;
            }

            ballList.Clear();
        }

        public void ExecuteHold()
        {
            for (int i = 0; i < ballList.Count; i++)
            {
                ballList[i].GetComponent<Rigidbody>().velocity = Vector3.zero;
                ballList[i].GetComponent<BallCtrl>().BallBoom();
            }
            ballList.Clear();
        }
        
    }
}