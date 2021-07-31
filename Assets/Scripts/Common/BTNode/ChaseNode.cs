using Pathfinding;
using UnityEngine;

namespace CFramework
{
    public class ChaseNode : ActionNode
    {
        [SerializeField] private float range = 2;

        private Seeker _seeker;
        private RoleCtrl _curRoleCtrl;
        private GameObject _target;
        private Path _path;
        private int _curWayPoint;
        Vector3 dir = Vector3.zero;

        protected override void OnStart()
        {
            _seeker = entity.GetComponent<Seeker>();
            _curRoleCtrl = entity.GetComponent<RoleCtrl>();

            _target = _curRoleCtrl.target;
            _curWayPoint = 0;
            dir = Vector3.zero;
            _path = null;
            _seeker.pathCallback += OnPathComplete;


            Debug.Log("开始追逐");
        }

        protected override NodeState OnUpdate()
        {
            if (_target == null)
            {
                Debug.Log("无目标");
                return NodeState.Failure;
            }

            //比较当前距离，是否需要寻路
            if (Vector3.Distance(_target.transform.position, entity.transform.position) < range)
            {
                Debug.Log("不需要寻路");
                return NodeState.Success;
            }

            _seeker.StartPath(entity.transform.position, _target.transform.position);

            if (_path == null)
            {
                Debug.Log("搜索路径中");
                return NodeState.Running;
            }
            
            if (_curWayPoint>=_path.vectorPath.Count)
            {
                Debug.Log("路径搜索结束");
                return NodeState.Success;
            }
            
            dir = (_path.vectorPath[_curWayPoint + 1] - entity.transform.position).normalized;
            _curRoleCtrl.direction = dir;
            _curRoleCtrl.curRoleFSM.ChangeState(RoleState.Run, 2);
            
            if (Vector3.Distance(entity.transform.position, _path.vectorPath[_curWayPoint]) < range)
            {
                _curWayPoint++;
            }
            return NodeState.Running;
        }
        

        protected override void OnStop()
        {
            _curRoleCtrl.direction = Vector3.zero;
            _path = null;
            _seeker.pathCallback -= OnPathComplete;
            //_curRoleCtrl.curRoleFSM.ChangeState(RoleState.Idle, 0);
            Debug.Log("追逐结束");
        }

        private void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                _path = p;
                _curWayPoint = 0;
            }
        }
    }
}