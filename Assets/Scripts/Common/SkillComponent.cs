using UnityEngine;

namespace CFramework
{
    public class SkillComponent : MonoBehaviour
    {
        protected GameObject Find(float range)
        {
            //正在搜索的半径
            int radius = 1;
            //一步一步扩大搜索半径,最大扩大到100
            while (radius < range)
            {
                //球形射线检测,得到半径radius米范围内所有的物件
                Collider[] cols = Physics.OverlapSphere(transform.position, radius);
                //判断检测到的物件中有没有Enemy
                foreach (var t in cols)
                    if (t.tag.Equals("Enemy"))
                        return t.gameObject;

                radius += 2;
            }

            return null;
        }

        private void AdjustCameraPosition(GameObject target)
        {
            //施放者和目标的中点
            Vector3 centerPoint = (target.transform.position - transform.position) / 2;
        }
    }
}