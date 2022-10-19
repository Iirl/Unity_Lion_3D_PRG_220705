using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace agi
{
    public class EnemySystem : MonoBehaviour
    {
        #region 資料
        EnemyAttack enemyAttack; 
        #endregion

        [SerializeField, Header("敵人資料")]
        private DataEnemy dataEnemy;
        //[SerializeField, Header("初始位置")]
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
        #region EnemyStateMachine
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
                //enemyState = GeneMotion.Running;
                enemyState.stateInt(1);
                //print($"Run {enemyState}");
            }

        }
        /// <summary>
        /// 漫遊狀態
        /// </summary>
        /// <returns></returns>
        private IEnumerator Wander()
        {
            MoveAnimateControl();
            //print("漫遊");
            nma.speed = dataEnemy.speedWalk;
            nma.SetDestination(target_v3);            
            while (nma.remainingDistance > 1) yield return null;
            if (nma.remainingDistance < 1) {
                MoveAnimateControl(0.1f);
                target_v3 = original + Random.insideUnitSphere * dataEnemy.rangeTrack;
                target_v3.y = transform.position.y;
                yield return new WaitForSeconds(0.5f);
                enemyState = enemyState.ToWait();

            }
        }

        /// <summary>
        /// 檢查物件程式：
        /// 只要在追蹤圈以內的物件出現了玩家，就會執行此程式。
        /// 如果碰到了玩家=進入追蹤狀態。
        /// 否則 = 回到移動(漫遊)狀態。
        /// </summary>
        private void CheckTargetTrackObject()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position + Vector3.up, dataEnemy.rangeTrack, dataEnemy.target_msak);
            if (hits.Length > 0)
            {
                //print($"碰到物件 {hits[0].name}");
                //transform.LookAt(hits[0].transform);
                target_v3 = hits[0].transform.position;
                if (enemyState == GeneMotion.isAtking) return;
                enemyState = enemyState.ToTrack();
            }
            else enemyState = enemyState.ToRun();

        }
        /// <summary>
        /// 追蹤狀態，此狀態會讓物件靠近玩家。
        /// </summary>
        private void Track()
        {
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("Attack")) nma.velocity = Vector3.zero;
            nma.SetDestination(target_v3);
            MoveAnimateControl(0.3f);
            ani.ResetTrigger("toAtk");
            if (Vector3.Distance(transform.position, target_v3) < dataEnemy.rangeAttack)
            {
                //print($"捕捉目標");
                enemyState = enemyState.ToAtk();
                ani.SetBool(GeneMotion.isAtking.ToString(),true);
            } else
            {
                ani.SetBool(GeneMotion.isAtking.ToString(), false);
                atkTime = dataEnemy.intervalAtkTime;
            }

        }
        /// <summary>
        /// 攻擊狀態，每攻擊一次就會回到追蹤狀態。
        /// 
        /// </summary>
        private void Attack()
        {
            //if (nma.remainingDistance > dataEnemy.rangeAttack) enemyState = StateMechine.ToTrack();
            nma.velocity = Vector3.zero;
            MoveAnimateControl(0);
            if (atkTime >= dataEnemy.intervalAtkTime) { 
                ani.SetTrigger("toAtk");
                enemyAttack.StartAttack(0);
                enemyState = enemyState.ToTrack();
                atkTime = 0;
            } else
            {
                atkTime += Time.deltaTime;
            }
        }
        #endregion
        /// <summary>
        /// 設定移動動畫狀態。
        /// </summary>
        /// <param name="f">大於0.5為跑步</param>
        private void MoveAnimateControl(float f=1) => ani.SetFloat(GeneMotion.Running.ToString(), f);

        #region 事件區域
        private void Awake()
        {
            enemyAttack = transform.GetChild(1).GetComponent<EnemyAttack>();
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
        private void OnDisable()
        {
            //nma.Stop();  //盡量不要用過時的方法，更新上容易出錯。
            nma.isStopped = true;
        }
        private void OnDrawGizmosSelected()
        {
            // 設定攻擊範圍
            //Gizmos.color = new Color(1, 0, 0, 0.15f);
            Gizmos.color = dataEnemy.attackRangeColor;
            Gizmos.DrawSphere(transform.position + Vector3.up, dataEnemy.rangeAttack);

            // 設定追蹤範圍
            Gizmos.color = dataEnemy.traceRangeColor;
            Gizmos.DrawSphere(transform.position + Vector3.up, dataEnemy.rangeTrack);

            // 設定追蹤目標
            Gizmos.color = new Color(1, 0, 0, 1f);
            Gizmos.DrawSphere(target_v3, 0.3f);
        }
        #endregion


    }
}