using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

namespace agi
{
    public class ObjectPoolSystem : MonoBehaviour
    {
        [SerializeField, Header("存放物件")]
        protected GameObject PoolObj;
        protected ObjectPool<GameObject> ObjPool;
        [SerializeField, Header("上限")]
        private int limit = 10;
        //
        protected List<GameObject> outsideObj;

        private void Awake()
        {
            ObjPool = new ObjectPool<GameObject>(CreateObj, SpawnTake, SpawnRelease, SpawnDestory, false, limit);
            outsideObj = new List<GameObject>();
        }
        // 物件池基本建構子內容
        protected virtual GameObject CreateObj() => Instantiate(PoolObj);
        protected virtual void SpawnDestory(GameObject obj) => Destroy(obj);
        protected virtual void SpawnRelease(GameObject obj) => obj.SetActive(false);
        protected virtual void SpawnTake(GameObject obj) => obj.SetActive(true);
        // 公開調用方法
        public int setLimit { set { limit = value; } }
        public int getLimit => limit;
        public GameObject SpwanObj() => ObjPool.Get();
        public GameObject SpwanObj(GameObject setObj)
        {
            PoolObj = setObj;
            return ObjPool.Get();
        }
        public void RelaseObj(GameObject obj) => ObjPool.Release(obj);
    }
}