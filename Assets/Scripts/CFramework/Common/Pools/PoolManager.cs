using System;
using System.Collections.Generic;
using System.Resources;
using UnityEngine;
using UnityEngine.Pool;

namespace CFramework
{
    public class PoolManager : MonoSingleton<PoolManager>
    {
        private Dictionary<string, GameObjectPool> _gameObjectPoolsDic = new Dictionary<string, GameObjectPool>();

        public void InitPool(string resName, ResourceType resType,int defaultCapacity, int maxSize)
        {
            if (_gameObjectPoolsDic.ContainsKey(resName))
            {
                return;
            }
            _gameObjectPoolsDic[resName] = new GameObjectPool(resName, resType, defaultCapacity, maxSize);
        }

        public GameObject LoadGameObject(string resName, ResourceType resType)
        {
            if (!_gameObjectPoolsDic.ContainsKey(resName))
            {
                _gameObjectPoolsDic[resName] = new GameObjectPool(resName, resType);
            }
            GameObject retGO = _gameObjectPoolsDic[resName].Pool.Get();
            return retGO;
        }

        public void ReleaseGameObject(GameObject go, string resName)
        {
            if (_gameObjectPoolsDic[resName] == null)
            {
                return;
            }
            
            _gameObjectPoolsDic[resName].Pool.Release(go);
        }

        public void Clear()
        {
            foreach (var pool in _gameObjectPoolsDic)
            {
                pool.Value.Clear();
            }
            
            _gameObjectPoolsDic.Clear();
        }
    }
}