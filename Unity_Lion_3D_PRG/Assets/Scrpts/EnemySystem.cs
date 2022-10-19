using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace agi
{
    public class EnemySystem : MonoBehaviour
    {
        #region ���
        EnemyAttack enemyAttack; 
        #endregion

        [SerializeField, Header("�ĤH���")]
        private DataEnemy dataEnemy;
        //[SerializeField, Header("��l��m")]
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
        /// ����A
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
        /// ���C���A
        /// </summary>
        /// <returns></returns>
        private IEnumerator Wander()
        {
            MoveAnimateControl();
            //print("���C");
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
        /// �ˬd����{���G
        /// �u�n�b�l�ܰ�H��������X�{�F���a�A�N�|���榹�{���C
        /// �p�G�I��F���a=�i�J�l�ܪ��A�C
        /// �_�h = �^�첾��(���C)���A�C
        /// </summary>
        private void CheckTargetTrackObject()
        {
            Collider[] hits = Physics.OverlapSphere(transform.position + Vector3.up, dataEnemy.rangeTrack, dataEnemy.target_msak);
            if (hits.Length > 0)
            {
                //print($"�I�쪫�� {hits[0].name}");
                //transform.LookAt(hits[0].transform);
                target_v3 = hits[0].transform.position;
                if (enemyState == GeneMotion.isAtking) return;
                enemyState = enemyState.ToTrack();
            }
            else enemyState = enemyState.ToRun();

        }
        /// <summary>
        /// �l�ܪ��A�A�����A�|������a�񪱮a�C
        /// </summary>
        private void Track()
        {
            if (ani.GetCurrentAnimatorStateInfo(0).IsName("Attack")) nma.velocity = Vector3.zero;
            nma.SetDestination(target_v3);
            MoveAnimateControl(0.3f);
            ani.ResetTrigger("toAtk");
            if (Vector3.Distance(transform.position, target_v3) < dataEnemy.rangeAttack)
            {
                //print($"�����ؼ�");
                enemyState = enemyState.ToAtk();
                ani.SetBool(GeneMotion.isAtking.ToString(),true);
            } else
            {
                ani.SetBool(GeneMotion.isAtking.ToString(), false);
                atkTime = dataEnemy.intervalAtkTime;
            }

        }
        /// <summary>
        /// �������A�A�C�����@���N�|�^��l�ܪ��A�C
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
        /// �]�w���ʰʵe���A�C
        /// </summary>
        /// <param name="f">�j��0.5���]�B</param>
        private void MoveAnimateControl(float f=1) => ani.SetFloat(GeneMotion.Running.ToString(), f);

        #region �ƥ�ϰ�
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
            //nma.Stop();  //�ɶq���n�ιL�ɪ���k�A��s�W�e���X���C
            nma.isStopped = true;
        }
        private void OnDrawGizmosSelected()
        {
            // �]�w�����d��
            //Gizmos.color = new Color(1, 0, 0, 0.15f);
            Gizmos.color = dataEnemy.attackRangeColor;
            Gizmos.DrawSphere(transform.position + Vector3.up, dataEnemy.rangeAttack);

            // �]�w�l�ܽd��
            Gizmos.color = dataEnemy.traceRangeColor;
            Gizmos.DrawSphere(transform.position + Vector3.up, dataEnemy.rangeTrack);

            // �]�w�l�ܥؼ�
            Gizmos.color = new Color(1, 0, 0, 1f);
            Gizmos.DrawSphere(target_v3, 0.3f);
        }
        #endregion


    }
}