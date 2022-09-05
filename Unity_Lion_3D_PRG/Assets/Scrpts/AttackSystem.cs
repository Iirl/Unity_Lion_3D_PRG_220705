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
        protected bool isAnimating;
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
            Collider[] hits = Physics.OverlapBox(transform.position + transform.TransformDirection(dataAtk.attackOffset),
                dataAtk.attackArea / 2,
                transform.rotation,
                dataAtk.targetMask);
            if (hits.Length > 0) print(hits[0].name);
            //foreach (var e in hits) print(e.name);

        }
        protected virtual void StopAttack() { }
        #endregion        
        private void OnDrawGizmos()
        {
            Gizmos.color = dataAtk.attackRangeColor;
            Gizmos.matrix = Matrix4x4.TRS(transform.position + transform.TransformDirection(dataAtk.attackOffset),
                transform.rotation, transform.localScale);
            Gizmos.DrawCube(Vector3.zero, dataAtk.attackArea);
        }
    }
}
