using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace agi
{
    /// <summary>
    /// 血量系統
    /// </summary>
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField, Header("血量資料")]
        protected DataHealth dataHealth;
        [SerializeField, Header("血量")]
        protected Image imageHealth;
        [SerializeField, Header("動作")]
        protected Animator ani;
        protected AttackSystem attackSystem;
        protected float hp;
        private string parHurt = Motion.toHurt.ToString();
        private string parDead = Motion.isDead.ToString();
        //
        public void GetHurt(float f) => Hurt(f);
        public virtual void GetHeal(float f) => Heal(f);

        protected virtual void Awake()
        {
            attackSystem = GetComponent<AttackSystem>();
            hp = dataHealth.hp;

        }

        /// <summary>
        /// 受到傷害
        /// </summary>
        /// <param name="point">傷害量</param>
        private void Hurt(float point)
        {
            hp -= point;
            //print($"HP{hp},DMG{point} , %{hp / dataHealth.maxHp}");
            ani.SetTrigger(parHurt);
            //if(!ani.GetBool("isDead")) 
                if (hp <= 0) Dead();
            imageHealth.fillAmount = hp / dataHealth.maxHp;
        }
        /// <summary>
        /// HP恢復
        /// </summary>
        /// <param name="point">恢復量</param>
        private void Heal(float point)
        {
            hp += point;
            if (hp >= dataHealth.maxHp) hp = dataHealth.maxHp;
            imageHealth.fillAmount = hp / dataHealth.maxHp;
        }

        /// <summary>
        /// 死亡切換
        /// </summary>
        /// <param name="die">是否死亡</param>
        protected virtual void Dead(bool die = true)
        {
            if (die) hp = 0;
            ani.SetBool(parDead,die);
            attackSystem.enabled = false;
        }
    }
}
