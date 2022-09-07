using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace agi
{
    /// <summary>
    /// ¦å¶q¨t²Î: Enmey
    /// </summary>
    public class EnemyHealthSystem : HealthSystem
    {
        EnemySystem es;

        protected override void Awake()
        {
            base.Awake();
            attackSystem = transform.GetChild(1).GetComponent<AttackSystem>();
            es = GetComponent<EnemySystem>();
        }

        protected override void Dead(bool die = true)
        {
            base.Dead(die);
            es.enabled = !die;
            Drop();
        }

        private void Drop()
        {
            float rand = Random.value;
            print(rand);
            if ( rand <= dataHealth.dropProb)
            {
                int item = Random.Range(0,dataHealth.dropItem.Length);
                GameObject drop= Instantiate(dataHealth.dropItem[item],
                                    transform.position + Vector3.up * 3,
                                    Random.rotation);
                Destroy(drop, 60);
            }
        }
    }
}
