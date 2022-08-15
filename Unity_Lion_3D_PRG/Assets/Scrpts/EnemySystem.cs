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
        [SerializeField]
        GeneMotion enemyState;

        Animator ani;
        NavMeshAgent nma;

        private Vector3 original;
        private Vector3 target_v3;
        private float timeIdel;

        private void StateSwitch()
        {
            switch (enemyState)
            {
                case GeneMotion.None:
                    Idel();
                    break;
                case GeneMotion.toHurt:
                    break;
                case GeneMotion.isDead:
                    break;
                case GeneMotion.Running:
                    StartCoroutine(Wander());
                    break;
                case GeneMotion.toJump:
                    break;
                default:
                    break;
            }
        }

        private void Idel()
        {
            nma.velocity = Vector3.zero;
            AnimateControl(0);
            timeIdel += Time.deltaTime;
            float rnd = Random.Range(dataEnemy.waitTimeRange.x, dataEnemy.waitTimeRange.y);
            if (timeIdel > rnd)
            {
                timeIdel = 0;
                enemyState = StateMechine.ToRun();
            }

        }
        private IEnumerator Wander()
        {
            AnimateControl();
            nma.speed = dataEnemy.speedWalk;
            nma.SetDestination(target_v3);            
            while (nma.remainingDistance > 1) yield return null;
            if (nma.remainingDistance < 1) {
                AnimateControl(0.1f);
                target_v3 = original + Random.insideUnitSphere * dataEnemy.rangeTrack;
                target_v3.y = transform.position.y;
                yield return new WaitForSeconds(0.5f);
                enemyState = StateMechine.ToWait();

            }
        }
        private void AnimateControl(float f=1)
        {
            switch (enemyState)
            {
                case GeneMotion.Running:
                    ani.SetFloat(enemyState.ToString(), f);
                    break;
                default:
                    ani.SetFloat(GeneMotion.Running.ToString(), 0);
                    break;
            }
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