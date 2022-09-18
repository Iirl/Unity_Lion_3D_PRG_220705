using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace agi
{
    public class EnemyAttack : AttackSystem
    {
        [SerializeField]
        Animator mainAnimator;
        protected override void Awake()
        {
            base.Awake();
            ani = mainAnimator;
        }

        protected override bool AnimateAttack()
        {
            if (!ani.GetCurrentAnimatorStateInfo(0).IsName(name)) return true;
            else return false;
        }
    }
}
