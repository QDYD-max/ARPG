using System;
using System.Collections.Generic;
using UnityEngine;

namespace CFramework
{
    public class BuffComponent : MonoBehaviour
    {
        public List<BuffBase> bufflist = new List<BuffBase>();
        private void Update()
        {
            foreach (BuffBase buff in bufflist)
            {
                buff.OnTick();
            }
        }

        public void AddBuff(BuffBase buff)
        {
            bufflist.Add(buff);
        }

        public void RemoveBuff()
        {
            
        }
    }
}