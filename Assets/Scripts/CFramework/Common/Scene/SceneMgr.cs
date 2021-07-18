using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace CFramework
{
    public class SceneMgr : Singleton<SceneMgr>
    {
        private float _curProgress;
        private bool _isRunning = false;

        //同步切换场景
        public void LoadScene(string name, UnityAction func)
        {
            SceneManager.LoadScene(name);
            func();
        }

        //异步切换场景
        public void LoadSceneAsync(string name, UnityAction func)
        {
            if (!_isRunning)
            {
                _curProgress = 0;
                GameManager.Instance.StartCoroutine(CoLoadSceneAsync(name, func));
            }
        }

        //协程异步加载场景,达到更新进度条的效果
        private IEnumerator CoLoadSceneAsync(string name, UnityAction func)
        {
            AsyncOperation async = SceneManager.LoadSceneAsync(name);
            async.allowSceneActivation = false;

            #region 优化进度的
            //临时的进度
            float tmp;
            while (async.progress < 0.9f)
            {
                //相当于滑动条应该到的位置
                tmp = async.progress;

                //当滑动条 < tmp 就意味着滑动条应该变化
                while (_curProgress < tmp)
                {
                    Debug.Log("12341");
                    //事件中心向外分发进度情况，ui系统接收，更新进度条
                    EventCenter.Instance.EventTrigger<float>("Loading", _curProgress);
                    _curProgress += 0.01f;
                    yield return new WaitForEndOfFrame();
                }
            }//进度为0.9
            
            //处理进度0.9~1
            tmp = 1f;
            while (_curProgress < tmp)
            {
                
                EventCenter.Instance.EventTrigger<float>("Loading", _curProgress);
                _curProgress += 0.01f;
                yield return new WaitForEndOfFrame();
            }
            #endregion

            //加载完成后才会运行func
            func?.Invoke();
            
            //进度条完成 允许显示
            async.allowSceneActivation = true;

            _isRunning = true;
        }
    }
}