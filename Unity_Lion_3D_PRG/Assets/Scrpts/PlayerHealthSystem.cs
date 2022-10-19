using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace agi
{
    /// <summary>
    /// 血量系統: Plyaer
    /// </summary>
    public class PlayerHealthSystem : HealthSystem
    {
        ThirdPersonalController tpc;

        protected override void Awake()
        {
            base.Awake();
            tpc = GetComponent<ThirdPersonalController>();
        }

        protected override void Dead(bool die = true)
        {
            base.Dead(die);
            tpc.enabled = !die;
        }

        public override void GetHeal(float f)
        {
            base.GetHeal(f);
            tpc.PlayTrack(7); // 回復音效
        }
    }
}
