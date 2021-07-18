using System;

namespace UnityEngine.Pool
{
    //Unity2021 版本封装的对象池，直接抄过来。是在2020及之前版本的UnityRendering下的ObjectPool的拓展
    
    /// <summary>
    /// 继承了IDisposable的数据对象，可以由Unity回收的类型，资源不能被Unity CLR回收时使用。官方没有给出具体的例子。
    /// </summary>
    /// <typeparam name="T">对象的类型</typeparam>
    public struct PooledObject<T> : IDisposable where T : class
    {
        private readonly T m_ToReturn;
        private readonly IObjectPool<T> m_Pool;

        internal PooledObject(T value, IObjectPool<T> pool)
        {
            this.m_ToReturn = value;
            this.m_Pool = pool;
        }

        void IDisposable.Dispose() => this.m_Pool.Release(this.m_ToReturn);
    }
    
    /// <summary>
    /// 对象池定义的接口
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IObjectPool<T> where T : class
    {
        int CountInactive { get; }

        T Get();

        PooledObject<T> Get(out T v);

        void Release(T element);

        void Clear();
    }
}