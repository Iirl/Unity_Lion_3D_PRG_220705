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
        protected virtual GameObject CreateObj() => Instantiate(PoolObj);           //若沒有釋放物就創造
        protected virtual void SpawnDestory(GameObject obj) => Destroy(obj);        //設定破壞物件的條件
        protected virtual void SpawnRelease(GameObject obj) => obj.SetActive(false);//Release 物件
        protected virtual void SpawnTake(GameObject obj) => obj.SetActive(true);    //Get 物件
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