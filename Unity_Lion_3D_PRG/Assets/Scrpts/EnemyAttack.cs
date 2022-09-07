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
    }
}
