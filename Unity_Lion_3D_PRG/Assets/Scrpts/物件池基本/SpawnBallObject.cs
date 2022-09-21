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
        /// 取出物件：設定開啟
        /// </summary>
        /// <param name="ball">動作物件</param>
        private void SpawnTake(GameObject ball) => ball.SetActive(true);
        /// <summary>
        /// 放回物件：設定關閉
        /// </summary>
        private void SpawnRelease(GameObject ball) => ball.SetActive(false);
        /// <summary>
        /// 超量物件：刪除
        /// </summary>
        private void SpawnDestory(GameObject ball) => Destroy(ball);
        //private void SP<T>(ref T ball) where T : IObjectPool<T>

        // 連續取出物件
        private void Spawn()
        {            
            Vector3 v3;
            v3.x = Random.Range(-24, 24);
            v3.y = Random.Range(10, 30);
            v3.z = Random.Range(-20, 20);
            GameObject tmpball =  ballPool.Get();
            tmpball.transform.position = v3;
            // 設定碰撞事件
            tmpball.GetComponent<BallCollisionPool>().ballOnHit = ReleaseBall;
        }
        // 退回物件
        public void ReleaseBall(GameObject ball)
        {
            ballPool.Release(ball);
        }
    }
}