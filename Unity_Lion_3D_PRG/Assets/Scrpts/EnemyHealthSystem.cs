using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace agi
{
    /// <summary>
    /// 血量系統: Enmey
    /// </summary>
    public class EnemyHealthSystem : HealthSystem
    {
        EnemySystem es;
        private Material matDissolve;
        private string nameDissolve = "dpValue";
        EnemyObjectPool dropItemSP;
        GameObject drop;
        //
        int probCount = 0;

        protected override void Awake()
        {
            base.Awake();
            attackSystem = transform.GetChild(1).GetComponent<AttackSystem>();
            es = GetComponent<EnemySystem>();
            matDissolve = GetComponentsInChildren<Renderer>()[1].material;
            matDissolve.SetFloat(nameDissolve, 1);
            dropItemSP = GameObject.Find("掉落物件池系統").GetComponent<EnemyObjectPool>();
        }
        /// <summary>
        /// 死亡時會執行一次
        /// </summary>
        private void OnDisable()
        {
            hp = dataHealth.hp;
            imageHealth.fillAmount = 1;
            imageHealth.transform.parent.gameObject.SetActive(true);
            es.enabled = true;
            matDissolve.SetFloat(nameDissolve, 1);
        }

        public delegate void delegateDead();
        public delegateDead onDead;
        /// <summary>
        /// 處理死亡效果
        /// </summary>
        /// <param name="die"></param>
        protected override void Dead(bool die = true)
        {
            base.Dead(die);
            es.enabled = !die;
            imageHealth.transform.parent.gameObject.SetActive(!die);
            //GetComponent<CapsuleCollider>().isTrigger = die;
            Drop();
            StartCoroutine(Dissolve(-3,1));
        }

        /// <summary>
        /// 掉落物設定
        /// </summary>
        private void Drop()
        {
            float rand = probCount > 5 ? probCount = 0: Random.value;

            if ( rand <= dataHealth.dropProb)
            {
                //print(dataHealth.dropItem.Count);
                int item = Random.Range(0,dataHealth.dropItem.Count);
                drop = dropItemSP.SpwanObj(dataHealth.dropItem[item]);
                drop.transform.position = transform.position + Random.insideUnitSphere + Vector3.up;

            }
            probCount++;            
        }
        /// <summary>
        /// 溶解程序
        /// </summary>
        /// <param name="min">輸入最小值</param>
        /// <param name="max">輸入最大值</param>
        /// <returns></returns>
        private IEnumerator Dissolve(float min, float max)
        {
            float value = max;
            matDissolve.SetFloat(nameDissolve, value);
            while (value > min)
            {
                value -= 0.1f;
                matDissolve.SetFloat(nameDissolve, value);
                yield return null;
            }
            // 在 SpawnSystem 中呼叫，當怪物死亡時會透過生成系統回收
            onDead();
        }
    }
}
