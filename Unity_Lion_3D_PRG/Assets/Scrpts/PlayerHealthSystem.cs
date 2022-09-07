using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace agi
{
    /// <summary>
    /// ��q�t��: Plyaer
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

    }
}
