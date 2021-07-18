using System.Collections.Generic;
using UnityEngine;

namespace CFramework
{
    public class NumericComponent : MonoBehaviour
    {
        private Dictionary<int, long> NumericDic = new Dictionary<int, long>();

        public void Initialize(NumericType numericType)
        {
            // 这里通过value，初始化value所需要的值的字典
            if (numericType > NumericType.Max)
            {
                Debug.Log("此接口用于初始化value对应计算属性的字典，具体值修改请使用Set");
                return;
            }
            int type = (int) numericType;
            int bas = type * 10 + 1;
            int add = type * 10 + 2;
            int pct = type * 10 + 3;
            int finalAdd = type * 10 + 4;
            int finalPct = type * 10 + 5;
            
            NumericDic[type] = 0;
            NumericDic[bas] = 0;
            NumericDic[add] = 0;
            NumericDic[pct] = 0;
            NumericDic[finalAdd] = 0;
            NumericDic[finalPct] = 0;
        }

        public float GetAsFloat(NumericType numericType)
        {
            return (float) GetByKey((int) numericType) / 10000;
        }

        public float GetAsFloat(int numericType)
        {
            return (float) GetByKey(numericType) / 10000;
        }

        public int GetAsInt(NumericType numericType)
        {
            return (int) GetByKey((int) numericType);
        }

        public long GetAsLong(NumericType numericType)
        {
            return GetByKey((int) numericType);
        }

        public int GetAsInt(int numericType)
        {
            return (int) GetByKey(numericType);
        }

        public long GetAsLong(int numericType)
        {
            return GetByKey(numericType);
        }

        public void Set(NumericType nt, float value)
        {
            this[nt] = (long) (value * 10000);
        }

        public void Set(NumericType nt, int value)
        {
            this[nt] = value;
        }

        public void Set(NumericType nt, long value)
        {
            this[nt] = value;
        }

        public long this[NumericType numericType]
        {
            get { return this.GetByKey((int) numericType); }
            set
            {
                long v = this.GetByKey((int) numericType);
                if (v == value)
                {
                    return;
                }

                NumericDic[(int) numericType] = value;

                OnUpdate(numericType);
            }
        }

        private long GetByKey(int key)
        {
            long value = 0;
            this.NumericDic.TryGetValue(key, out value);
            return value;
        }

        private void OnUpdate(NumericType numericType)
        {
            
            if (numericType < NumericType.Max)
            {
                Debug.Log("请不要直接修改value");
                return;
            }

            int final = (int) numericType / 10;
            int bas = final * 10 + 1;
            int add = final * 10 + 2;
            int pct = final * 10 + 3;
            int finalAdd = final * 10 + 4;
            int finalPct = final * 10 + 5;

            // 一个数值可能会多种情况影响，比如速度,加个buff可能增加速度绝对值100，也有些buff增加10%速度，所以一个值可以由5个值进行控制其最终结果
            // final = (((base + add) * (100 + pct) / 100) + finalAdd) * (100 + finalPct) / 100;
            long old = this.NumericDic[final];
            long result =
                (long) (((this.GetAsLong(bas) + this.GetAsLong(add)) * (100 + this.GetAsFloat(pct)) / 100f +
                         this.GetAsLong(finalAdd)) * (100 + this.GetAsFloat(finalPct)) / 100f);
            this.NumericDic[final] = result;
            EventCenter.Instance.EventTrigger<NumericChange>(CEventType.NumericChange, new NumericChange()
            {
                Entity = gameObject,
                NumericType = (NumericType) final,
                Old = old,
                New = result
            });
        }
    }
}