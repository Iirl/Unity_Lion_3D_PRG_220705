using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace agi
{
    /// <summary>
    /// ��q�t��
    /// </summary>
    public class HealthSystem : MonoBehaviour
    {
        [SerializeField, Header("��q���")]
        protected DataHealth dataHealth;
        [SerializeField, Header("��q")]
        protected Image imageHealth;
        [SerializeField, Header("�ʧ@")]
        protected Animator ani;
        protected AttackSystem attackSystem;
        private float hp;
        private string parHurt = Motion.toHurt.ToString();
        private string parDead = Motion.isDead.ToString();
        //
        public void GetHurt(float f) => Hurt(f);

        protected virtual void Awake()
        {
            attackSystem = GetComponent<AttackSystem>();
            hp = dataHealth.hp;

        }

        /// <summary>
        /// ����ˮ`
        /// </summary>
        /// <param name="point">�ˮ`�q</param>
        private void Hurt(float point)
        {
            hp -= point;
            //print($"HP{hp},DMG{point} , %{hp / dataHealth.maxHp}");
            ani.SetTrigger(parHurt);
            if (hp <= 0) Dead();
            imageHealth.fillAmount = hp / dataHealth.maxHp;
        }

        /// <summary>
        /// ���`����
        /// </summary>
        /// <param name="die">�O�_���`</param>
        protected virtual void Dead(bool die = true)
        {
            if (die) hp = 0;
            ani.SetBool(parDead,die);
            attackSystem.enabled = false;
        }
    }
}
