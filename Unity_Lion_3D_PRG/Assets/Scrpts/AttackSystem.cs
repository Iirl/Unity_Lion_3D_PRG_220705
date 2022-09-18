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
        protected Animator ani;
        protected bool isAnimating;
        [SerializeField]
        protected string name;
        #endregion
        // 需要執行攻擊時執行此程式方法。
        public void StartAttack(int id) => StartCoroutine(AttackFlow(id));
        // 
        #region 攻擊判定
        private IEnumerator AttackFlow(int idx)
        {
            if (isAnimating) yield break;
            float attackDelay = 0;
            if (idx == 0) attackDelay = dataAtk.attackDelay;
            if (idx == 1) attackDelay = 0.0005f;
            isAnimating = true;
            yield return new WaitForSeconds(dataAtk.attackInterval);
            CheckAttackArea(idx);
            yield return new WaitForSeconds(attackDelay);
            isAnimating = false;
            StopAttack();
        }
        private void CheckAttackArea(int idx)
        {
            //print("檢查攻擊區域");
            if (AnimateAttack()) return;
            //if (!ani.GetCurrentAnimatorStateInfo(0).IsName(name)) return;
            bool isHit = false;
            Collider[] hits = Physics.OverlapBox(transform.position + transform.TransformDirection(dataAtk.attackOffset),
                dataAtk.attackArea / 2,
                transform.rotation,
                dataAtk.targetMask);
            if (hits.Length > 0)
            {
                isHit = true;
                DmgCheck(hits[0].gameObject, idx);
            }
            SoundAttack(isHit);
            //foreach (var e in hits) print(e.name);

        }
        protected virtual bool AnimateAttack() { return false; }
        protected virtual void SoundAttack(bool hit) { }
        protected virtual void StopAttack() { }
        #endregion        
        public void DmgCheck(GameObject target, int dmg = 0) => target.GetComponent<HealthSystem>().GetHurt(dataAtk.attack[dmg]);


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
