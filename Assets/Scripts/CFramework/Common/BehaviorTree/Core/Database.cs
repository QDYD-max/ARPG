using System;
using System.Collections.Generic;
using UnityEngine;

namespace CFramework.BT
{
    public abstract class Database : MonoBehaviour
    {
        private Dictionary<string, object> _dataBase;

        public T GetData<T>(string name)
        {
            return (T)_dataBase[name];
        }

        public void SetData(string name, Type obj)
        {
            _dataBase[name] = (object)obj;
        } 
    }
}