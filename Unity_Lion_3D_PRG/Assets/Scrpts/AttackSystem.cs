using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace agi
{
    /// <summary>
    /// 基礎攻擊系統：
    /// 玩家與敵人會共用此系統，此系統僅提供基本的範圍判定、偵測與碰撞。
    /// </summary>
    public class AttackSystem : MonoBehaviour
    {
        #region 欄位
        [SerializeField]
        DataAtk dataAtk;
        protected bool isAnimating;
        #endregion
        // 需要執行攻擊時執行此程式方法。
        public void StartAttack() =>  StartCoroutine(AttackFlow());
        // 
        #region 攻擊判定
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
            //print("檢查攻擊區域");
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
