using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace agi
{
    /// <summary>
    /// ��¦�����t�ΡG
    /// ���a�P�ĤH�|�@�Φ��t�ΡA���t�ζȴ��Ѱ򥻪��d��P�w�B�����P�I���C
    /// </summary>
    public class AttackSystem : MonoBehaviour
    {
        #region ���
        [SerializeField]
        DataAtk dataAtk;
        protected Animator ani;
        protected bool isAnimating;
        [SerializeField]
        protected string name;

        #endregion
        // �ݭn��������ɰ��榹�{����k�C
        public void StartAttack() =>  StartCoroutine(AttackFlow());
        // 
        #region �����P�w
        private IEnumerator AttackFlow()
        {
            if (isAnimating) yield break;
            isAnimating = true;
            yield return new WaitForSeconds(dataAtk.attackInterval);
            CheckAttackArea();
            yield return new WaitForSeconds(dataAtk.attackDelay);
            isAnimating = false;
            StopAttack();
        }
        private void CheckAttackArea()
        {
            //print("�ˬd�����ϰ�");
            //if (!ani.GetCurrentAnimatorStateInfo(0).IsName(name)) return;
            Collider[] hits = Physics.OverlapBox(transform.position + transform.TransformDirection(dataAtk.attackOffset),
                dataAtk.attackArea / 2,
                transform.rotation,
                dataAtk.targetMask);

            if (hits.Length > 0)
            {
                hits[0].GetComponent<HealthSystem>().GetHurt(dataAtk.attack);
            }
            //foreach (var e in hits) print(e.name);

        }
        protected virtual void StopAttack() { }
        #endregion        


        protected virtual void Awake()
        {
            ani = GetComponent<Animator>();
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = dataAtk.attackRangeColor;
            Gizmos.matrix = Matrix4x4.TRS(transform.position + transform.TransformDirection(dataAtk.attackOffset),
                transform.rotation, transform.localScale);
            Gizmos.DrawCube(Vector3.zero, dataAtk.attackArea);
        }
        
    }
}
