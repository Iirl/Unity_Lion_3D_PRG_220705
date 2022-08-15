using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace agi
{
    public class EnemySystem : MonoBehaviour
    {
        [SerializeField, Header("敵人資料")]
        private DataEnemy dataEnemy;
        private Vector3 original;
        [SerializeField]
        private GeneMotion enemyState;
        private Vector3 target_v3;

        NavMeshAgent nma;
        Animator ani;
        private float timeIdel;
        private float atkTime;

        private void StateSwitch()
        {
            switch (enemyState)
            {
                case GeneMotion.isIdel:
                    Idel();
                    break;
                case GeneMotion.Running:
                    StartCoroutine(Wander());
                    break;
                case GeneMotion.toTrack:
                    Track();
                    break;
                case GeneMotion.isAtking:
                    Attack();
                    break;
                default:
                    break;
            }
        }
        /// <summary>
        /// 停止狀態
        /// </summary>
        private void Idel()
        {
            nma.velocity = Vector3.zero;
            MoveAnimateControl(0);
            timeIdel += Time.deltaTime;
            float rnd = Random.Range(dataEnemy.waitTimeRange.x, dataEnemy.waitTimeRange.y);
            if (timeIdel > rnd)
            {
                timeIdel = 0;
                enemyState = StateMechine.ToRun();
            }

        }
        /// <summary>
        /// 漫遊狀態
        /// </summary>
        /// <returns></returns>
        private IEnumerator Wander()
        {
            MoveAnimateControl();
            nma.speed = dataEnemy.speedWalk;
            nma.SetDestination(target_v3);            
            while (nma.remainingDistance > 1) yield return null;
            if (nma.remainingDistance < 1) {
                MoveAnimateControl(0.1f);
                target_v3 = original + Random.insideUnitSphere * dataEnemy.rangeTrack;
                target_v3.y = transform.position.y;
                yield return new WaitForSeconds(0.5f);
                enemyState = StateMechine.ToWait();

            }
        }

        private void CheckTargetTrackObject()
        {
            if (enemyState == GeneMotion.isAtking) return;
            Collider[] hits = Physics.OverlapSphere(transform.position + Vector3.up, dataEnemy.rangeTrack, dataEnemy.target_msak);
            if (hits.Length > 0)
            {
                //print($"碰到物件 {hits[0].name}");
                //transform.LookAt(hits[0].transform);
                target_v3 = hits[0].transform.position;
                enemyState = StateMechine.ToTrack();
            }
        }

        private void Track()
        {
            nma.SetDestination(target_v3);
            MoveAnimateControl(0.3f);
            if (Vector3.Distance(transform.position, target_v3) < dataEnemy.rangeAttack)
            {
                //print("捕捉目標");
                enemyState = StateMechine.ToAtk();
                ani.SetBool(GeneMotion.isAtking.ToString(),true);
            } else
            {
                ani.SetBool(GeneMotion.isAtking.ToString(), false);
            }

        }

        private void Attack()
        {
            //if (nma.remainingDistance > dataEnemy.rangeAttack) enemyState = StateMechine.ToTrack();
            nma.velocity = Vector3.zero;

            if (atkTime > dataEnemy.intervalAtkTime) { 
                ani.SetTrigger("toAtk");
                atkTime = 0;
            } else
            {
                atkTime += Time.deltaTime;
            }
        }
        /// <summary>
        /// 移動動畫控制系統
        /// </summary>
        /// <param name="f"></param>
        private void MoveAnimateControl(float f=1)
        {
            ani.SetFloat(GeneMotion.Running.ToString(), f);

        }

        #region 事件區域
        #endregion
        private void Awake()
        {
            ani = GetComponent<Animator>();
            nma = GetComponent<NavMeshAgent>();
        }
        private void Start()
        {
            original = transform.position;
            target_v3 = original;
        }
        private void Update()
        {
            StateSwitch();
            if(!enemyState.Equals("isAtking")) CheckTargetTrackObject();
        }
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = new Color(1, 0, 0, 0.15f);
            Gizmos.DrawSphere(transform.position + Vector3.up, dataEnemy.rangeAttack);

            Gizmos.color = new Color(0, 1, 0, 0.25f);
            Gizmos.DrawSphere(transform.position + Vector3.up, dataEnemy.rangeTrack);

            Gizmos.color = new Color(1, 0, 0, 1f);
            Gizmos.DrawSphere(target_v3, 0.3f);
        }


    }
}