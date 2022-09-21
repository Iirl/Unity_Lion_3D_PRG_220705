using UnityEngine;
using UnityEngine.Pool;

namespace agi
{
    public class SpawnBallObject : MonoBehaviour
    {
        [SerializeField]
        GameObject ball;
        ObjectPool<GameObject> ballPool;
        private int limitBall = 10;

        private void Awake()
        {
            ballPool = new ObjectPool<GameObject>(CreateBall, SpawnTake, SpawnRelease, SpawnDestory,false, limitBall);
            InvokeRepeating("Spawn", 0, 0.2f);
        }

        private GameObject CreateBall() => Instantiate(ball);
        /// <summary>
        /// ���X����G�]�w�}��
        /// </summary>
        /// <param name="ball">�ʧ@����</param>
        private void SpawnTake(GameObject ball) => ball.SetActive(true);
        /// <summary>
        /// ��^����G�]�w����
        /// </summary>
        private void SpawnRelease(GameObject ball) => ball.SetActive(false);
        /// <summary>
        /// �W�q����G�R��
        /// </summary>
        private void SpawnDestory(GameObject ball) => Destroy(ball);
        //private void SP<T>(ref T ball) where T : IObjectPool<T>

        // �s����X����
        private void Spawn()
        {            
            Vector3 v3;
            v3.x = Random.Range(-24, 24);
            v3.y = Random.Range(10, 30);
            v3.z = Random.Range(-20, 20);
            GameObject tmpball =  ballPool.Get();
            tmpball.transform.position = v3;
            // �]�w�I���ƥ�
            tmpball.GetComponent<BallCollisionPool>().ballOnHit = ReleaseBall;
        }
        // �h�^����
        public void ReleaseBall(GameObject ball)
        {
            ballPool.Release(ball);
        }
    }
}