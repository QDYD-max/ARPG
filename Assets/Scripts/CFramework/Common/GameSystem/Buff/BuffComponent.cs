using System;
using System.Collections.Generic;
using UnityEngine;

namespace CFramework
{
    public class BuffComponent : MonoBehaviour
    {
        private List<IBuff> bufflist = new List<IBuff>();

        public void AddBuff(IBuff buff)
        {
            bufflist.Add(buff);
        }

        public void RemoveBuff(IBuff buff)
        {
            bufflist.Remove(buff);
        }
    }
}