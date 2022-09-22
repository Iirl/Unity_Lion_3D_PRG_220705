using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace agi
{
    /// <summary>
    /// ��q�t��: Enmey
    /// </summary>
    public class EnemyHealthSystem : HealthSystem
    {
        EnemySystem es;
        private Material matDissolve;
        private string nameDissolve = "dpValue";
        EnemyObjectPool obPS;
        GameObject drop;

        protected override void Awake()
        {
            base.Awake();
            attackSystem = transform.GetChild(1).GetComponent<AttackSystem>();
            es = GetComponent<EnemySystem>();
            matDissolve = GetComponentsInChildren<Renderer>()[1].material;
            matDissolve.SetFloat(nameDissolve, 1);
            obPS = FindObjectOfType<EnemyObjectPool>();
        }

        /// <summary>
        /// �B�z���`�ĪG
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
        /// �������]�w
        /// </summary>
        private void Drop()
        {
            float rand = Random.value;
            if ( rand <= dataHealth.dropProb)
            {
                int item = Random.Range(0,dataHealth.dropItem.Count);/*
                GameObject drop= Instantiate(dataHealth.dropItem[item],
                                    transform.position + Vector3.up * 3,
                                    Random.rotation);
                Destroy(drop, 60);//*/
                drop = obPS.SpwanObj(dataHealth.dropItem[item]);
                //GameObject drop = obPS.SpwanObj(); 
                drop.transform.position = transform.position + Random.insideUnitSphere + Vector3.up;
                //drop.GetComponent<ObjectPoolSystem>().RelaseObj 
            }
        }
        /// <summary>
        /// ���ѵ{��
        /// </summary>
        /// <param name="min">��J�̤p��</param>
        /// <param name="max">��J�̤j��</param>
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
        }
    }
}
