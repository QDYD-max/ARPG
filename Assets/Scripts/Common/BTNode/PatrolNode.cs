using System.Collections.Generic;
using Pathfinding;
using UnityEngine;

namespace CFramework
{
    public class PatrolNode : ActionNode
    {
        private List<Vector3> _patrolPath;
        private int _curTargetIndex;
        private RoleCtrl _curRoleCtrl;

        private Seeker _seeker;

        //判断玩家与航点的距离
        private const float NextWaypointDistance = 1f;

        //对当前的航点进行编号
        private int _curWayPoint = 0;

        //存储路径
        private Path _path;
        private bool _isPathFound = false;

        protected override void OnStart()
        {
            _patrolPath = entity.GetComponent<RoleMonsterAI>().PatrolPath;
            _curRoleCtrl = entity.GetComponent<RoleCtrl>();
            _seeker = entity.GetComponent<Seeker>();
            if (_patrolPath.Count == 0)
            {
                Debug.Log("无巡逻点配置");
            }
            else
            {
                _curTargetIndex = 0;
                _path = _seeker.StartPath(entity.transform.position, _patrolPath[_curTargetIndex],OnPathComplete);
            }
        }

        protected override NodeState OnUpdate()
        {
            if (_path == null)
            {
                Debug.Log("无路径配置");
            }
            
            Debug.Log("巡逻中: " + _curTargetIndex);
            UpdatePatrolPath();
            if (_isPathFound)
            {
                Vector3 dir = (_path.vectorPath[_curWayPoint + 1] - entity.transform.position).normalized;
                _curRoleCtrl.direction = dir;
                _curRoleCtrl.curRoleFSM.ChangeState(RoleState.Run, 1);
            }
            
            return NodeState.Running;
        }

        protected override void OnStop()
        {
        }

        private void UpdatePatrolPath()
        {
            
            if(_patrolPath.Count == 0) return;
            //因为与目标点重叠的时候没有路径，所以先做距离判断，再做路径判断
            if (Vector3.Distance(_patrolPath[_curTargetIndex], entity.transform.position) < NextWaypointDistance ||
                Vector3.Distance(entity.transform.position, _path.vectorPath[_curWayPoint]) <
                NextWaypointDistance)
            {
                _curWayPoint++;
                if (_curWayPoint >= _path.vectorPath.Count - 1)
                {
                    Debug.Log("到达目标点: " + _curTargetIndex);
                    _curTargetIndex++;
                    _curTargetIndex %= _patrolPath.Count;
                    
                    _isPathFound = false;
                    
                    _seeker.StartPath(entity.transform.position, _patrolPath[_curTargetIndex],OnPathComplete);
                }
            }
        }
        
        private void OnPathComplete(Path p)
        {
            if (!p.error)
            {
                _path = p;
                _isPathFound = true;
                _curWayPoint = 0;
            }
        }
    }
}