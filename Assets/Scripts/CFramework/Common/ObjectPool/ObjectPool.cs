// Decompiled with JetBrains decompiler
// Type: UnityEngine.Pool.ObjectPool`1
// Assembly: UnityEngine.CoreModule, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: 531E906A-5D94-47DC-A62C-95AB61FF1C26
// Assembly location: E:\Unity\.UnityEditor\2021.1.6f1c1\Editor\Data\Managed\UnityEngine\UnityEngine.CoreModule.dll

using System;
using System.Collections.Generic;

namespace UnityEngine.Pool
{
    /// <summary>
    /// 继承了对象池接口，外部调用对象池操作使用Get和Release。
    /// Get--(if pooledobject is empty do m_CreateFunc)-->m_ActionOnGet
    /// Release-->actionOnRelease-->(if max capacity is not enough do actionOnDestroy)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ObjectPool<T> : IDisposable, IObjectPool<T> where T : class
    {
        internal readonly Stack<T> m_Stack;//用栈维护对象池
        private readonly Func<T> m_CreateFunc;//对象实例化的调用方法
        private readonly Action<T> m_ActionOnGet;//获取对象池对象的调用方法
        private readonly Action<T> m_ActionOnRelease;//释放对象池对象的调用方法
        private readonly Action<T> m_ActionOnDestroy;//销毁对象池对象的调用方法
        private readonly int m_MaxSize;//对象池最大容量
        internal bool m_CollectionCheck;//检查对象池中的对象是否存在

        public int CountAll { get; private set; }//对象池当前的容量

        public int CountActive => this.CountAll - this.CountInactive;//对象池中当前使用的容量

        public int CountInactive => this.m_Stack.Count;//对象池当前未使用的对象数
        
        //构造函数初始化上述值
        public ObjectPool(
            Func<T> createFunc,
            Action<T> actionOnGet = null,
            Action<T> actionOnRelease = null,
            Action<T> actionOnDestroy = null,
            bool collectionCheck = true,
            int defaultCapacity = 10,
            int maxSize = 10000)
        {
            if (createFunc == null)
                throw new ArgumentNullException(nameof(createFunc));
            if (maxSize <= 0)
                throw new ArgumentException("Max Size must be greater than 0", nameof(maxSize));
            this.m_Stack = new Stack<T>(defaultCapacity);
            this.m_CreateFunc = createFunc;
            this.m_MaxSize = maxSize;
            this.m_ActionOnGet = actionOnGet;
            this.m_ActionOnRelease = actionOnRelease;
            this.m_ActionOnDestroy = actionOnDestroy;
            this.m_CollectionCheck = collectionCheck;
        }
        
        //重要：获取对象池对象，调用获取方法。如果对象不够调用创建函数创建再执行上述步骤。
        public T Get()
        {
            T obj;
            if (this.m_Stack.Count == 0)
            {
                obj = this.m_CreateFunc();
                ++this.CountAll;
            }
            else
                obj = this.m_Stack.Pop();

            Action<T> actionOnGet = this.m_ActionOnGet;
            if (actionOnGet != null)
                actionOnGet(obj);
            return obj;
        }
        
        //用此方法可以获得对象池对象和PooledObject
        public PooledObject<T> Get(out T v) => new PooledObject<T>(v = this.Get(), (IObjectPool<T>) this);

        public void Release(T element)
        {
            if (this.m_CollectionCheck && this.m_Stack.Count > 0 && this.m_Stack.Contains(element))
                throw new InvalidOperationException("Trying to release an object that has already been released to the pool.");
            Action<T> actionOnRelease = this.m_ActionOnRelease;
            if (actionOnRelease != null)
                actionOnRelease(element);
            
            //对象池中的小于设定的最大容量则推入，用此维持对象池数量于控制水位线
            if (this.CountInactive < this.m_MaxSize)
            {
                this.m_Stack.Push(element);
            }
            else
            {
                Action<T> actionOnDestroy = this.m_ActionOnDestroy;
                if (actionOnDestroy != null)
                    actionOnDestroy(element);
            }
        }

        public void Clear()
        {
            if (this.m_ActionOnDestroy != null)
            {
                foreach (T obj in this.m_Stack)
                    this.m_ActionOnDestroy(obj);
            }

            this.m_Stack.Clear();
            this.CountAll = 0;
        }

        public void Dispose() => this.Clear();
    }
}
