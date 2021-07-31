using UnityEngine;
using UnityEngine.Pool;

namespace CFramework
{
    public class GameObjectPool
    {
        private string _resName;
        public ObjectPool<GameObject> Pool;

        private ResourceType _resType;

        public GameObjectPool(string resName,ResourceType resType, int defaultCapacity = 1, int maxSize = 1)
        {
            _resName = resName;
            _resType = resType;
            Pool = new ObjectPool<GameObject>(FuncGameObjectOnCreate, ActionGameObjectOnGet, ActionGameObjectOnRelease, ActionGameObjectOnDestroy, false,defaultCapacity, maxSize);
        }
        GameObject FuncGameObjectOnCreate()
        {
            //resName
            GameObject goPrefab = ResourceLoader.Load<GameObject>(_resType, _resName);

            GameObject go = GameObject.Instantiate(goPrefab);
            go.name = "Pooled " + _resName;
            //go.hideFlags = HideFlags.HideInHierarchy;
            
            return go;
        }

        void ActionGameObjectOnGet(GameObject go)
        {
            go.SetActive(true);
            /*if (go.GetComponent<ParticleSystem>() != null)
            {
                go.GetComponent<ParticleSystem>().Play();
            }*/
        }

        void ActionGameObjectOnRelease(GameObject go)
        {
            go.SetActive(false);
            /*if (go.GetComponent<ParticleSystem>() != null)
            {
                go.GetComponent<ParticleSystem>().Pause();
            }*/
        }

        void ActionGameObjectOnDestroy(GameObject go)
        {
            GameObject.Destroy(go);
        }

        public void Clear()
        {
            Pool.Clear();
        }
    }
}