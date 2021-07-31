using UnityEngine;

namespace CFramework
{
    public class SearchNode : ActionNode
    {
        [SerializeField] private float range = 5f;
        [SerializeField] private float angle = 30f;


        private RoleCtrl _curRoleCtrl;

        protected override void OnStart()
        {
            _curRoleCtrl = entity.GetComponent<RoleCtrl>();
        }

        protected override NodeState OnUpdate()
        {
            if (_curRoleCtrl.isAttack)
                return NodeState.Failure;
            
            FindTarget();
            if (_curRoleCtrl.target != null)
            {
                return NodeState.Success;
            }

            return NodeState.Running;
        }

        protected override void OnStop()
        {
            entity.GetComponent<RoleCtrl>().direction = Vector3.zero;
        }

        bool CanSee(Collider c)
        {
            Vector3 offset = entity.GetComponent<CharacterController>().center;
            Ray detectRay = new Ray(entity.transform.position + offset, entity.transform.forward * range);
            RaycastHit hitInfo;
            if (Physics.Raycast(detectRay, out hitInfo, range))
            {
                if (hitInfo.collider == c)
                {
                    return true;
                }
            }

            return false;
        }

        bool IsInMyVision(Collider c)
        {
            GameObject target = c.gameObject;
            //计算与目标距离
            float distance = Vector3.Distance(entity.transform.position, target.transform.position);

            Vector3 mVec = entity.transform.rotation * Vector3.forward; //当前朝向
            Vector3 tVec = target.transform.position - entity.transform.position; //与目标连线的向量

            //计算两个向量间的夹角
            float angle = Mathf.Acos(Vector3.Dot(mVec.normalized, tVec.normalized)) * Mathf.Rad2Deg;
            if (distance < range)
            {
                if (angle <= this.angle)
                {
                    /*if (CanSee(c))
                        return true;*/
                    return true;
                }
            }

            return false;
        }

        void FindTarget()
        {
            Collider[] allObejects = Physics.OverlapSphere(entity.transform.position, range, LayerMask.GetMask("Role"));
            foreach (Collider c in allObejects)
            {
                if (IsInMyVision(c))
                {
                    Debug.Log("I see a target");
                    _curRoleCtrl.target = c.gameObject;
                }
            }
        }
    }
}