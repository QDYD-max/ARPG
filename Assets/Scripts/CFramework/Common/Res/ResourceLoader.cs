using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using CFramework;
using UnityEngine;
using UnityEngine.Events;
using Object = UnityEngine.Object;

namespace CFramework
{
    public static class ResourceLoader
    {
        //资源缓存
        private static Dictionary<string, Object> _cacheDic = new Dictionary<string, Object>();

        public static T Load<T>(ResourceType type, string name, bool isCache = false) where T : Object
        {
            //Key值为路径
            string key = GetPath(type, name);
            //Debug.Log("Load Path: " + key);
            T obj;

            if (_cacheDic.ContainsKey(key))
            {
                obj = _cacheDic[key] as T;
            }
            else
            {
                obj = Resources.Load<T>(key);
                if (isCache)
                {
                    _cacheDic.Add(key, obj);
                }
            }

            return obj;
        }

        public static void LoadAsync<T>(ResourceType type, string name, UnityAction<T> callback, bool isCache = false) where T : Object
        {
            //Key值为路径
            T obj;
            string key = GetPath(type, name);
            //Debug.Log("LoadAsync Path: " + key);
            if (_cacheDic.ContainsKey(key))
            {
                obj = _cacheDic[key] as T;
                callback(obj);
            }
            else
            {
                GameManager.Instance.StartCoroutine(CoLoadAsync(key, callback, isCache));
            }
        }
        
        private static IEnumerator CoLoadAsync<T>(string path, UnityAction<T> callback, bool isCache) where T : Object
        {

            ResourceRequest r = Resources.LoadAsync<T>(path);
            yield return r;
            if (isCache)
            {
                _cacheDic.Add(path, r.asset);
            }
            callback(r.asset as T);
        }

        private static string GetPath(ResourceType type, string name)
        {
            StringBuilder sb = new StringBuilder();
            sb.Clear();
            switch (type)
            {
                case ResourceType.UI:
                    sb.Append("UI/");
                    break;
                case ResourceType.Role:
                    sb.Append("Role/");
                    break;
                case ResourceType.Pool:
                    sb.Append("Pool/");
                    break;
                case ResourceType.Audio:
                    sb.Append("Audio/");
                    break;
                case ResourceType.Game:
                    sb.Append("Game/");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }

            sb.Append(name);
            return sb.ToString();
        }

        public static void Clear()
        {
            Resources.UnloadUnusedAssets();
            _cacheDic.Clear();
        }
    }
}