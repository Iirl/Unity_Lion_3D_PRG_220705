using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace agi
{
    [DefaultExecutionOrder(200)]
    public class SpawnSystem : MonoBehaviour
    {
        private ObjectPoolEnemy objecctSketon;
        private GameObject SketonObj;
        [SerializeField, Header("�H���ͦ��ɶ�")]
        private Vector2 rangeTime = new(2, 5); 

        private void Awake()
        {
            objecctSketon = GameObject.Find("�Ǫ�������t��").GetComponent<ObjectPoolEnemy>();
            Spawn();
        }

        private void Spawn()
        {
            SketonObj = objecctSketon.SpwanObj();
            SketonObj.transform.position = gameObject.transform.position;
            SketonObj.GetComponent<EnemyHealthSystem>().onDead = EnemyDead;
        }

        private void EnemyDead()
        {
            objecctSketon.RelaseObj(SketonObj);
            float rndTime = UnityEngine.Random.Range(rangeTime.x, rangeTime.y);
            Invoke("Spawn", rndTime);
        }
    }
}
