namespace CFramework
{
    using UnityEngine;

    /// <summary>
    /// 倒计时器。
    /// </summary>
    public sealed class CountDownTimer
    {
        public bool IsAutoCycle { get; private set; } // 是否自动循环（小于等于0后重置）
        public bool IsStopped { get; private set; } // 是否暂停了

        public float CurrentTime => UpdateCurrentTime(); // 当前时间

        public bool IsTimeUp =>  CurrentTime <= 0; // 是否时间到

        public float Duration { get; private set; } // 计时时间长度

        private float _lastTime; // 上一次更新的时间
        private int _lastUpdateFrame; // 上一次更新倒计时的帧数（避免一帧多次更新计时）
        private float _currentTime; // 当前计时器剩余时间

        public CountDownTimer() : this(0f, false, false)
        {
        }

        /// <summary>
        /// 构造倒计时器
        /// </summary>
        /// <param name="duration">起始时间</param>
        /// <param name="autoCycle">是否自动循环</param>
        public CountDownTimer(float duration, bool autoCycle = false, bool autoStart = true)
        {
            IsStopped = true;
            Duration = Mathf.Max(0f, duration);
            IsAutoCycle = autoCycle;
            Reset(duration, !autoStart);
        }

        /// <summary>
        /// 更新计时器时间
        /// </summary>
        /// <returns>返回剩余时间</returns>
        private float UpdateCurrentTime()
        {
            if (IsStopped || _lastUpdateFrame == Time.frameCount) // 暂停了或已经这一帧更新过了，直接返回
                return _currentTime;
            if (_currentTime <= 0) // 小于等于0直接返回，如果循环那就重置时间
            {
                if (IsAutoCycle)
                    Reset(Duration, false);
                return _currentTime;
            }

            _currentTime -= Time.time - _lastTime;
            UpdateLastTimeInfo();
            return _currentTime;
        }

        /// <summary>
        /// 更新时间标记信息
        /// </summary>
        private void UpdateLastTimeInfo()
        {
            _lastTime = Time.time;
            _lastUpdateFrame = Time.frameCount;
        }

        /// <summary>
        /// 重置计时器，并开始计时
        /// </summary>
        public void Start()
        {
            Reset(Duration, false);
        }

        /// <summary>
        /// 重置计时器
        /// </summary>
        /// <param name="duration">持续时间</param>
        /// <param name="isStoped">是否暂停</param>
        public void Reset(float duration, bool isStoped = false)
        {
            UpdateLastTimeInfo();
            Duration = Mathf.Max(0f, duration);
            _currentTime = Duration;
            IsStopped = isStoped;
        }

        /// <summary>
        /// 暂停
        /// </summary>
        public void Pause()
        {
            UpdateCurrentTime(); // 暂停前先更新一遍
            IsStopped = true;
        }

        /// <summary>
        /// 继续（取消暂停）
        /// </summary>
        public void Continue()
        {
            UpdateLastTimeInfo(); // 继续前先更新一般时间信息
            IsStopped = false;
        }

        /// <summary>
        /// 终止，暂停且设置当前值为0
        /// </summary>
        public void End()
        {
            IsStopped = true;
            _currentTime = 0f;
        }

        /// <summary>
        /// 获取倒计时完成率（0为没开始计时，1为计时结束）
        /// </summary>
        /// <returns></returns>
        public float GetPercent()
        {
            UpdateCurrentTime();
            return Mathf.InverseLerp(Duration, 0, _currentTime);
        }
    }
}